using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMasterMVP : MonoBehaviour {

    public List<GameObject> marbles; // R, G, B, Y
    public Chest chest;

    private bool[] marbleStatus = { false, false, false, false };
    private bool marbleCorrect;
  
    public void OpenChest()
    {
        chest.Open();

        // Make Yellow Marble Accessable
        marbles[3].SetActive(true);
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

    public void MarbleExit (string slotName)
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
            Escape();
        }
    }

    private void Escape()
    {
        Debug.Log("You've escaped! Congrats!");
    }
}
