using Godot;
using System;
using LeafblowerLiftoff.scripts.enums;

public partial class World : ParallaxBackground
{
	private bool _scrolling = true;

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
		ScrollOffset = ScrollOffset with { X = ScrollOffset.X - (float)(GameManager.Instance.Speed * delta) };
	}
}
