using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public GameObject ears;
    public List<GameObject> audioSources;
    public AudioSource sfxSource;

    public AudioClip win;

    public List<GameObject> marbles; // R, G, B, Y
    public MeshRenderer marblesClueFrame;
    public Texture marblesClue;

    private bool[] marbleStatus = { false, false, false, false };
    private bool marbleCorrect;
    

    // Use this for initialization
    void Start () {

        foreach (GameObject audioSource in audioSources)
        {
            audioSource.transform.SetParent(ears.transform);
        }
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
        sfxSource.clip = win;
        sfxSource.Play();
        yield return new WaitForSeconds(sfxSource.clip.length);
        SteamVR_LoadLevel.Begin("Welcome");
    }

}
