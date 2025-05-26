using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed;

    private bool hasArrived = false;
    public float rotationSpeed = 45f;
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;

    private Vector3 startPosOffset;

    public void Init(Vector3 target, float speed)
    {
        targetPosition = target;
        moveSpeed = speed;
        startPosOffset = transform.position;
    }

    void Update()
    {
        // Move UFO toward target
        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 move = direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position += move;


        }

        // Levitate (bobbing up and down)
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        Vector3 levitateOffset = new Vector3(0f, yOffset, 0f);
        transform.position = new Vector3(transform.position.x, startPosOffset.y + yOffset, transform.position.z);

        // Optional: rotate around Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
