//Detect when the orange block has touched the goal. 
//Detect when the orange block has touched an obstacle. 
//Put this script onto the orange block. There's nothing you need to set in the editor.
//Make sure the goal is tagged with "goal" in the editor.

using UnityEngine;

public class Crate : MonoBehaviour
{

    public GoalType type;

    public MeshRenderer innerCubeRenderer;

    public void SetColor(Color color) {
        innerCubeRenderer.material.color = color;
    }

    [HideInInspector]
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization. 
    /// Don't need to manually set.
    /// </summary>
	public IPushAgent agent;  //

    void OnTriggerEnter(Collider col)
    {
        // Touched goal.
        if (col.gameObject.CompareTag("goal"))
        {
            // Get the goal component from the thing we touched
            var goal = col.gameObject.GetComponent<CrateDestination>();

            if (goal == null) {
                Debug.LogWarning("Touched a goal, but it has no CrateDestination component");
                return;
            }

            // if (goal.type != this.type) {
            //     Debug.LogWarning("Touched a goal, but it's the wrong type for the current crate.");
            //     agent.IHitWrongGoal(gameObject, col.gameObject);
            // }

            if (goal.type == this.type) {

                IsActive = false;

                // Tell the agent that this block touched this goal.
                agent.IScoredAGoal(gameObject, col.gameObject);
                
            }
            
        }
    }

    public bool IsActive {
        get {
            return gameObject.activeInHierarchy;
        }
        set {
            if (value) {
                // Move back to the default layer so we can be "seen"
                gameObject.layer = LayerMask.NameToLayer("Default");
                gameObject.SetActive(true);
            } else {
                // Move to the IgnoreRaycast layer so that we can't be "seen"
                // anymore
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                // Disable this object
                gameObject.SetActive(false);
            }
        }
    }

    public void Reset() {
        IsActive = true;
    }
}
