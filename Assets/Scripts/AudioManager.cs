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

        triggerActions["Boss1"] = () => PlayMusic(0, true);
        triggerActions["Boss2"] = () => PlayMusic(1, true);
        triggerActions["Boss3"] = () => PlayMusic(2, true);
        triggerActions["Boss4"] = () => PlayMusic(3, true);
        triggerActions["BG1"] = () => PlayMusic(4, true);
        triggerActions["BG2"] = () => PlayMusic(5, true);
        triggerActions["BG3"] = () => PlayMusic(6, true);
        triggerActions["BG4"] = () => PlayMusic(7, true);
        triggerActions["Intro"] = () => PlayMusic(8, true);
        triggerActions["End"] = () => PlayMusic(9, true);

        triggerActions["CallAnswered"] = () => PlayVoiceLine(0, false);

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

    private void PlayMusic(int index, bool loop = false)
    {
        if (index >= 0 && index < musicClips.Count)
        {
            musicSource.clip = musicClips[index];
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music index out of range");
        }
    }

    private void PlayVoiceLine(int index, bool loop = false)
    {
        if (index >= 0 && index < voiceLineClips.Count)
        {
            voiceLineSource.clip = voiceLineClips[index];
            voiceLineSource.loop = loop;
            voiceLineSource.Play();
        }
        else
        {
            Debug.LogWarning("Voice line index out of range");
        }
    }
}
