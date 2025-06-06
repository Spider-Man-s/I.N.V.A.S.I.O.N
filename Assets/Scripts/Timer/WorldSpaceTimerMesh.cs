using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldSpaceTimerMesh : MonoBehaviour
{
    public float startTimeInSeconds = 1200f;
    private float currentTime;
    private TextMeshPro textMesh;

    public AudioSource explosionSound;
    public Image whiteFlashPanel; 
    private bool hasExploded = false;

    public CameraShake cameraShake;

    void Start()
    {
        currentTime = startTimeInSeconds;
        textMesh = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
                currentTime = 0; 

            UpdateTimerDisplay(currentTime);
        }
        else if (!hasExploded)
        {
            hasExploded = true;
            currentTime = 0; 
            UpdateTimerDisplay(currentTime);
            TriggerExplosionEffect();
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        textMesh.text = $"{minutes:00}:{seconds:00}";

        if (time > 900f) textMesh.color = Color.green;
        else if (time > 600f) textMesh.color = new Color(1f, 0.6f, 0f);
        else if (time > 300f) textMesh.color = new Color(1f, 0.3f, 0f);
        else textMesh.color = Color.red;
    }

    void TriggerExplosionEffect()
    {
        if (explosionSound != null)
        {
            explosionSound.Play();
        }
        if (whiteFlashPanel != null)
        {
            StartCoroutine(FadeToWhite());
        }
        if (cameraShake != null)
        {
            cameraShake.TriggerShake(1f, 0.05f); 
        }

    }

    System.Collections.IEnumerator FadeToWhite()
    {
        float duration = 2f;
        float time = 0f;
        Color startColor = new Color(1, 1, 1, 0);
        Color endColor = new Color(1, 1, 1, 1);

        while (time < duration)
        {
            time += Time.deltaTime;
            whiteFlashPanel.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("QuitGame");
    }
}
