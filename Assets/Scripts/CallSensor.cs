using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CallSensor : MonoBehaviour
{
    [Tooltip("Check to trigger AcceptCall, uncheck to trigger DeclineCall")]
    public bool isAcceptButton = true;

    [Tooltip("Reference to the VibratePhone script managing the call")]
    public VibratePhone phoneScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger") && phoneScript != null)
        {
            if (isAcceptButton)
            {
                phoneScript.AcceptCall();
            }
            else
            {
                phoneScript.DeclineCall();
            }
        }
    }
}
