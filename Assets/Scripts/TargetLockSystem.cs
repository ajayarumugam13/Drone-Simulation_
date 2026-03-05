using Unity.VisualScripting;
using UnityEngine;
public class TargetLockSystem : MonoBehaviour 
{ 
    public static TargetLockSystem Instance; 
    [Header("References")] public Camera droneCamera; 
    [Header("Lock Settings")] public float lockRange = 500f; 
    public LayerMask targetLayer;
    public bool B_TragetLocked = false;
    public Transform LockedTarget { get; private set; } 
    void Awake() { 
        Instance = this; 
    } 
    void Update() 
    { 
        if (DroneInput.Instance == null) return; 
        if (DroneInput.Instance.LockPressed) { Debug.Log("LOCK BUTTON PRESSED"); 
            if (LockedTarget == null) { TryLockTarget(); } 
            else { UnlockTarget(); } } 
        if (LockedTarget != null) { MaintainLock(); }
        if (DroneInput.Instance.FirePressed)
        {
            MissileLaunch.instance.moveMissile();
        }
    } 
    
    void TryLockTarget() 
    { Ray ray = new Ray(droneCamera.transform.position, droneCamera.transform.forward); 
        Debug.DrawRay(ray.origin, ray.direction * lockRange, Color.red, 1f); 
        if (Physics.Raycast(ray, out RaycastHit hit, lockRange, targetLayer)) 
        { 
            LockedTarget = hit.transform; Debug.Log("TARGET LOCKED: " + LockedTarget.name); B_TragetLocked = true; 
            MissileLaunch.instance.GO_To=hit.transform.gameObject;
        } 
        else { Debug.Log("NO TARGET FOUND"); B_TragetLocked = false; } } 
    void MaintainLock() 
    { Vector3 dir = LockedTarget.position - droneCamera.transform.position; 
        Quaternion lookRot = Quaternion.LookRotation(dir); droneCamera.transform.rotation = Quaternion.Lerp(droneCamera.transform.rotation, lookRot, Time.deltaTime * 5f); } 
    void UnlockTarget() { Debug.Log("TARGET UNLOCKED"); LockedTarget = null; MissileLaunch.instance.GO_To = null;
    } 
}
