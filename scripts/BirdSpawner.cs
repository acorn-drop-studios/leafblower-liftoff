using Godot;
using System;
using LeafblowerLiftoff.scripts;
using LeafblowerLiftoff.scripts.entities;

public partial class BirdSpawner : Spawner
{
	public override void Spawn()
	{
		var spawnLocation = new Vector2(2000,TargetNode.Position.Y);
		if (spawnLocation != Vector2.Zero)
		{
			var nodeScene = NodeScene.Instantiate();
			if (nodeScene is Spawnable spawnable)
			{
				AddChild(nodeScene);
				spawnable.Spawn(spawnLocation, null);
			}
		}
	}
}
