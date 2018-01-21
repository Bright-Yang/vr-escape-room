using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

    [Header("Audio")]
    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource ttsSource;
    public AudioClip ttsWin;
    public AudioClip ttsTimeup;

    [Header("Portal")]
    public AudioSource portalSfxSource;
    public AudioClip sfxPortalRight;
    public AudioClip sfxPortalWrong;

    [Header("Door")]
    public AudioSource doorSfxSource;
    public AudioClip sfxDoorOpen;

    [Header("Marbles")]
    public List<GameObject> marbles; // R, G, B, Y
    public MeshRenderer marblesClueFrame;
    public Texture marblesClue;

    [Header("UI")]
    public float totalMinutes;
    public GameObject canvas;
    public Countdown countdown;
    private bool timeupInProcess = false;

    private float totalSec;
    private bool[] marbleFull = { false, false, false, false }; // If there's a marble in the slot
    private bool[] marbleStatus = { false, false, false, false }; // If it's the correct marble
    private bool marbleFilled;
    private bool marbleCorrect;

    // Use this for initialization
    void Start () {

        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }

        totalSec = totalMinutes * 60;
        countdown.startCountdown(totalSec);
    }

    public void ShowMarblesClue()
    {
        marblesClueFrame.materials[1].SetTexture("_MainTex", marblesClue);
    }

    public void MarbleEnter(string marbleName, string slotName)
    {

        if (slotName == "SlotRed")
        {
            marbleFull[0] = true;
            if (marbleName == "MarbleRed")
            {
                marbleStatus[0] = true;
            }
        }
        else if (slotName == "SlotGreen")
        {
            marbleFull[1] = true;
            if (marbleName == "MarbleGreen")
            {
                marbleStatus[1] = true;
            }
        }
        else if (slotName == "SlotBlue")
        {
            marbleFull[2] = true;
            if (marbleName == "MarbleBlue")
            {
                marbleStatus[2] = true;
            }
        }
        else if (slotName == "SlotYellow")
        {
            marbleFull[3] = true;
            if (marbleName == "MarbleYellow")
            {
                marbleStatus[3] = true;
            }
        }

        CheckPuzzle();
    }

    public void MarbleExit(string slotName)
    {
        if (slotName == "SlotRed")
        {
            marbleFull[0] = false;
            marbleStatus[0] = false;
        }
        else if (slotName == "SlotGreen")
        {
            marbleFull[1] = false;
            marbleStatus[1] = false;
        }
        else if (slotName == "SlotBlue")
        {
            marbleFull[2] = false;
            marbleStatus[2] = false;
        }
        else if (slotName == "SlotYellow")
        {
            marbleFull[3] = false;
            marbleStatus[3] = false;
        }
    }

    public void CheckPuzzle()
    {
        // Check if Marbles are filled

        marbleFilled = true;

        for (int i = 0; i < marbleFull.Length; i++)
        {
            if (marbleFull[i] == false)
            {
                marbleFilled = false;
            }
        }

        // Check if Marbles are correct

        marbleCorrect = true;

        for (int i = 0; i < marbleStatus.Length; i++)
        {
            if (marbleStatus[i] == false)
            {
                marbleCorrect = false;
            }
        }

        // If both are true, escape!

        if (marbleCorrect == true)
        {
            StartCoroutine("escape");
        } else if (marbleFilled == true) {
            portalSfxSource.clip = sfxPortalWrong;
            portalSfxSource.Play();
        }
    }

    IEnumerator escape()
    {
        portalSfxSource.clip = sfxPortalRight;
        portalSfxSource.Play();
        yield return new WaitForSeconds(portalSfxSource.clip.length);
        ttsSource.clip = ttsWin;
        ttsSource.Play();
        yield return new WaitForSeconds(ttsSource.clip.length);
        LoadWelcome();
    }

    public void LoadWelcome()
    {
        SteamVR_LoadLevel.Begin("Welcome");
    }

    public void LoadTutorial()
    {
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, 1);
        SteamVR_LoadLevel.Begin("Tutorial");
    }

    public void ToggleCanvas()
    {

        if (canvas.activeSelf)
        {
            canvas.SetActive(false);
        }
        else
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
        if (Time.timeSinceLevelLoad > totalSec && !timeupInProcess)
        {
            timeupInProcess = true;
            StartCoroutine("timeup");
        }
    }

    IEnumerator timeup()
    {
        doorSfxSource.clip = sfxDoorOpen;
        doorSfxSource.Play();
        yield return new WaitForSeconds(doorSfxSource.clip.length);
        ttsSource.clip = ttsTimeup;
        ttsSource.Play();
        yield return new WaitForSeconds(ttsSource.clip.length);
        LoadWelcome();
    }

}
