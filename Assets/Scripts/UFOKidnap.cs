using UnityEngine;

public class UFOKidnap : MonoBehaviour
{

    public GameObject ufoPrefab;
    public Transform spawnPoint;
    public Transform finalPoint;
    public float ufoSpeed = 5f;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            hasSpawned = true;

            // Spawn UFO
            GameObject ufo = Instantiate(ufoPrefab, spawnPoint.position, spawnPoint.rotation);

            // Start moving UFO toward final point
            ufo.AddComponent<UFOMovement>().Init(finalPoint.position, ufoSpeed);
        }
    }
}
