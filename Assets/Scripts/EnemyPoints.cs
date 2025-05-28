using UnityEngine;

public class EnemyPoints : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddPoints()
    {
        GameStats.Score += 150;
    }
}
