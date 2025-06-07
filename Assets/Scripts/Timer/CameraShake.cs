using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalLocalPos;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 1.0f;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = originalLocalPos + new Vector3(shakeOffset.x, shakeOffset.y, 0f);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalLocalPos;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
