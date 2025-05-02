
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

    public static int Score = 37;

    public static int PlayerHealth = 100;
    public static bool PlayerDeath = false;

    public static int RevolverUpgradeLevel = 1;
    public static int ShotgunUpgradeLevel = 1;
    public static int ARUpgradeLevel = 1;
    public static int PistolUpgradeLevel = 1;



}
