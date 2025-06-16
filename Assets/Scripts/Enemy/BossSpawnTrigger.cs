using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject boss1Prefab; 

    [Header("Spawn Settings")]
    public Vector3 spawnPosition = new Vector3(23.084f, -0.675f, 8.165258f);
    public Quaternion spawnRotation = new Quaternion(0.0f, -1.0f, 0.0f, 0.000002950429461634485f);
    private Vector3 spawnScale = Vector3.one;

    [Header("Trigger Settings")]
    public bool destroyTriggerAfterSpawn = true; 
    public bool spawnOnlyOnce = true; 

    private bool hasSpawned = false;

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("BossSpawnTrigger: Nema Collider komponente na objektu!");
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning("BossSpawnTrigger: Collider nije postavljen kao Trigger!");
        }

        if (boss1Prefab == null)
        {
            Debug.LogError("BossSpawnTrigger: Boss1 Prefab nije postavljen!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnOnlyOnce && hasSpawned)
            {
                return;
            }

            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (boss1Prefab != null)
        {
            GameObject spawnedBoss = Instantiate(boss1Prefab, spawnPosition, spawnRotation);
            spawnedBoss.transform.localScale = spawnScale;

            Debug.Log("Boss1 je spawn-ovan na poziciji: " + spawnPosition);

            hasSpawned = true;

            if (destroyTriggerAfterSpawn)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("BossSpawnTrigger: Boss1 Prefab nije postavljen!");
        }
    }

    public void ResetSpawn()
    {
        hasSpawned = false;
    }
}