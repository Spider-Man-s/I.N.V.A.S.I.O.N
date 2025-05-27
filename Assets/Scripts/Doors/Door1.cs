
using UnityEngine;

public class Door1 : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator doorAnimator;
    public string openTrigger = "OpenDoor";
    public string closeTrigger = "CloseDoor";
    public float triggerDistance = 2f;

    [Header("Player Reference")]
    public Transform playerTransform; // Povežite XR Rig ovdje

    private bool isPlayerNear = false;
    private bool isDoorOpen = false;

    void Start()
    {
        if (doorAnimator == null)
            doorAnimator = GetComponent<Animator>();

        // Ako nije postavljen player, pokušajte ga pronaći
        if (playerTransform == null)
        {
            GameObject xrRig = GameObject.FindGameObjectWithTag("Player");
            if (xrRig != null)
                playerTransform = xrRig.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool wasPlayerNear = isPlayerNear;
        isPlayerNear = distance <= triggerDistance;

        // Otvori vrata kada se player približi
        if (isPlayerNear && !wasPlayerNear && !isDoorOpen)
        {
            OpenDoor();
        }
        // Zatvori vrata kada se player udalji
        else if (!isPlayerNear && wasPlayerNear && isDoorOpen)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        doorAnimator.SetTrigger(openTrigger);
        isDoorOpen = true;
    }

    void CloseDoor()
    {
        doorAnimator.SetTrigger(closeTrigger);
        isDoorOpen = false;
    }

    // Vizualizacija u Scene view-u
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}