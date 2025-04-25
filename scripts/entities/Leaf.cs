using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.entities;

[GlobalClass]
public partial class Leaf : Spawnable
{
	public override void Spawn(Vector2 spawnLocation, Vector2? target)
	{
		Position = new Vector2(2000, 581);
	}

	// public override void _PhysicsProcess(double delta)
	// {
	//     if (_isScrolling)
	//     {
	//         var body = GetNode<RigidBody2D>("StaticBody2D");
	//         var velocity = body.LinearVelocity;
	//         velocity.X = -(float)(GameManager.Instance.CurrentSpeed + Speed);
	//         //
	//         body.LinearVelocity = velocity;
	//     }
	// }

	public override void _Process(double delta)
	{
	}

	public void ScreenExited()
	{
		QueueFree();
	}
}
