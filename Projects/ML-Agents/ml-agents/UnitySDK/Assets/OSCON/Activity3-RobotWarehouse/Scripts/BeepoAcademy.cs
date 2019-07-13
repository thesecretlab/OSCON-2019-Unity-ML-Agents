//Every scene needs an academy script. 
//Create an empty gameObject and attach this script.
//The brain needs to be a child of the Academy gameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public enum GoalType {
    Goal1,
    Goal2,
}

[System.Serializable]
public class GoalDefinition {
    public GoalType type;
    public Color color;
}

public class BeepoAcademy : Academy
{


	public float agentRunSpeed;
    public float agentRotationSpeed;

    /// <summary>
    /// The spawn area margin multiplier.
    /// ex: .9 means 90% of spawn area will be used. 
    /// .1 margin will be left (so players don't spawn off of the edge). 
    /// The higher this value, the longer training time required.
    /// </summary>
	public float spawnAreaMarginMultiplier;

    /// <summary>
    /// When a goal is scored the ground will switch to this 
    /// material for a few seconds.
    /// </summary>
    public Material goalScoredMaterial;

    /// <summary>
    /// When an agent fails, the ground will turn this material for a few seconds.
    /// </summary>
    public Material failMaterial;

    /// <summary>
    /// The gravity multiplier.
    /// Use ~3 to make things less floaty
    /// </summary>
	public float gravityMultiplier;

    void State()
    {
        Physics.gravity *= gravityMultiplier;

    }

    public GoalDefinition[] goalDefinitions;

    public GoalDefinition FindGoalDefinition(GoalType type) {
        foreach (var definition in goalDefinitions) {
            if (definition.type == type) {
                return definition;
            }
        }
        return null;
    }
}
