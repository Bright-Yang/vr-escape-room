using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Welcome : MonoBehaviour {

    [Header("Audio")]
    public AudioMixer audioMixer;
    public AudioMixerSnapshot BgmOn;
    public AudioMixerSnapshot BgmOff;
    public AudioMixerSnapshot masterOff;
    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource bgm;
    public AudioSource story;

    void Start()
    {
        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }
    }

    public void LoadStory()
    {
        StartCoroutine("startStory");
    }

    IEnumerator startStory()
    {
        BgmOff.TransitionTo(1);
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, 1);

        yield return new WaitForSeconds(2);
        bgm.Pause();
        story.Play();

        yield return new WaitForSeconds(70);
        masterOff.TransitionTo(3);

        yield return new WaitForSeconds(3);
        SteamVR_LoadLevel.Begin("Tutorial");
    }
}
