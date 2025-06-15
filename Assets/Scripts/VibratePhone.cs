using UnityEngine;
using BNG;
using UnityEngine.SceneManagement;
public class VibratePhone : MonoBehaviour
{
    public ControllerHand vibrationHand = ControllerHand.Left;
    public float vibrationDuration;
    public float vibrationAmplitude;
    public float vibrationFrequency;
    public float vibrationInterval;

    private bool isCalling = false;

    public GameObject buttonAC;
    public GameObject buttonDEC;
    public GameObject gameText;
    public GameObject finger;
    void Start()
    {
        StartPhoneCall();
    }

    public void StartPhoneCall()
    {
        if (isCalling) return;

        isCalling = true;
        InvokeRepeating(nameof(PhoneVibrate), 0f, vibrationInterval);
        Debug.Log("Army is calling...");
    }

    void PhoneVibrate()
    {
        if (InputBridge.Instance == null)
        {
            Debug.LogWarning("InputBridge.Instance is null!");
            return;
        }

        Debug.Log("Vibrating controller...");
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
        gameText.SetActive(false);
    }

    public void DeclineCall()
    {

        SceneManager.LoadScene("QuitGame");
    }

    void StopPhoneCall()
    {
        CancelInvoke(nameof(PhoneVibrate));
        isCalling = false;
    }
}
