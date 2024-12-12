using UnityEngine;
using Valve.VR;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject grenade;
    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private SteamVR_Action_Boolean spawnButton;
    [SerializeField]
    private SteamVR_Action_Vector2 moveCube;
    [SerializeField]
    private SteamVR_Action_Single speedBtn;

    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Transform VRCamera;

    private void Update()
    {
        if (spawnButton.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Instantiate(grenade, spawnPoint.transform.position, Quaternion.identity);
        }
        Vector2 move = moveCube.GetAxis(SteamVR_Input_Sources.LeftHand);
        Vector3 fwd = new Vector3(VRCamera.forward.x, 0, VRCamera.forward.z);
        fwd = fwd.normalized;
        Vector3 right = new Vector3(VRCamera.right.x, 0, VRCamera.right.z);
        right = right.normalized;
        cube.transform.position += (right * move.x + fwd * move.y) * speedBtn.GetAxis(SteamVR_Input_Sources.LeftHand) / 5;
    }
}
