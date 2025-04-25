using System;
using System.Linq;
using Godot;
using LeafblowerLiftoff.scripts.entities;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts;

public partial class Spawner : Node
{
	[Export] public PackedScene NodeScene { get; set; }
	[Export] public Node2D TargetNode { get; set; }
	[Export] public float SpawnTimerMin { get; set; } = 1.0f;
	[Export] public float SpawnTimerMax { get; set; } = 5.0f;

	private Timer _spawnTimer;

	public override void _Ready()
	{
		_spawnTimer = new Timer();
		AddChild(_spawnTimer);

		_spawnTimer.WaitTime = GetTime();
		_spawnTimer.Timeout += OnSpawnTimerTimeout;
		GameManager.Instance.GameStateChanged += () =>
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Playing:
					_spawnTimer.Start();
					break;
				case GameState.GameOver:
					_spawnTimer.Stop();
					break;
				case GameState.Menu:
					_spawnTimer.Stop();
					break;
				case GameState.NewGame:
					Reset();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		};
	}

	private void Reset()
	{
		foreach (var node in GetChildren().Where(x=>x is Spawnable))
		{
			node.QueueFree();
		}
	}
	
	private void OnSpawnTimerTimeout()
	{
		Spawn();
		_spawnTimer.WaitTime = GetTime();
	}

	private float GetTime()
	{
		var intervalDivider = GameManager.Instance.CurrentSpeed / (int)Speed.Start;

		return (float)GD.RandRange(SpawnTimerMin / intervalDivider, SpawnTimerMax / intervalDivider);
	}

	private Vector2 GetSpawnLocation()
	{
		var y = GD.RandRange(64, 600);

		return new Vector2(2000, y);
	}

	public virtual void Spawn()
	{
		var spawnLocation = GetSpawnLocation();
		if (spawnLocation != Vector2.Zero)
		{
			var nodeScene = NodeScene.Instantiate();
			if (nodeScene is entities.Spawnable spawnable)
			{
				AddChild(nodeScene);
				spawnable.Spawn(spawnLocation, null);
			}
		}
	}
}
