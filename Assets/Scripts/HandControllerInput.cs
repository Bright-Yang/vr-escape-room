using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackObj;
    private SteamVR_Controller.Device device;

    // Check Hand
    private bool isLeft;

    // Teleporter
    public int rayLength;
    public GameObject player;
    public GameObject aimerObj;
    public LineRenderer laser;
    public float aimerYAdjust; // specific to adjust teleportAimer y position
    public LayerMask laserMask;
    private Vector3 teleportLocation;

    // Walking
    public Transform playerCam;
    public float moveSpeed;
    private Vector3 movementDirection;

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

        laser = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackObj.index);

        // Walk

        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            movementDirection = playerCam.transform.forward;
            movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
            movementDirection *= moveSpeed * Time.deltaTime;
            player.transform.position += movementDirection;
        }

        // Teleportation w/ Left Touchpad
        if (isLeft)
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(0, gameObject.transform.position);

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, laserMask))
                {
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                    aimerObj.SetActive(true);
                    aimerObj.transform.position = teleportLocation + new Vector3(0, aimerYAdjust, 0);
                }
                else
                {
                    teleportLocation = transform.forward * rayLength + transform.position;
                    laser.SetPosition(1, teleportLocation);
                    RaycastHit groundRay;
                    if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                    {
                        teleportLocation = new Vector3(
                            transform.forward.x * 5 + transform.position.x,
                            groundRay.point.y,
                            transform.forward.z * rayLength + transform.position.z);
                        aimerObj.SetActive(true);
                        aimerObj.transform.position = teleportLocation + new Vector3(0, aimerYAdjust, 0);
                    }
                    else
                    {
                        teleportLocation = player.transform.position;
                    }
                }
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(false);
                aimerObj.SetActive(false);
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                player.transform.position = teleportLocation;

            }
        }

    }

    // Grab & Release Obj
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Grab") || col.gameObject.CompareTag("Marble"))
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ReleaseObject(col);
            }
        }
    }

    private void GrabObject(Collider coli)
    {
        coli.transform.SetParent(gameObject.transform);
        coli.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
    }

    private void ReleaseObject(Collider coli)
    {
        coli.transform.SetParent(null);
        Rigidbody rigidbody = coli.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = device.velocity;
        rigidbody.angularVelocity = device.angularVelocity;
    }

}