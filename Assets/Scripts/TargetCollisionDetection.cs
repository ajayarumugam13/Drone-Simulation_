using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetCollisionDetection : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            particleEffect.Play();
            ApplyTransformData();
        }
    }

    public void ApplyTransformData()
    {
        transform.SetPositionAndRotation(
            targetPosition,
            Quaternion.Euler(targetRotation)
        );
    }
}
