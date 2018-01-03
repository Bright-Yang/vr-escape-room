using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Controller : MonoBehaviour {

    public Tutorial tutorial;

    private SteamVR_TrackedObject trackObj;
    private SteamVR_Controller.Device device;
    private bool isLeft;

    // Use this for initialization
    void Start () {
        trackObj = GetComponent<SteamVR_TrackedObject>();

        if (trackObj.name == "Controller (left)")
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }

    }

    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)trackObj.index);

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && tutorial.currClip == 2)
        {
            tutorial.LearnTeleport();
        }

        if (isLeft && device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && tutorial.currClip == 3)
        {
            tutorial.LearnGrab();
        }
    }
}
