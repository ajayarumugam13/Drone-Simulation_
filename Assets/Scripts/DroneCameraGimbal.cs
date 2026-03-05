using UnityEngine;
using UnityEngine.InputSystem;

public class DroneCameraGimbal : MonoBehaviour
{
    [Header("References")]
    public Transform pitchPivot;   // CameraPitch transform

    [Header("Rotation Settings")]
    public float yawSpeed = 120f;
    public float pitchSpeed = 100f;

    [Header("Pitch Limits")]
    public float minPitch = -60f;
    public float maxPitch = 40f;

    [Header("Smoothing")]
    public float smoothTime = 0.05f;

    private Vector2 lookInput;

    private float yaw;
    private float pitch;

    private float yawVelocity;
    private float pitchVelocity;

    void Update()
    {
        if (DroneInput.Instance == null) return;

        lookInput = DroneInput.Instance.Look;
        HandleRotation();
    }

    void HandleRotation()
    {
        // Accumulate rotation
        yaw += lookInput.x * yawSpeed * Time.deltaTime;
        pitch -= lookInput.y * pitchSpeed * Time.deltaTime;

        // Clamp pitch
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Smooth damp
        float smoothYaw = Mathf.SmoothDampAngle(
            transform.localEulerAngles.y,
            yaw,
            ref yawVelocity,
            smoothTime
        );

        float smoothPitch = Mathf.SmoothDampAngle(
            pitchPivot.localEulerAngles.x,
            pitch,
            ref pitchVelocity,
            smoothTime
        );

        // Apply rotation
        transform.localRotation = Quaternion.Euler(0f, smoothYaw, 0f);
        pitchPivot.localRotation = Quaternion.Euler(smoothPitch, 0f, 0f);
    }
}

