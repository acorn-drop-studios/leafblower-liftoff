using System;
using Godot;
using LeafblowerLiftoff.scripts.enums;

namespace LeafblowerLiftoff.scripts;

public partial class GameManager : Node
{
    private const int MetersPerSecond = 8;
    public static GameManager Instance { get; private set; }
    private readonly object @lock = new();
    private bool _stopProcessingInput = false;

    public int Score { get; private set; } = 0;
    public GameState GameState { get; private set; } = GameState.Menu;
    public double CurrentSpeed { get; set; } = (int)Speed.Reset;
    public uint HighScore { get; set; }

    public double Distance { get; set; }

    [Signal]
    public delegate void ScoreChangedEventHandler(int score);

    [Signal]
    public delegate void DistanceChangedEventHandler(int distance);

    [Signal]
    public delegate void HighScoreChangedEventHandler(int highScore);

    [Signal]
    public delegate void GameStateChangedEventHandler();

    public override void _Ready()
    {
        Instance = this;
        CurrentSpeed = (int)Speed.Start;
        EmitSignal(SignalName.GameStateChanged);
        HighScore = SaveLoad.GetHighScore();
    }

    public void SetGameState(GameState gameState)
    {
        if (gameState == GameState)
        {
            return;
        }

        lock (@lock)
        {
            GameState = gameState;
            switch (gameState)
            {
                case GameState.Playing:
                    EmitSignal(SignalName.HighScoreChanged, HighScore);
                    break;
                case GameState.GameOver:
                    CurrentSpeed = (int)Speed.Reset;
                    SaveLoad.SaveHighScore((uint)Distance);
                    _stopProcessingInput = true;
                    var waitTimer = new Timer()
                    {
                        Autostart = true,
                        OneShot = true,
                        WaitTime = 3f,
                    };
                    AddChild(waitTimer);
                    waitTimer.Timeout += () => _stopProcessingInput = false;
                    waitTimer.Start();
                    break;
                case GameState.NewGame:
                    NewGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }

            EmitSignal(SignalName.GameStateChanged);
        }
    }

    private void NewGame()
    {
        Score = 0;
        Distance = 0;
        EmitSignal(SignalName.ScoreChanged);
        EmitSignal(SignalName.DistanceChanged, (int)Distance);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (_stopProcessingInput)
        {
            return;
        }

        if (@event is InputEventScreenTouch touch)
        {
            var ev = new InputEventAction()
            {
                Action = "liftoff",
                Pressed = true,
            };
            Input.ParseInputEvent(ev);
            return;
        }

        switch (GameState)
        {
            case GameState.Menu:
            case GameState.NewGame:
                SetGameState(GameState.Playing);
                break;
            case GameState.GameOver:
                if (@event.IsActionPressed("liftoff"))
                {
                    SetGameState(GameState.NewGame);
                }

                break;
            case GameState.Playing:
                break;
        }


        GetViewport().SetInputAsHandled();
    }

    public override void _Process(double delta)
    {
        if (GameState != GameState.Playing)
        {
            return;
        }

        CurrentSpeed += (int)Speed.Step * delta;
        CurrentSpeed = Mathf.Clamp(CurrentSpeed, (double)Speed.Start, (double)Speed.Max);
        Distance += (MetersPerSecond * delta) * (CurrentSpeed / (int)Speed.Start);
        EmitSignal(SignalName.DistanceChanged, (int)Distance);
    }

    public void AddScore(int score)
    {
        lock (@lock)
        {
            Score += score;
            EmitSignal(SignalName.ScoreChanged, Score);
        }
    }
}