using UnityEngine;

public class RemoveUI : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject objectToRemove;

    private bool hasStarted = false;

    void Update()
    {
        if (audioSource == null || objectToRemove == null)
            return;

        if (audioSource.isPlaying)
        {
            hasStarted = true;
        }

        if (hasStarted && !audioSource.isPlaying)
        {
            objectToRemove.SetActive(false);
            enabled = false;
        }
    }
}
