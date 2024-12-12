using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


[RequireComponent(typeof(LineRenderer))]
public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean teleport;

    private RaycastHit hit;
    private LineRenderer lineRenderer;

    [SerializeField]
    private LayerMask groundMask;

    private GameObject sphere;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(sphere.GetComponent<Collider>());
        sphere.transform.localScale = Vector3.one * 0.1f;
    }

    private Coroutine movement = null;
    private void Update()
    {
        if (teleport.GetState(SteamVR_Input_Sources.RightHand))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10, groundMask))
            {
                sphere.SetActive(true);
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
                sphere.transform.position = hit.point;
            }
            else
            {
                lineRenderer.enabled = false;
                sphere.SetActive(false);
            }
        }

        else if (teleport.GetStateUp(SteamVR_Input_Sources.RightHand) && movement == null)
        {
            movement = StartCoroutine(SmoothMove(transform.root.position, hit.point, 0.1f));
        }
        else
        {
            lineRenderer.enabled = false;
            sphere.SetActive(false);
        }
    }

    private IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float step)
    {
        float s = 0;
        Vector3 currentPos = startPos;
        while ((currentPos - endPos).magnitude > 0.01f)
        {
            s+=step;
            transform.root.position = Vector3.Lerp(startPos, endPos, s);
            currentPos = transform.root.position;
            yield return null;
        }
        movement = null;
    }
}
