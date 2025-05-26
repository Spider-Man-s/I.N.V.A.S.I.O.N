using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;


    public void PlaySoundOnce()
    {
        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }
}



