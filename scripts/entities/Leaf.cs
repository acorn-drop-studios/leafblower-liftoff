using Godot;
using System;
using LeafblowerLiftoff.scripts;

[GlobalClass]
public partial class Leaf : Spawnable
{
	public override void _Ready()
	{
	}

	public override void Spawn(Vector2 spawnLocation, Vector2? target)
	{
		this.Position = new Vector2(2000, 581);
		GD.Print("Spawned leaf");
		SpawnManager.Instance.ReturnSpawnLocation(spawnLocation);
	}

	
}
