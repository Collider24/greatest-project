using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fixedCube;
    [SerializeField]
    private GameObject fixedSphere;
    [SerializeField]
    private GameObject springCube;
    [SerializeField]
    private GameObject springSphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trailer"))
        {
            fixedCube.SetActive(true); 
            fixedSphere.SetActive(true);
            springSphere.GetComponent<MeshRenderer>().enabled = false;
            springCube.SetActive(false);
        }
    }
}
