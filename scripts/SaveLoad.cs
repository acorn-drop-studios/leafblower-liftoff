using System;
using Godot;
using FileAccess = Godot.FileAccess;

namespace LeafblowerLiftoff.scripts;

public partial class SaveLoad : Node
{
    public static uint GetHighScore()
    {
        using var saveFile = FileAccess.Open("user://savegame.dat", FileAccess.ModeFlags.Read);
        
        return saveFile.Get32();;
    }

    public static void SaveHighScore(uint highScore)
    {
        if (highScore < GameManager.Instance.HighScore)
        {
            return;
        }

        GameManager.Instance.HighScore = highScore;

        using var saveFile = FileAccess.Open("user://savegame.dat", FileAccess.ModeFlags.Write);
        saveFile.Store32(highScore);
        saveFile.Close();
    }
}