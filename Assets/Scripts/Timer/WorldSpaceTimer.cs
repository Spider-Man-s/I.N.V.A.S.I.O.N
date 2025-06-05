using TMPro;
using UnityEngine;

public class WorldSpaceTimer : MonoBehaviour
{
    public float startTimeInSeconds = 1200f; // 20:00
    private float currentTime;
    private TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = startTimeInSeconds;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay(currentTime);
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // Boja prema vremenu
        if (time > 900f) // 15:01+
        {
            timerText.color = Color.green;
        }
        else if (time > 600f) // 10:01 - 15:00
        {
            timerText.color = new Color(1f, 0.6f, 0f); // svjetlija narančasta
        }
        else if (time > 300f) // 05:01 - 10:00
        {
            timerText.color = new Color(1f, 0.3f, 0f); // tamnija narančasta
        }
        else // 00:00 - 05:00
        {
            timerText.color = Color.red;
        }
    }
}
