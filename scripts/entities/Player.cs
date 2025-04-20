using Godot;

namespace LeafblowerLiftoff.scripts.entities;

public partial class Player : CharacterBody2D
{
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private float _boostSpeed = 100f;
    private float _boostIncrement = 50f;

    public override void _PhysicsProcess(double delta)
    {
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
            emitting = false;
        }
        else
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("fall");
            emitting = false;
        }


        GetNode<GpuParticles2D>("GPUParticles2D").Emitting = emitting;
        GetNode<Area2D>("GPUParticles2D/Area2D").Monitoring = emitting;

        Velocity = velocity;
        MoveAndSlide();
    }

    public void OnParticleAreaEntered(Area2D area)
    {
        area.QueueFree();
        GD.Print("Particle area entered");
    }

    public void OnBodyEntered(Node body)
    {
        body.QueueFree();
        GD.Print("Body entered");
    }
}