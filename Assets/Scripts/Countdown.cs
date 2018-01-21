using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public Text countdownText;
    private float totalSec;

    public void startCountdown(float sec)
    {
        totalSec = sec;
    }
    
    public void updateText()
    {
        float remainTime = totalSec - Time.timeSinceLevelLoad;
        if (remainTime < 0)
        {
            remainTime = 0;
        }

        float remainMin = Mathf.Floor(remainTime / 60);
        float remainSec = Mathf.Floor(remainTime - remainMin * 60);
        string countdownString = remainMin.ToString("00") + ":" + remainSec.ToString("00");

        countdownText.text = countdownString;
    }

}
