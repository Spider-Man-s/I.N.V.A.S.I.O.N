using UnityEngine;
using System.Collections;
public class ShieldPack : MonoBehaviour
{
    public int shieldAmount = 25;
    public AudioClip pickupSound;
    private float respawnTime = 30f;

    private Collider[] colliders;
    private Renderer[] renderers;
    private bool isCollected = false;

    void Start()
    {
        colliders = GetComponents<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {

            GameStats.PlayerShields = Mathf.Min(GameStats.PlayerShields + shieldAmount, 100);

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            HidePickup();
            StartCoroutine(RespawnAfterDelay(respawnTime));
        }
    }

    void HidePickup()
    {
        isCollected = true;

        foreach (Collider col in colliders)
            col.enabled = false;

        foreach (Renderer rend in renderers)
            rend.enabled = false;
    }

    void ShowPickup()
    {
        isCollected = false;

        foreach (Collider col in colliders)
            col.enabled = true;

        foreach (Renderer rend in renderers)
            rend.enabled = true;
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPickup();
    }
}
