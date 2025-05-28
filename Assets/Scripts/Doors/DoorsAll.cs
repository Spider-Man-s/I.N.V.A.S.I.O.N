using UnityEngine;

public class DoorsAll : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator doorAnimator;
    public string openTrigger = "OpenDoor";
    public string closeTrigger = "CloseDoor";

    private int objectsInTrigger = 0;

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
}
