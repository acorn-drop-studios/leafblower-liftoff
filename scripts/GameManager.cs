using Godot;
using System;
using LeafblowerLiftoff.scripts.enums;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }
	private readonly object @lock = new();

	public int Score { get; private set; } = 0;
	public GameState GameState { get; private set; } = GameState.Playing;
	public double Speed { get; set; } = 400;

	[Signal]
	public delegate void ScoreChangedEventHandler(int score);
	[Signal]
	public delegate void GameStateChangedEventHandler();

	public override void _Ready()
	{
		Instance = this;
		EmitSignal(SignalName.GameStateChanged);
	}
	
	public int IncrementScore(int amount = 100)
	{
		lock (@lock)
		{
			Score += amount;
			EmitSignal(SignalName.ScoreChanged, Score);
			return Score;
		}
	}
	
	public void SetGameState(GameState gameState)
	{
		lock (@lock)
		{
			GameState = gameState;
			EmitSignal(SignalName.GameStateChanged);
		}
	}
}
