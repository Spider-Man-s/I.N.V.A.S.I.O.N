using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;
using AYellowpaper.SerializedCollections;
using Unity.Mathematics;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

public class EnemyWaveController : MonoBehaviour
{
    //skripta koja kontrolira koji spawner ce se upalit

    //dict koji spaja enemy type sa instancom neprijatelja
    public SerializedDictionary<Enemy.EnemyType, GameObject> enemyDict = new SerializedDictionary<Enemy.EnemyType, GameObject>();

    //za poolanje neprijatelja
    private Dictionary<Enemy.EnemyType, List<GameObject>> pooledEnemies = new Dictionary<Enemy.EnemyType, List<GameObject>>();

    //trenutni wave
    public Dictionary<Enemy.EnemyType, int> currentWave = new Dictionary<Enemy.EnemyType, int>();
    
    public List<EnemySpawner> allSpawners = new List<EnemySpawner>();

    public List<EnemySpawner> activeSpawners = new List<EnemySpawner>();

    //downtime izmedu waveova
    public float downTime;

    //mogu li se uopce waveovi spawnati
    public bool canStartWave;

    public bool waveStarted = false;

    private Transform player;

    private WaveDefinitions waveDefinitions;

    //ovisno o stanju igre mijenjaj kolicine neprijatelja, to cemo kasnije



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveDefinitions = transform.GetComponent<WaveDefinitions>();
        //pronadi sve spawnere
        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            //Debug.Log(spawner.name);
            allSpawners.Add(spawner.GetComponent<EnemySpawner>());
        }

        //pronadi playera
        player = GameObject.FindGameObjectWithTag("Player").transform;

        canStartWave = true;
        //za svaki tip enemya napravi pool
        foreach (Enemy.EnemyType enemyType in Enum.GetValues(typeof(Enemy.EnemyType)))
        {
            //potencijalno spawnaj odma par instanca
            pooledEnemies.Add(enemyType, new List<GameObject>());
        }

        //postavi wave
        currentWave.Add(Enemy.EnemyType.Radnik, 0);
        currentWave.Add(Enemy.EnemyType.AranianMac, 0);
        currentWave.Add(Enemy.EnemyType.AranianPistolj, 0);
        currentWave.Add(Enemy.EnemyType.Komando, 0);
        currentWave.Add(Enemy.EnemyType.Sviker, 0);

        //zapocni wave
        if (canStartWave)
        {
            //pokreni wave i spawnanje enemya
            DetermineWave();
            SpawnWave(currentWave);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartWave && !waveStarted)
        {
            //flag upaljen, zapocni spawnanje opet
            DetermineWave();
            SpawnWave(currentWave);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameStats.KeysFound += 1;
        }
    }

    public GameObject SpawnPooledEnemy(Enemy.EnemyType enemyType)
    {
        //provjeri ima li ikog u poolu, ako ne instanciraj novog
        foreach (GameObject pooledEnemy in pooledEnemies[enemyType])
        {
            if (!pooledEnemy.activeInHierarchy)
            {
                //spawnaj ovog lika
                pooledEnemy.SetActive(true);
                //resetaj flagove
                Enemy enemyScript = pooledEnemy.GetComponent<Enemy>();
                enemyScript.ResetEnemy();
                return pooledEnemy;
            }
        }

        //nismo nikog pronasli, spawnaj novog
        GameObject instancedEnemy = Instantiate(enemyDict[enemyType], Vector3.zero, quaternion.identity);
        pooledEnemies[enemyType].Add(instancedEnemy);

        return instancedEnemy;

    }

    private void FindSpawnersNearPlayer(int numOfSpawners)
    {
        activeSpawners.Clear();
        //pronadi prikladne spawnere ovisno o zoni u kojoj je igrac, zasad samo odredi najblizi spawner po udaljenosti

        List<EnemySpawner> foundSpawners = allSpawners.Where(obj => obj != null)
        .Select(obj => new
        {
            Object = obj,
            Distance = Vector3.Distance(player.position, obj.transform.position)
        })
        .OrderBy(x => x.Distance)
        .Take(numOfSpawners)
        .Select(x => x.Object)
        .ToList();


        //Debug.Log(foundSpawners.Count);

        activeSpawners = foundSpawners;

        

    }

    private void DetermineWave()
    {
        //ovisno o stanju igre odredi koje neprijatelje spawnat
        if (GameStats.KeysFound == 0)
        {
            activeSpawners = waveDefinitions.Key0Spawners;
            foreach (Enemy.EnemyType enemy in waveDefinitions.Key0Wave.Keys)
            {
                currentWave[enemy] += waveDefinitions.Key0Wave[enemy];
            }
        }
        else if (GameStats.KeysFound == 1)
        {
            activeSpawners = waveDefinitions.Key1Spawners;
            foreach (Enemy.EnemyType enemy in waveDefinitions.Key1Wave.Keys)
            {
                currentWave[enemy] += waveDefinitions.Key1Wave[enemy];
            }
        }
        else if (GameStats.KeysFound == 2)
        {
            activeSpawners = waveDefinitions.Key2Spawners;
            foreach (Enemy.EnemyType enemy in waveDefinitions.Key2Wave.Keys)
            {
                currentWave[enemy] += waveDefinitions.Key2Wave[enemy];
            }
        }
        else if (GameStats.KeysFound == 3)
        {
            activeSpawners = waveDefinitions.Key3Spawners;
            foreach (Enemy.EnemyType enemy in waveDefinitions.Key3Wave.Keys)
            {
                currentWave[enemy] += waveDefinitions.Key3Wave[enemy];
            }
        }
        else if (GameStats.KeysFound == 4)
        {
            activeSpawners = waveDefinitions.Key4Spawners;
            foreach (Enemy.EnemyType enemy in waveDefinitions.Key4Wave.Keys)
            {
                currentWave[enemy] += waveDefinitions.Key4Wave[enemy];
            }
        }

    }



    private void SpawnWave(Dictionary<Enemy.EnemyType, int> wave)
    {
        waveStarted = true;
        //aktiviraj sve aktivne spawnere

        foreach (EnemySpawner spawner in activeSpawners)
        {
            spawner.neprijateljiZaSpawnat = wave;
            spawner.StartSpawning();
        }
    }

    public void CheckIfWaveIsOver()
    {
        //provjeri jesu li svi spawneri gotovi, ako da stvori downtime
        bool gotovo = true;

        foreach (EnemySpawner spawner in activeSpawners)
        {
            if (!spawner.finishedSpawning)
            {
                gotovo = false;
            }
        }

        if (gotovo)
        {
            DownTime();
        }


    }



    private void DownTime()
    {
        //kada su svi spawneri gotovi, pauziraj spawnanje neprijatelja da igrac malo odise
        Debug.Log("Wave gotov ide pauza od " + downTime.ToString() + " sekundi");
        StartCoroutine(DownTimeCoroutine());
    }

    private IEnumerator DownTimeCoroutine()
    {
        yield return new WaitForSeconds(downTime);

        //resetaj sve spawnere
        foreach (EnemySpawner spawner in activeSpawners)
        {
            spawner.finishedSpawning = false;
        }

        if (canStartWave)
            {
                //zapocni opet waveove
                DetermineWave();
                SpawnWave(currentWave);
                yield break;

            }
            else
            {
                //prekini za sad
                waveStarted = false;
                yield break;
            }
    }

    
}
