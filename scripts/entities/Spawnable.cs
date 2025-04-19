using Godot;
using LeafblowerLiftoff.scripts.entities;

namespace LeafblowerLiftoff.scripts;

[GlobalClass]
public partial class Spawnable: BaseEntity
{
	
	public virtual void Spawn(Vector2 spawnLocation, Vector2? target)
	{
		this.Position = spawnLocation;
		GD.Print("Spawned");
		SpawnManager.Instance.ReturnSpawnLocation(spawnLocation);
	}
}
