using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerController : MonoBehaviour
{

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;

    private void Start()
    {
        frontLeftWheelCollider.motorTorque = 1;
        frontRightWheelCollider.motorTorque = 1;
    }

    private void FixedUpdate()
    {
        UpdateWheels();
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
