using UnityEngine;

// A LED node emitting light based on start/stop signals from the previous node in the chain.
public class LEDNode : MonoBehaviour
{
    public LEDNode prevNode = null;
    public bool nextStart { get; private set; } = false;
    public bool isFirstNode = false;
    public bool isPointLightEn = false;
    public bool slowProgress = false;

    private Light pointLight = null;
    private Renderer rend = null;
    private Material mat;

    private float intensity = 0;
    private float minIntensity = 0;
    private float maxIntensity = 1.0f;
    private float onTime = 0;
    private float maxOnTime = 1.0f;
    private float onTimerSpeed = 2.0f;
    private float offTime = 0;
    private float maxOffTime = 20.0f;
    private float offTimerSpeed = 20.0f;

    private enum LightState { INCR, DECR, IDLE }
    private LightState lightState = LightState.IDLE;

    void Start()
    {
        // Get components safely
        pointLight = GetComponentInChildren<Light>(); // Ako je child objekat
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            mat = rend.sharedMaterial;
            mat.EnableKeyword("_EMISSION"); // Omogući Emission ako je URP/HDRP
        }
    }

    void Update()
    {
        ResolveNodeState();
        UpdateColor();
    }

    private void ResolveNodeState()
    {
        switch (lightState)
        {
            case LightState.INCR:
                IntensityIncrease();
                break;
            case LightState.DECR:
                IntensityDecrease();
                break;
            case LightState.IDLE:
                LightIdle();
                break;
        }
    }

    private void IntensityIncrease()
    {
        if (!slowProgress && !nextStart)
            nextStart = true;

        intensity = maxIntensity;

        if (isPointLightEn && pointLight != null)
            pointLight.intensity = maxIntensity; // Bolja praksa za URP/HDRP

        if (onTime < maxOnTime)
            onTime += onTimerSpeed * Time.deltaTime;
        else
        {
            onTime = 0;
            lightState = LightState.DECR;
        }
    }

    private void IntensityDecrease()
    {
        intensity = minIntensity;

        if (isPointLightEn && pointLight != null)
            pointLight.intensity = 0; // Postavi na 0 umesto enabled=false

        if (slowProgress && !nextStart)
            nextStart = true;
        else
            nextStart = false;

        lightState = LightState.IDLE;
    }

    private void LightIdle()
    {
        if (slowProgress && nextStart)
            nextStart = false;

        if (prevNode != null && prevNode.nextStart)
        {
            lightState = LightState.INCR;
        }
        else if (isFirstNode)
        {
            if (offTime < maxOffTime)
                offTime += offTimerSpeed * Time.deltaTime;
            else
            {
                offTime = 0;
                lightState = LightState.INCR;
                nextStart = true;
            }
        }
    }

    private void UpdateColor()
    {
        if (mat != null)
        {
            Color baseColor = mat.color;
            Color finalColor = baseColor * Mathf.LinearToGammaSpace(intensity);
            mat.SetColor("_EmissionColor", finalColor);
        }
    }
}
