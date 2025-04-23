using Godot;

namespace LeafblowerLiftoff.scripts.entities;

[GlobalClass]
public partial class Leaf : Spawnable
{
	public override void Spawn(Vector2 spawnLocation, Vector2? target)
	{
		Position = new Vector2(2000, 581);
	}

	public override void _PhysicsProcess(double delta)
	{
		
	}

	public override void _Process(double delta)
	{
		
	}

	public void ScreenExited()
	{
		QueueFree();
	}
	
}
