using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{
    [Header("Movement Forces")]
    public float moveForce = 15f;     // Forward / Strafe force
    public float liftForce = 20f;     // Up / Down force
    public float yawTorque = 8f;      // Rotation force

    [Header("Stabilization")]
    public float stabilizationForce = 5f;

    private Rigidbody rb;

    // Input values (will come from Input System later)
    [HideInInspector] public Vector2 moveInput; // X = strafe, Y = forward
    [HideInInspector] public float liftInput;   // up/down
    [HideInInspector] public float yawInput;    // rotation

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (DroneInput.Instance == null) return;

        moveInput = DroneInput.Instance.Move;
        liftInput = DroneInput.Instance.Lift;
        yawInput = DroneInput.Instance.Yaw;

        HandleMovement();
        HandleStabilization();
    }

    void HandleMovement()
    {
        // Forward & Strafe
        Vector3 moveDirection =
            (transform.forward * moveInput.y +
             transform.right * moveInput.x) * moveForce;

        rb.AddForce(moveDirection, ForceMode.Force);

        // Ascend / Descend
        Vector3 lift = Vector3.up * liftInput * liftForce;
        rb.AddForce(lift, ForceMode.Force);

        // Yaw rotation
        rb.AddTorque(Vector3.up * yawInput * yawTorque, ForceMode.Force);
    }

    void HandleStabilization()
    {
        // Prevent unwanted rolling & pitching
        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 counterTorque = new Vector3(
            -angularVelocity.x,
            0f,
            -angularVelocity.z
        );

        rb.AddTorque(counterTorque * stabilizationForce, ForceMode.Force);
    }
}