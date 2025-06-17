using UnityEngine;

public class TriggerCaller : MonoBehaviour
{
    [Tooltip("Name of the trigger to call on AudioManager")]
    public string triggerName;


    public void CallTrigger()
    {
        if (!string.IsNullOrEmpty(triggerName))
        {
            AudioManager.Instance?.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Trigger name is not set on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CallTrigger();
            gameObject.SetActive(false);
        }
    }
}
