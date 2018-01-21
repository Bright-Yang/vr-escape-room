using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Welcome : MonoBehaviour {

    [Header("Audio")]
    public AudioMixer audioMixer;
    public AudioMixerSnapshot bgmOn;
    public AudioMixerSnapshot bgmOff;
    public AudioMixerSnapshot masterOff;
    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource bgm;
    public AudioSource story;
    public float storyLength;

    [Header("Canvas")]
    public GameObject welcomeCanvas;
    public GameObject skipStoryCanvas;
    public GameObject skipTutorialCanvas;

    void Start()
    {
        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }
    }

    public void StartGame()
    {
        welcomeCanvas.SetActive(false);
        skipStoryCanvas.SetActive(true);
    }

    public void SkipStory()
    {
        skipStoryCanvas.SetActive(false);
        skipTutorialCanvas.SetActive(true);
    }

    public void LoadStory()
    {
        StartCoroutine("startStory");
    }

    IEnumerator startStory()
    {
        bgmOff.TransitionTo(1);
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, 1);

        yield return new WaitForSeconds(2);
        bgm.Pause();
        story.Play();

        yield return new WaitForSeconds(storyLength + 2);
        masterOff.TransitionTo(1);

        yield return new WaitForSeconds(1);
        SkipStory();
        bgmOn.TransitionTo(1);
        bgm.Play();
        story.Pause();
        SteamVR_Fade.View(Color.black, 0);
        SteamVR_Fade.View(Color.clear, 1);
    }

    public void LoadTutorial()
    {
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, 1);
        SteamVR_LoadLevel.Begin("Tutorial");
    }

    public void LoadGame()
    {
        SteamVR_LoadLevel.Begin("Puzzle");
    }
}
