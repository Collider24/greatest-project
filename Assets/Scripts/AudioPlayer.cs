using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            audio.Play();
        }
    }
}
