using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //Skripta koja kontrolira kolicinu, broj i poziciju spawna neprijatelja
    public Transform spawnPoint;

    //is the spawner active and spawning enemies
    public bool spawning;

    //has the spawner completed this round of spawns
    public bool finishedSpawning;

    //lista neprijatelja za spawnati, treba puniti pomocu neke druge skripte
    public Dictionary<Enemy.EnemyType, int> neprijateljiZaSpawnat = new Dictionary<Enemy.EnemyType, int>();

    //queue za spawnanje neprijatelja
    private List<Enemy.EnemyType> spawnQueue = new List<Enemy.EnemyType>();
    private int spawnQueueIndex = 0;

    //spawn delay izmedu 2 neprijatelja
    public float spawnDelay;

    //treba li spawnati sve vrste neprijatelja odjednom ili jedan po jedan
    public bool spawnEnemiesTogether;

    private EnemyWaveController enemyWaveController;

    private Transform player;

    void Awake()
    {
        enemyWaveController = GameObject.FindGameObjectWithTag("WaveController").GetComponent<EnemyWaveController>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawning = false;
        finishedSpawning = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawning()
    {
        //pokreni korutinu za spawnanje
        spawning = true;
        if (!finishedSpawning)
        {
            if (!spawnEnemiesTogether)
            {
                spawnQueue.Clear();
                //ako se spawnaju randomly jedan po jedan, stvori spawn queue
                spawnQueueIndex = 0;
                foreach (Enemy.EnemyType enemyType in neprijateljiZaSpawnat.Keys.ToList())
                {
                    for (int i = 0; i < neprijateljiZaSpawnat[enemyType]; i++)
                    {
                        spawnQueue.Add(enemyType);
                    }
                }

                //shuffle
                Shuffle(spawnQueue);

                
                
            }
            StartCoroutine(SpawnEnemy());
        }

    }
    
    // Fisher-Yates shuffle algorithm
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        if (spawnEnemiesTogether)
        {
            //spawnaj jedan od svakog neprijatelja zajedno
            foreach (Enemy.EnemyType enemyType in neprijateljiZaSpawnat.Keys.ToList())
            {
                if (neprijateljiZaSpawnat[enemyType] > 0)
                {
                    neprijateljiZaSpawnat[enemyType] -= 1;
                    GameObject spawnedEnemy = enemyWaveController.SpawnPooledEnemy(enemyType);
                    SetUpEnemy(spawnedEnemy);
                }


            }
        }
        else
        {
            //spawnaj jedan po jedan
            //odaberi iz queuea




            Enemy.EnemyType enemyType = spawnQueue[spawnQueueIndex];
            spawnQueueIndex++;
            neprijateljiZaSpawnat[enemyType] -= 1;
            GameObject spawnedEnemy = enemyWaveController.SpawnPooledEnemy(enemyType);
            SetUpEnemy(spawnedEnemy);

        }

        yield return new WaitForSeconds(spawnDelay);

        //provjeri jel jos ima neprijatelja u dictu
        if (neprijateljiZaSpawnat.Values.Sum() <= 0)
        {
            //prazan dict, prestani spawnat
            spawning = false;
            finishedSpawning = true;
            enemyWaveController.CheckIfWaveIsOver();
            yield break;
        }

        StartCoroutine(SpawnEnemy());


    }

    private void SetUpEnemy(GameObject spawnedEnemy)
    {
        spawnedEnemy.transform.position = transform.position;
        spawnedEnemy.transform.parent = transform;
        //postavi agenta i tako
        Enemy enemySkripta = spawnedEnemy.GetComponent<Enemy>();
        NavMeshAgent agent = spawnedEnemy.GetComponent<NavMeshAgent>();
        agent.SetDestination(player.position);
    }
}
