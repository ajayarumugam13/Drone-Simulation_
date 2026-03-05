using UnityEngine;
using UnityEngine.InputSystem;

public class DroneInput : MonoBehaviour
{
    public static DroneInput Instance { get; private set; }

    public Vector2 Move { get; private set; }
    public float Lift { get; private set; }
    public float Yaw { get; private set; }
    public Vector2 Look { get; private set; }

    public bool LockPressed { get; private set; }
    public bool FirePressed { get; private set; }

    private DroneInputActions inputActions;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        inputActions = new DroneInputActions();
    }

    void OnEnable()
    {
        inputActions.Drone.Enable();

        inputActions.Drone.LockTarget.performed += _ => LockPressed = true;
        inputActions.Drone.FireMissile.performed += _ => FirePressed = true;
    }

    void OnDisable()
    {
        inputActions.Drone.LockTarget.performed -= _ => LockPressed = true;
        inputActions.Drone.FireMissile.performed -= _ => FirePressed = true;

        inputActions.Drone.Disable();
    }

    void Update()
    {
        Move = inputActions.Drone.Move.ReadValue<Vector2>();
        Lift = inputActions.Drone.Lift.ReadValue<float>();
        Yaw = inputActions.Drone.Yaw.ReadValue<float>();
        Look = inputActions.Drone.CameraLook.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        // Reset one-frame buttons
        LockPressed = false;
        FirePressed = false;
    }

    // called by Input System
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            FirePressed = true;
    }

    public void OnLock(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            LockPressed = true;
    }

    public void ConsumeFire()
    {
        FirePressed = false;
    }

    public void ConsumeLock()
    {
        LockPressed = false;
    }
}