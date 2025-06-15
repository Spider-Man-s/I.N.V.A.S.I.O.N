using UnityEngine;
using BNG;

public class VibratePhone : MonoBehaviour
{
    public ControllerHand vibrationHand = ControllerHand.Left;
    public float vibrationDuration = 0.2f;
    public float vibrationAmplitude = 0.5f;
    public float vibrationFrequency = 0.5f;
    public float vibrationInterval = 1f;

    private bool isCalling = false;

    void Start()
    {
        StartPhoneCall();
    }

    public void StartPhoneCall()
    {
        // ui set active
        if (isCalling) return;

        isCalling = true;
        InvokeRepeating(nameof(PhoneVibrate), 0f, vibrationInterval);
        Debug.Log("Army is calling...");
    }

    void PhoneVibrate()
    {
        InputBridge.Instance.VibrateController(
            vibrationAmplitude,
            vibrationDuration,
            vibrationFrequency,
            vibrationHand
        );
    }

    public void AcceptCall()
    {
        StopPhoneCall();
        Debug.Log("Call accepted.");
    }

    public void DeclineCall()
    {
        StopPhoneCall();
        Debug.Log("Call declined. Ringing again...");
        Invoke(nameof(StartPhoneCall), 5f);
    }

    void StopPhoneCall()
    {
        CancelInvoke(nameof(PhoneVibrate));
        isCalling = false;
    }
}
