using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CallAfterDelay());
    }

    IEnumerator CallAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        PlaySoundOnce();
    }

    public void PlaySoundOnce()
    {
        if (soundToPlay != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }
}
