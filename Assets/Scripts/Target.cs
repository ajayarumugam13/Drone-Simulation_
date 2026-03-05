using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public string targetName = "Enemy Target";

    public void OnTargetHit()
    {
        // Placeholder for later (damage / explosion)
        Debug.Log($"{targetName} hit!");
        
    }
}

