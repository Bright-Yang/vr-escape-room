using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Key : MonoBehaviour {

    public Rigidbody rb;
    public Util_Animate chest;
    public Tutorial tutorial;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Chest"))
        {
            gameObject.SetActive(false);
            chest.Trigger();
            tutorial.LearnDoor();
        }
    }

    private void onGrab()
    {
        rb.isKinematic = false;
    }

    private void onUngrab()
    {
        rb.isKinematic = true;
    }

}
