using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music Sources")]
    public AudioSource musicSource;
    public List<AudioClip> musicClips;

    [Header("Voice Lines")]
    public AudioSource voiceLineSource;
    public List<AudioClip> voiceLineClips;

    private Dictionary<string, System.Action> triggerActions = new();

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeTriggers();
    }

    private void InitializeTriggers()
    {

        //  triggerActions["Boss1Dead"] = () => PlayMusic(1);
        //  triggerActions["Boss2Dead"] = () => PlayMusic(2);
        //  triggerActions["Boss3Dead"] = () => PlayMusic(3);
        // triggerActions["Boss4Dead"] = () => PlayMusic(4);

        triggerActions["CallAnswered"] = () => PlayVoiceLine(0);
        // triggerActions["VictoryVoice"] = () => PlayVoiceLine(1);
    }

    public void SetTrigger(string triggerName)
    {
        if (triggerActions.TryGetValue(triggerName, out var action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning("No action found for trigger: " + triggerName);
        }
    }

    private void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Count)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music index out of range");
        }
    }

    private void PlayVoiceLine(int index)
    {
        if (index >= 0 && index < voiceLineClips.Count)
        {
            voiceLineSource.clip = voiceLineClips[index];
            voiceLineSource.Play();
        }
        else
        {
            Debug.LogWarning("Voice line index out of range");
        }
    }
}
