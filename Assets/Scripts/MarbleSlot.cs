using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSlot : MonoBehaviour {

    public Puzzle puzzle;
    private float marbleZ = 0.14f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Marble"))
        {
            col.transform.SetParent(null);
            col.GetComponent<Rigidbody>().isKinematic = false;
            col.transform.position = gameObject.transform.position;
            col.transform.rotation = gameObject.transform.rotation;
            puzzle.MarbleEnter(col.name, gameObject.name);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Marble"))
        {
            puzzle.MarbleExit(gameObject.name);
        }
    }
}
