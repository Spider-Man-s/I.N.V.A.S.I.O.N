using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Key4Win : MonoBehaviour
{
    public AudioSource winnerSound;
    public Image pinkFlashPanel;
    public CameraShake cameraShake;

    public void Win()
    {
        GameStats.win = true;
        winnerSound.Play();
        StartCoroutine(FadeToPink());
        cameraShake.TriggerShake(1f, 0.05f);
    }

    IEnumerator FadeToPink()
    {
        float duration = 1f; 
        float time = 0f;

        Color startColor = new Color(0, 0.8f, 1, 0);        
        Color endColor = new Color(0, 0.8f, 1, 0.8f);       

        while (time < duration)
        {
            time += Time.deltaTime;
            pinkFlashPanel.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("QuitGame");
    }
}