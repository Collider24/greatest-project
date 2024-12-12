using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;

public class EnterInCar : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean enterButton;
    [SerializeField]
    private SteamVR_Action_Boolean exitButton;
    [SerializeField]
    private SteamVR_ActionSet carSet;
    [SerializeField]
    private SteamVR_ActionSet defaultSet;
    [SerializeField]
    private Transform seat;
    [SerializeField]
    private Transform exitPoint;

    private bool enterButtonPressed = false;
    private bool inDrive = false;

    private Transform player;
    [SerializeField]
    private TeleportManager teleportManager;

    private void FixedUpdate()
    {
        if (!inDrive) { return; }
        if (enterButton.GetStateDown(SteamVR_Input_Sources.RightHand) && !enterButtonPressed)
        {
            enterButtonPressed = true;
            StartCoroutine(Unpress());
            player.parent = null;
            player.transform.position = exitPoint.position;
            player.transform.rotation = exitPoint.rotation;
            inDrive = false;
            teleportManager.enabled = true;
            carSet.Deactivate(SteamVR_Input_Sources.Any);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && enterButton.GetStateDown(SteamVR_Input_Sources.RightHand) && !enterButtonPressed)
        {
            enterButtonPressed = true;
            StartCoroutine(Unpress());
            player = other.transform.root;
            player.position = seat.position;
            player.transform.root.SetParent(seat.transform);
            player.localEulerAngles = Vector3.zero;
            inDrive = true;
            carSet.Activate(SteamVR_Input_Sources.Any);
            teleportManager.enabled = false;
        }
    }

    private IEnumerator Unpress()
    {
        yield return new WaitForSeconds(0.2f);
        enterButtonPressed = false;
    }
}
