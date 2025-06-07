using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        Debug.Log("Application quit requested");
    }

    public void ResetApplication()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
