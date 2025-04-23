using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.gui;

public partial class StartScreen : Control
{
    public override void _Ready()
    {
        GameManager.Instance.GameStateChanged += () =>
        {
            switch (GameManager.Instance.GameState )
            {
                case  GameState.Menu:
                    Visible = true;
                    break;
                default:
                    Visible = false;
                    break;
            }
        };
    }
}