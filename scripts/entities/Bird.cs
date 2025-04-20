using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.entities;

[GlobalClass]
public partial class Bird : Spawnable
{
	public void OnBodyEntered(Node body)
	{
		if (body is Player player)
		{
			GD.Print("Bird collided with player");
			GameManager.Instance.SetGameState(GameState.GameOver);
		}
	}
}
