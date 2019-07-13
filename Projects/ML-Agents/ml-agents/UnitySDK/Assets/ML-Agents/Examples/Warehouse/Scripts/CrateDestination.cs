using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestination : MonoBehaviour
{

    public GoalType type;
    
    public void SetColor(Color color) {
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
            // Tint the renderer to the colour we specify
            renderer.material.color = color;
        }
    }
}
