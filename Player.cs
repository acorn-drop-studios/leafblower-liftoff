using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public int LiftOffSpeed { get; set; } = 1500;
    private float _boost = 150f;
    private float _increment = 250f;
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        if (Input.IsActionJustPressed("liftoff"))
        {
            velocity.Y = -_boost;
            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = true;
        }
        else if (Input.IsActionPressed("liftoff"))
        {
            velocity.Y -= _increment * (float)delta;
            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = true;
        }
        else
        {
            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = false;
            velocity.Y += _gravity * (float)delta;
        }

        if (IsOnFloor())
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("run");
        }
        else
        {
            // velocity.Y += _gravity * (float)delta;
            var isFalling = velocity.Y > 0;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(isFalling ? "fall" : "jump");
        }

        Velocity = velocity;
        if (velocity.Y != 0)
            GD.Print("Velocity: " + Velocity);

        MoveAndSlide();
    }
}