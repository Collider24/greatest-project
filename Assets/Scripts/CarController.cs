using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private float horizontalInput;
    private float currentSteerAngle;

    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [SerializeField]
    private SteamVR_Action_Single throttleButton;

    [SerializeField]
    private SteamVR_Action_Single brakeButton;

    private float throttle = 0;

    private float currentSettedSpeed = 0;
    private float currentAngle = 0;

    private float brakeInput;
    private float currentBrakeForce = 0;

    [SerializeField]
    private CircularDrive steeringWheel;

    [SerializeField]
    private AudioSource idleMotorSound;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleBrake();
    }

    private void GetInput()
    {

        horizontalInput = 0;

        throttle = throttleButton.GetAxis(SteamVR_Input_Sources.RightHand);
        idleMotorSound.pitch = 1 + throttle / 3;
        idleMotorSound.volume = 0.5f + throttle / 2;


        brakeInput = brakeButton.GetAxis(SteamVR_Input_Sources.LeftHand);
        horizontalInput = steeringWheel.outAngle / 540;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = throttle * motorForce;
        frontRightWheelCollider.motorTorque = throttle * motorForce;
        rearLeftWheelCollider.motorTorque = throttle * motorForce;
        rearRightWheelCollider.motorTorque = throttle * motorForce;
    }

    private void HandleBrake()
    {
        currentBrakeForce = brakeInput * brakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        if (horizontalInput >= 0)
        {
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle + horizontalInput * 10;
        }
        else if (horizontalInput < 0)
        {
            frontLeftWheelCollider.steerAngle = currentSteerAngle + horizontalInput * 10;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
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
