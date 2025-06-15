using UnityEngine;
using BNG;
using UnityEngine.SceneManagement;
public class VibratePhone : MonoBehaviour
{
    public ControllerHand vibrationHand = ControllerHand.Left;
    public float vibrationDuration = 1.2f;
    public float vibrationAmplitude = 1f;
    public float vibrationFrequency = 0.1f;
    public float vibrationInterval = 1.9f;

    private bool isCalling = false;

    public GameObject buttonAC;
    public GameObject buttonDEC;

    public GameObject finger;
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
        AudioManager.Instance.SetTrigger("CallAnswered");
        buttonAC.SetActive(false);
        buttonDEC.SetActive(false);
        finger.SetActive(false);
    }

    public void DeclineCall()
    {
        StopPhoneCall();
        Debug.Log("Call declined. Ringing again...");
        Invoke(nameof(StartPhoneCall), 5f);

        //dodat scene transition
        SceneManager.LoadScene("QuitGame");
    }

    void StopPhoneCall()
    {
        CancelInvoke(nameof(PhoneVibrate));
        isCalling = false;
    }
}
