using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BNG;
public class DamagePack : MonoBehaviour
{
    public float powerupDuration = 30f;
    public float respawnTime = 30f;
    public AudioClip pickupSound;

    private Collider[] colliders;
    private Renderer[] renderers;
    private bool isCollected = false;

    private Dictionary<RaycastWeapon, float> originalDamages = new Dictionary<RaycastWeapon, float>();

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
            StartCoroutine(ActivateDoubleDamage());
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            HidePickup();
            StartCoroutine(RespawnAfterDelay(respawnTime));
        }
    }

    IEnumerator ActivateDoubleDamage()
    {
        RaycastWeapon[] weapons = GameObject.FindObjectsOfType<RaycastWeapon>();
        originalDamages.Clear();

        foreach (var weapon in weapons)
        {
            if (weapon.CompareTag("Weapon"))
            {
                originalDamages[weapon] = weapon.Damage;
                weapon.Damage *= 2f;
            }
        }

        yield return new WaitForSeconds(powerupDuration);

        foreach (var entry in originalDamages)
        {
            if (entry.Key != null)
            {
                entry.Key.Damage = entry.Value;
            }
        }
    }

    void HidePickup()
    {
        isCollected = true;

        foreach (var col in colliders)
            col.enabled = false;

        foreach (var rend in renderers)
            rend.enabled = false;
    }

    void ShowPickup()
    {
        isCollected = false;

        foreach (var col in colliders)
            col.enabled = true;

        foreach (var rend in renderers)
            rend.enabled = true;
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPickup();
    }
}
