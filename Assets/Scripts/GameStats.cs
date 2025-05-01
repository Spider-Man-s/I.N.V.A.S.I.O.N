
using UnityEngine;

public static class GameStats
{

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public static Difficulty currentDifficulty = Difficulty.Easy;

    public static int Score = 999999;

    public static bool PlayerDeath = false;

    public static int RevolverUpgradeLevel = 1;




}
