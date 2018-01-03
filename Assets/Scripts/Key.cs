using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Util_Animate chest;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Chest"))
        {
            gameObject.SetActive(false);
            chest.Trigger();
        }
    }
}
