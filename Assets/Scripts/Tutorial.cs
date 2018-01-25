using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VRTK;

public class Tutorial : MonoBehaviour {

    [Header("Audio")]
    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource voiceSource;
    public List<AudioClip> voiceOvers;
    public int currClip;

    [Header("SFX")]
    public AudioSource keySource;
    public AudioSource buttonWrapSource;
    public AudioClip sfxNotification;

    [Header("Tooltip")]
    public VRTK_ControllerTooltips leftTooltip;
    public VRTK_ControllerTooltips rightTooltip;
    public VRTK_ControllerTooltips.TooltipButtons triggerTooltip;
    public VRTK_ControllerTooltips.TooltipButtons gripTooltip;
    public VRTK_ControllerTooltips.TooltipButtons touchpadTooltip;
    public VRTK_ControllerTooltips.TooltipButtons menuTooltip;

    [Header("Countdown")]
    public float totalMinutes;
    private float totalSec;
    public GameObject canvas;
    public Countdown countdown;

    // Use this for initialization
    void Start () {

        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }

        
        currClip = -1;
        StartCoroutine("startTutorial");

        totalSec = totalMinutes * 60;
        countdown.startCountdown(totalSec);
    }

    void PlayClip()
    {
        currClip++;
        voiceSource.clip = voiceOvers[currClip];
        voiceSource.Play();
    }

    IEnumerator startTutorial()
    {
        canvas.SetActive(false);
        yield return new WaitForSeconds(1);
        
        PlayClip(); // 01

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);
        LearnWalk();
    }

    public void LearnWalk()
    {
        PlayClip(); // 02
    }

    public void LearnTeleport()
    {
        if (currClip == 1 && !voiceSource.isPlaying)
        {
            PlayClip(); // 03
            leftTooltip.UpdateText(gripTooltip, "");
            rightTooltip.UpdateText(gripTooltip, "");
            leftTooltip.UpdateText(touchpadTooltip, "Teleport");
        }
    }

    public void LearnGrab()
    {
        if (currClip == 2 && !voiceSource.isPlaying)
        {
            StartCoroutine("learnGrab");
        }
    }

    IEnumerator learnGrab()
    {
        PlayClip(); // 04

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        PlayClip(); // 05

        leftTooltip.UpdateText(touchpadTooltip, "");
        leftTooltip.UpdateText(triggerTooltip, "Grab");
        rightTooltip.UpdateText(triggerTooltip, "Grab");

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        keySource.clip = sfxNotification;
        keySource.Play();
    }

    public void LearnUse ()
    {
        if (currClip == 4)
        {
            PlayClip(); // 06
        }
    }

    public void LearnDoor ()
    {
        if (currClip == 5)
        {
            StartCoroutine("learnDoor");
        }
    }

    IEnumerator learnDoor ()
    {
        yield return new WaitForSeconds(1);
        PlayClip(); // 07
    }

    public void LearnButton ()
    {
        if (currClip == 6)
        {
            StartCoroutine("learnButton");
        }
    }

    IEnumerator learnButton()
    {
        PlayClip(); // 08

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        PlayClip(); // 09
        leftTooltip.UpdateText(triggerTooltip, "");
        rightTooltip.UpdateText(triggerTooltip, "");
        rightTooltip.UpdateText(touchpadTooltip, "Interact");

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        buttonWrapSource.clip = sfxNotification;
        buttonWrapSource.Play();
    }

    public void LearnMenu ()
    {
        if (currClip == 8)
        {
            PlayClip(); // 10
            rightTooltip.UpdateText(touchpadTooltip, "");
            leftTooltip.UpdateText(menuTooltip, "Menu");
            rightTooltip.UpdateText(menuTooltip, "Menu");
        }
    }

    public void EndTutorial()
    {
        if (currClip == 9)
        {
            StartCoroutine("endTutorial");
        }
    }

    IEnumerator endTutorial()
    {
        yield return new WaitForSeconds(0.5f);

        PlayClip(); // 11
        leftTooltip.UpdateText(gripTooltip, "Walk");
        rightTooltip.UpdateText(gripTooltip, "Walk");
        leftTooltip.UpdateText(triggerTooltip, "Grab");
        rightTooltip.UpdateText(triggerTooltip, "Grab");
        leftTooltip.UpdateText(touchpadTooltip, "Teleport");
        rightTooltip.UpdateText(touchpadTooltip, "Interact");

        yield return new WaitForSeconds(voiceOvers[currClip].length);

        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, 1);
        PlayClip(); // 12 aka Game Start

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);
        SteamVR_LoadLevel.Begin("Puzzle");
    }

    public void ToggleCanvas()
    {
        if (canvas.activeSelf)
        {
            canvas.SetActive(false);
            EndTutorial();
        } else
        {
            canvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (canvas.activeSelf)
        {
            countdown.updateText();
        }
    }

}
