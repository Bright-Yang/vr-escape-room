using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public Animator anim;
    private int openHash = Animator.StringToHash("Open");

    public void Open()
    {
        anim.SetTrigger(openHash);
    }

}
