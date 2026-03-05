using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionDistance = 1000f;
    public LayerMask targetLayer;

    [Header("Debug")]
    public bool showDebugRay = true;

    public Target currentTarget { get; private set; }

    void Update()
    {
        DetectTarget();
    }

    void DetectTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (showDebugRay)
        {
            Debug.DrawRay(transform.position, transform.forward * detectionDistance, Color.red);
        }
        if (Physics.Raycast(ray, out hit, detectionDistance, targetLayer))
        {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                //Debug.Log("Target detected: " + target.name);
            }
            if (target != null)
            {
                currentTarget = target;
                return;
            }
        }

        // No valid target found
        currentTarget = null;
    }
}

