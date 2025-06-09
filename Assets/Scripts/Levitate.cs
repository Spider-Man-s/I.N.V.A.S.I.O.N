using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float rotationSpeed;
    public float floatAmplitude;
    public float floatFrequency;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
