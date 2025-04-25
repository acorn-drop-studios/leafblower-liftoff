using Godot;

namespace LeafblowerLiftoff.scripts.entities;

[GlobalClass]
public partial class Spawnable: BaseEntity
{
	public virtual void Spawn(Vector2 spawnLocation, Vector2? target)
	{
		Position = spawnLocation;
	}
}
