using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_CanvasToCamera : MonoBehaviour {

    public GameObject parent;
    public float heightAdj = 1.8f;

    private void Update()
    {
        gameObject.transform.position = new Vector3(parent.transform.position.x, heightAdj, parent.transform.position.z);
        gameObject.transform.rotation = new Quaternion(0, parent.transform.rotation.y, 0, parent.transform.rotation.w);
    }
}
