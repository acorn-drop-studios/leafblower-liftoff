using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts.gui;

public partial class GameOverScreen : Control
{
    public override void _Ready()
    {
        GameManager.Instance.GameStateChanged += () =>
        {
            switch (GameManager.Instance.GameState)
            {
                case GameState.GameOver:
                    Visible = true;
                    GetNode<Label>("VBoxContainer/HBoxContainer2/Score").Text = GameManager.Instance.Score.ToString();
                    GetNode<Label>("VBoxContainer/HBoxContainer/Distance").Text =
                        $"{Mathf.RoundToInt(GameManager.Instance.Distance).ToString()}m";

                    break;
                default:
                    Visible = false;
                    break;
            }
        };
    }
}