using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);

        if (sound == null)
        {
            Debug.LogWarning($"Sound not found: {soundName}");
            return;
        }

        sound.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);

        if (sound != null)
        {
            sound.source.Stop();
        }
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.5f, 2f)] public float pitch = 1f;

    public bool loop;

    [HideInInspector] public AudioSource source;
}