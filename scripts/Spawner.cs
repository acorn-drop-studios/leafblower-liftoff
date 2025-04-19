using System.Linq;
using Godot;

namespace LeafblowerLiftoff.scripts;

public partial class Spawner : Node
{
    [Export] public PackedScene NodeScene { get; set; }
    [Export] public Node TargetNode { get; set; }
    
    private Timer _spawnTimer;
    private float _spawnTimerWaitTime = 1.0f;

    public override void _Ready()
    {
        _spawnTimer = new Timer();
        AddChild(_spawnTimer);
        _spawnTimer.WaitTime = _spawnTimerWaitTime;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        Spawn();
    }

    public void Spawn()
    {
        var spawnLocation = SpawnManager.Instance.GetSpawnLocation();
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