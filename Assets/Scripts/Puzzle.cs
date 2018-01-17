using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

    [Header("Audio")]
    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource sfxSource;
    public AudioClip sfxWin;
    public AudioClip sfxTimeup;

    [Header("Marbles")]
    public List<GameObject> marbles; // R, G, B, Y
    public MeshRenderer marblesClueFrame;
    public Texture marblesClue;

    [Header("UI")]
    public float totalMinutes;
    public GameObject canvas;
    public Text countdown;

    private float totalSec;
    private bool[] marbleStatus = { false, false, false, false };
    private bool marbleCorrect;

    // Use this for initialization
    void Start () {

        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }

        totalSec = totalMinutes * 60;
        UpdateCountdown();
    }

    public void ShowMarblesClue()
    {
        marblesClueFrame.materials[1].SetTexture("_MainTex", marblesClue);
    }

    public void MarbleEnter(string marbleName, string slotName)
    {

        if (marbleName == "MarbleRed" && slotName == "SlotRed")
        {
            marbleStatus[0] = true;
        }
        else if (marbleName == "MarbleGreen" && slotName == "SlotGreen")
        {
            marbleStatus[1] = true;
        }
        else if (marbleName == "MarbleBlue" && slotName == "SlotBlue")
        {
            marbleStatus[2] = true;
        }
        else if (marbleName == "MarbleYellow" && slotName == "SlotYellow")
        {
            marbleStatus[3] = true;
        }

        CheckPuzzle();
    }

    public void MarbleExit(string slotName)
    {
        if (slotName == "SlotRed")
        {
            marbleStatus[0] = false;
        }
        else if (slotName == "SlotGreen")
        {
            marbleStatus[1] = false;
        }
        else if (slotName == "SlotBlue")
        {
            marbleStatus[2] = false;
        }
        else if (slotName == "SlotYellow")
        {
            marbleStatus[3] = false;
        }
    }

    public void CheckPuzzle()
    {
        // Check Marbles

        marbleCorrect = true;

        for (int i = 0; i < marbleStatus.Length; i++)
        {
            if (marbleStatus[i] == false)
            {
                marbleCorrect = false;
            }
        }

        // Check Player

        // If both are true, escape!

        if (marbleCorrect == true)
        {
            StartCoroutine("escape");
        }
    }

    IEnumerator escape()
    {
        sfxSource.clip = sfxWin;
        sfxSource.Play();
        yield return new WaitForSeconds(sfxSource.clip.length);
        LoadWelcome();
    }

    public void LoadWelcome()
    {
        SteamVR_LoadLevel.Begin("Welcome");
    }

    public void LoadTutorial()
    {
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

    private void UpdateCountdown()
    {
        float remainTime = totalSec - Time.timeSinceLevelLoad;
        float remainMin = Mathf.Floor(remainTime / 60);
        float remainSec = Mathf.Floor(remainTime - remainMin * 60);
        string countdownString = remainMin.ToString("00") + ":" + remainSec.ToString("00");
        countdown.text = countdownString;
    }

    private void Update()
    {
        if (canvas.activeSelf)
        {
            UpdateCountdown();
        }
        if (Time.timeSinceLevelLoad > totalSec)
        {
            StartCoroutine("timeup");
        }
    }

    IEnumerator timeup()
    {
        sfxSource.clip = sfxTimeup;
        sfxSource.Play();
        yield return new WaitForSeconds(sfxSource.clip.length);
        LoadWelcome();
    }

}
