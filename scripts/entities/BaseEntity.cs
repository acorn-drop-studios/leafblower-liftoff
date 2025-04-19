using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.entities;

public partial class BaseEntity: Node2D
{
    private bool _isScrolling = true;

    public override void _Ready()
    {
        _isScrolling = GameManager.Instance.GameState == GameState.Playing;
        GameManager.Instance.GameStateChanged += () =>
        {
            _isScrolling = GameManager.Instance.GameState == GameState.Playing;
        };
    }

    public override void _Process(double delta)
    {
        if (_isScrolling)
        {
            Position = Position with { X = (float)(Position.X - GameManager.Instance.Speed * delta) };
        }
    }
}