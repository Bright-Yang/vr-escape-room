using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_CanvasToCamera : MonoBehaviour {

    public GameObject parent;

	// Use this for initialization
	void Start () {
        gameObject.transform.SetParent(parent.transform);
	}
}
