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
    public AudioSource sfxSource;
    public List<AudioClip> voiceOvers;
    public int currClip;

    [Header("Tooltip")]
    public VRTK_ControllerTooltips leftTooltip;
    public VRTK_ControllerTooltips rightTooltip;
    public VRTK_ControllerTooltips.TooltipButtons triggerTooltip;
    public VRTK_ControllerTooltips.TooltipButtons gripTooltip;
    public VRTK_ControllerTooltips.TooltipButtons touchpadTooltip;
    public VRTK_ControllerTooltips.TooltipButtons menuTooltip;

    [Header("Objects")]
    public GameObject canvas;
    public GameObject desk;
    public GameObject key;
    public GameObject chest;
    public GameObject cabinet;
    public GameObject buttonWrap;

    // Use this for initialization
    void Start () {

        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }

        currClip = -1;
        StartCoroutine("startTutorial");
    }

    void PlayClip()
    {
        currClip++;
        voiceSource.clip = voiceOvers[currClip];
        voiceSource.Play();
    }

    IEnumerator startTutorial()
    {
        SteamVR_Fade.View(Color.black, 0);
        yield return new WaitForSeconds(1);

        canvas.SetActive(false);
        PlayClip(); // 01

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        
        SteamVR_Fade.View(Color.black, 0);
        SteamVR_Fade.View(Color.clear, 1);

        yield return new WaitForSeconds(2);
        LearnWalk();
    }

    public void LearnWalk()
    {
        Debug.Log("Called LearnWalk");
        PlayClip(); // 02
    }

    public void LearnTeleport()
    {
        Debug.Log("Called LearnTeleport");
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
        Debug.Log("Triggered Teleport");
        if (currClip == 2 && !voiceSource.isPlaying)
        {
            StartCoroutine("learnGrab");
        }
    }

    IEnumerator learnGrab()
    {
        PlayClip(); // 04
        desk.SetActive(true);
        key.SetActive(true);

        yield return new WaitForSeconds(voiceOvers[currClip].length + 1);

        PlayClip(); // 05

        leftTooltip.UpdateText(touchpadTooltip, "");
        leftTooltip.UpdateText(triggerTooltip, "Grab");
        rightTooltip.UpdateText(triggerTooltip, "Grab");
        
    }

    public void LearnUse ()
    {
        if (currClip == 4)
        {
            PlayClip(); // 06
            chest.SetActive(true);
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

        yield return new WaitForSeconds(5);
        // chest.SetActive(false);

        yield return new WaitForSeconds(1);
        cabinet.SetActive(true);
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

        yield return new WaitForSeconds(6);

        // cabinet.SetActive(false);
        buttonWrap.SetActive(true);
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

}
