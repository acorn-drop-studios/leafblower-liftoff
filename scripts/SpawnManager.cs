using System.Collections.Generic;
using System.Linq;
using Godot;

namespace LeafblowerLiftoff.scripts;

public partial class SpawnManager:Node
{
    public static SpawnManager Instance { get; private set; }

    private List<Vector2> _spawnLocations =
    [
        new Vector2(2000, 581),
        new Vector2(2000, 581),
        new Vector2(2000, 581),
        new Vector2(2000, 581),
        new Vector2(2000, 581)
    ];
    
    public Vector2 GetSpawnLocation()
    {
        if (_spawnLocations.Count == 0)
        {
            GD.Print("No spawn locations available");
            return Vector2.Zero;
        }

        var spawnLocation = GD.Randf() % _spawnLocations.Count;
        var location = _spawnLocations[(int)spawnLocation];
        _spawnLocations.RemoveAt((int)spawnLocation);
        
        return location;
    }

    public void ReturnSpawnLocation(Vector2 spawnLocation)
    {
        _spawnLocations.Add(spawnLocation);
    }
}