using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private List<AudioClip> musicClips;

    private string currentSceneName;


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

    public void Start()
    {
        // Get all music assets in project
        musicClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds"));

        SceneManager.activeSceneChanged += SceneChange;

        PlaySceneMusic(SceneManager.GetActiveScene());

        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private enum SceneType
    {
        Main, Stage, Other
    };

    SceneType getSceneType(string sceneName)
    {
        if (sceneName.StartsWith("Level"))
        {
            if (sceneName == "LevelSelectScene")
                return SceneType.Main;
            if (sceneName == "LevelClearScene" || sceneName == "LevelOverScene")
                return SceneType.Other;
            return SceneType.Stage;
        }
        return SceneType.Main;
    }

    private void PlaySceneMusic(Scene scene)
    {
        switch (getSceneType(scene.name))
        {
            case SceneType.Main:
                musicSource.clip = musicClips.Find(clip => clip.name == "MainMusic");
                break;
            case SceneType.Stage:
                musicSource.clip = musicClips.Find(clip => clip.name.StartsWith(scene.name.Substring(0, scene.name.Length - 5)));
                break;
            case SceneType.Other:
                return;
        }
        musicSource.Play();
    }

    private void SceneChange(Scene _, Scene next)
    {
        if (musicSource.isPlaying && getSceneType(currentSceneName) == getSceneType(next.name))
        {
            currentSceneName = next.name;
            return;
        }

        musicSource.Stop();
        PlaySceneMusic(next);

        currentSceneName = next.name;
    }

    public void PlaySFX(string name)
    {
        var music = musicClips.Find(clip => clip.name == name);
        sfxSource.PlayOneShot(music);
    }

    // Coroutine to play sound effect
    public IEnumerator PlaySFXAfterCoroutine(string sfxName, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySFX(sfxName);
    }

    public void PlaySFXAfter(string sfxName, float delay)
    {
        StartCoroutine(PlaySFXAfterCoroutine(sfxName, delay));
    }
}