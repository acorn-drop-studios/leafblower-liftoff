using Godot;

namespace LeafblowerLiftoff.scripts.gui;

public partial class Hud : Control
{
	private Label _scoreLabel;
	private Label _distanceLabel;

	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("HBoxContainer/VBoxContainer/MarginContainer2/Score");
		_distanceLabel = GetNode<Label>("HBoxContainer/VBoxContainer/MarginContainer/Distance");
		
		GameManager.Instance.ScoreChanged += (score) =>
		{
			_scoreLabel.Text = $"{score:D3}";
		};
		GameManager.Instance.DistanceChanged += (distance) =>
		{
			_distanceLabel.Text = $"{distance:D4}m";
		};
		GameManager.Instance.HighScoreChanged += (highScore) =>
		{
			GetNode<Label>("HBoxContainer/VBoxContainer/MarginContainer3/HighScore").Text = $"Best: {highScore.ToString()}";
		};
	}
	
	public void ScreenExited()
	{
		QueueFree();
	}
}
