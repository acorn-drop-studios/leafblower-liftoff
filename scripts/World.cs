using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts;

public partial class World : ParallaxBackground
{
	private bool _scrolling;

	public override void _Ready()
	{
		GameManager.Instance.GameStateChanged += () =>
		{
			_scrolling = GameManager.Instance.GameState switch
			{
				GameState.Playing => true,
				GameState.GameOver or GameState.Menu => false,
				_ => _scrolling
			};
		};
	}

	public override void _Process(double delta)
	{
		if (!_scrolling) return;
		ScrollOffset = ScrollOffset with { X = ScrollOffset.X - (float)(GameManager.Instance.CurrentSpeed * delta) };
	}
}
