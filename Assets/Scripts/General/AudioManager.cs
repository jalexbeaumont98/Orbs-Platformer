using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Library")]
    public List<SoundData> sounds = new List<SoundData>();

    private AudioSource source;
    private Dictionary<string, AudioClip> soundLookup;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();

        // Build quick lookup dictionary for performance
        soundLookup = new Dictionary<string, AudioClip>();
        foreach (SoundData s in sounds)
        {
            if (!soundLookup.ContainsKey(s.soundName))
                soundLookup.Add(s.soundName, s.clip);
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundLookup.TryGetValue(soundName, out AudioClip clip))
        {
            source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"AudioManager: Sound '{soundName}' not found!");
        }
    }
}
