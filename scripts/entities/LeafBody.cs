using Godot;
using System;
using LeafblowerLiftoff.scripts;
using LeafblowerLiftoff.scripts.enums;

public partial class LeafBody : RigidBody2D
{   
	private bool _isScrolling = false;

	public override void _Ready()
	{
		_isScrolling = GameManager.Instance.GameState == GameState.Playing;
		GameManager.Instance.GameStateChanged += () =>
		{
			_isScrolling = GameManager.Instance.GameState == GameState.Playing;
		};
	}
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if (_isScrolling)
		{
			state.SetLinearVelocity(new Vector2((float)-GameManager.Instance.CurrentSpeed,(float)GameManager.Instance.CurrentSpeed));
		}
	}
}
