using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class WaveDefinitions : MonoBehaviour
{
    //Skripta za definiciju waveova za svaki stage igre i spawnera

    public SerializedDictionary<Enemy.EnemyType, int> Key0Wave = new SerializedDictionary<Enemy.EnemyType, int>();
    public List<EnemySpawner> Key0Spawners = new List<EnemySpawner>();

    public SerializedDictionary<Enemy.EnemyType, int> Key1Wave = new SerializedDictionary<Enemy.EnemyType, int>();
    public List<EnemySpawner> Key1Spawners = new List<EnemySpawner>();

    public SerializedDictionary<Enemy.EnemyType, int> Key2Wave = new SerializedDictionary<Enemy.EnemyType, int>();
    public List<EnemySpawner> Key2Spawners = new List<EnemySpawner>();

    public SerializedDictionary<Enemy.EnemyType, int> Key3Wave = new SerializedDictionary<Enemy.EnemyType, int>();
    public List<EnemySpawner> Key3Spawners = new List<EnemySpawner>();

    public SerializedDictionary<Enemy.EnemyType, int> Key4Wave = new SerializedDictionary<Enemy.EnemyType, int>();
    public List<EnemySpawner> Key4Spawners = new List<EnemySpawner>();
}
