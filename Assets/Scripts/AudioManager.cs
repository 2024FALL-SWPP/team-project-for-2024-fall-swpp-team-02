using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private List<AudioClip> sfxClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        int index = musicClips.FindIndex(clip => clip.name == name);
        musicSource.clip = musicClips[index];
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        int index = sfxClips.FindIndex(clip => clip.name == name);
        sfxSource.PlayOneShot(sfxClips[index]);
    }
}