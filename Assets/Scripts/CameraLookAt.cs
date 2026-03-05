using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [Tooltip("Target the camera should look at")]
    public Transform target;

    void LateUpdate()
    {
        if (target == null) return;

        transform.LookAt(target);
    }

}
