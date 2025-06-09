using UnityEngine;
using System.Collections;
using BNG;

public class SpeedPack : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 30f;
    public float respawnTime = 30f;
    public AudioClip pickupSound;

    public SmoothLocomotion playerController;

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
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            StartCoroutine(ApplySpeedBoost());
            HidePickup();
            StartCoroutine(RespawnAfterDelay(respawnTime));
        }
    }

    IEnumerator ApplySpeedBoost()
    {
        isCollected = true;

        if (playerController != null)
        {
            float originalSpeed = playerController.MovementSpeed;
            playerController.MovementSpeed *= speedMultiplier;

            yield return new WaitForSeconds(duration);

            playerController.MovementSpeed = originalSpeed;
        }
    }

    void HidePickup()
    {
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
