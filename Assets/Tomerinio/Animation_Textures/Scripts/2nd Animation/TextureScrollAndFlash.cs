//All Right Receved © TPN GAMES.
using System.Collections;
using UnityEngine;

public class TextureScrollAndFlash : MonoBehaviour
{
    public float flashDuration = 5.0f; // Duration of flashing
    public float flashInterval = 0.1f; // Time between each flash (0.1 seconds)
    private string nameMy = "TPNGAMES";
    private Renderer objectRenderer;
    private bool isFlashing = false;
    private Color originalColor;
    private Color flashColor = Color.red; // Color to flash

    void Start()
    {
        if (nameMy == "TPNGAMES")
        {
            objectRenderer = GetComponent<Renderer>();
            originalColor = objectRenderer.material.color;
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        while (true)
        {
            // Flash the texture
            yield return StartCoroutine(FlashTexture());

            // Optional: Add a pause between flash cycles
            // yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator FlashTexture()
    {
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            isFlashing = !isFlashing;
            objectRenderer.material.color = isFlashing ? flashColor : originalColor;
            elapsedTime += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }
        // Reset color after flashing
        objectRenderer.material.color = originalColor;
    }
}