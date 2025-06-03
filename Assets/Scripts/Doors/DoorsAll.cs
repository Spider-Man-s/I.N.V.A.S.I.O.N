using UnityEngine;
using System.Collections;

public class DoorsAll : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator doorAnimator;
    public string openTrigger = "OpenDoor";
    public string closeTrigger = "CloseDoor";

    private int objectsInTrigger = 0;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public MeshRenderer visualEffect1;
    public MeshRenderer visualEffect2;
    public float effectDuration = 1.3f;

    void Start()
    {
        if (doorAnimator == null)
            doorAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Optional: filter by tag if needed, e.g., if (other.CompareTag("Player"))
        objectsInTrigger++;
        if (objectsInTrigger == 1)
        {
            doorAnimator.SetTrigger(openTrigger);
        }
        if (!GetComponent<Animator>().enabled)
        {
            audioSource.PlayOneShot(audioClip);
            StartCoroutine(ShowEffectTemporary());
        }
    }

    void OnTriggerExit(Collider other)
    {
        objectsInTrigger--;
        if (objectsInTrigger <= 0)
        {
            doorAnimator.SetTrigger(closeTrigger);
            objectsInTrigger = 0; // Just in case
        }
    }

    IEnumerator ShowEffectTemporary()
    {
        visualEffect1.enabled = true;
        visualEffect2.enabled = true;
        yield return new WaitForSeconds(effectDuration);
        visualEffect1.enabled = false;
        visualEffect2.enabled = false;
    }

}
