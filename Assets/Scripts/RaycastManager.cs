using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rayOrigin;
    [SerializeField]
    private Material redMat;
    [SerializeField]
    private Material initMat;
    private void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin.transform.position, rayOrigin.transform.forward, 10);
        foreach (RaycastHit hit in hits) 
        {
            hit.collider.gameObject.GetComponent<MeshRenderer>().material = redMat;
            Debug.DrawLine(rayOrigin.transform.position, hit.point, Color.red);
        }
    }
}
