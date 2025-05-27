using UnityEngine;

public class Enemy : MonoBehaviour
{
    //base enemy skripta za nasljedivanje
    public enum EnemyType
    {
        Enemy1,
        Enemy2
    }

    public EnemyType enemyType;

    public float hp;

    //score when defeated
    public int score;

    public GameObject enemyPrefab;

    private void TrackPlayer()
    {
        //pronadi igraca
    }
}
