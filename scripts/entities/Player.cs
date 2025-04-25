using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.entities;

public partial class Player : CharacterBody2D
{
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private float _boostSpeed = 100f;
	private float _boostIncrement = 50f;

	public override void _Ready()
	{
		GameManager.Instance.GameStateChanged += () =>
		{
			if (GameManager.Instance.GameState == GameState.NewGame)
			{
				Velocity = Vector2.Zero;
				Position = new Vector2(116, 584);
			}
			else if (GameManager.Instance.GameState == GameState.GameOver)
			{
				GetNode<GpuParticles2D>("GPUParticles2D").Emitting = false;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("die");
				Velocity = Velocity with { X = (float)Speed.Start, Y = _gravity };
			}
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		if (GameManager.Instance.GameState != GameState.Playing)
		{
			GetNode<GpuParticles2D>("GPUParticles2D").Emitting = false;
			//GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
			Velocity = Velocity with
			{
				X = (float)Mathf.Clamp(Velocity.X - 150 * delta, 0.0f, (float)Speed.Max),
				Y = Mathf.Clamp(Velocity.Y + _gravity * (float)delta, 0, 3000)
			};
			MoveAndSlide();
			return;
		}

		var velocity = Velocity;
		var boosting = false;
		var emitting = false;

		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("liftoff"))
		{
			velocity.Y -= _boostSpeed;
			boosting = true;
		}
		else if (Input.IsActionPressed("liftoff"))
		{
			velocity.Y -= _boostIncrement;
			boosting = true;
		}

		if (boosting)
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("jump");
			emitting = true;
		}
		else if (IsOnFloor())
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("run");
		}
		else
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("fall");
		}

		GetNode<GpuParticles2D>("GPUParticles2D").Emitting = emitting;
		GetNode<Area2D>("GPUParticles2D/Area2D").Monitoring = emitting;

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnParticleAreaEntered(Area2D area)
	{
		GD.Print(area.GetParent().Name);
		if (area.GetParent() is Leaf leaf)
		{
			GD.Print("Hit leaf");
			//leaf.GetChild<StaticBody2D>(0).ConstantLinearVelocity = new Vector2(-100,0);
			GameManager.Instance.AddScore(1);
		}
	}

	public void OnBodyEntered(Node2D body)
	{
		//body.QueueFree();
		if (body.GetParent() is Leaf leaf)
		{
			GD.Print("Body entered" + body.Name);
			var staticBody = body as RigidBody2D;
			staticBody.ApplyCentralImpulse(new Vector2(-2000,-5000));
			GameManager.Instance.AddScore(1);
			staticBody.SetCollisionLayerValue(4,false);
		}
	}
}
