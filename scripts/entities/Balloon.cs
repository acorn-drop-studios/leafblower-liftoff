using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.entities;

public partial class Balloon : Spawnable
{
	public void OnBodyEntered(Node body)
	{
		if (body is Player player)
		{
			GD.Print("Balloon collided with player");
			GameManager.Instance.SetGameState(GameState.GameOver);
		}
	}
	
	public void ScreenExited()
	{
		QueueFree();
	}
}
