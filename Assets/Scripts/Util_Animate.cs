using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_Animate : MonoBehaviour {

    public Animator anim;
    public AudioSource audioSource;
    private int openHash = Animator.StringToHash("Animate");

    public void Trigger()
    {
        anim.SetTrigger(openHash);
        audioSource.Play();
    }
}
