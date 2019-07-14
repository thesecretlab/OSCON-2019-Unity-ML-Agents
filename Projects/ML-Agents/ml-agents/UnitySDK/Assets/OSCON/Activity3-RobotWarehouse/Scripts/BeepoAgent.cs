//Put this script on your agent.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public interface IPushAgent
{
    void IScoredAGoal(GameObject crate, GameObject goal);
    void IHitWrongGoal(GameObject crate, GameObject goal);
}

public class BeepoAgent: Agent, IPushAgent
{
    /// <summary>
    /// The ground. The bounds are used to spawn the elements.
    /// </summary>
	public GameObject ground;

    public GameObject area;

    /// <summary>
    /// The area bounds.
    /// </summary>
	[HideInInspector]
    public Bounds areaBounds;

    BeepoAcademy academy;

    /// <summary>
    /// The goals to push the blocks to.
    /// </summary>
    private CrateDestination[] goals;

    /// <summary>
    /// The blocks to be pushed to the goals.
    /// </summary>
    private Crate[] blocks;
    
    private Rigidbody agentRB;

    public bool useVectorObs;

    RayPerception rayPer;

      // ============== ABOVE HERE SHOULD NOT NEED TO TOUCH =============

    void Awake()
    {
        // code for awake goes here
    }

    public override void InitializeAgent()
    {
        // code for init goes here
    }

    public override void CollectObservations()
    {
        // code for obs goes here
    }

    /// <summary>
    /// Moves the agent according to the selected action.
    /// </summary>
	public void MoveAgent(float[] act)
    {
        // Moving code goes here
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
	public override void AgentAction(float[] vectorAction, string textAction)
    {
        // code goes here
    }

    /// <summary>
    /// Called when the agent moves the block into the goal.
    /// </summary>
    public void IScoredAGoal(GameObject target, GameObject goal)
    {
        // code for goal scoring goes here
    }

    public void IHitWrongGoal(GameObject target, GameObject goal)
    {
        // code for getting wrong goal goes here
    }

    /// <summary>
    /// In the editor, if "Reset On Done" is checked then AgentReset() will
    /// be called automatically anytime we mark done = true in an agent
    /// script.
    /// </summary>
	public override void AgentReset()
    {
        // agent reset code goes here        
    }

    /// <summary>
    /// Swap ground material, wait time seconds, then swap back to the
    /// regular material.
    /// </summary>
    IEnumerator ShowGoalAchievedAnimation(GameObject target, GameObject goal)
    {
        // TODO: replace this with new 'goal scored' effect if you like
        yield break;
        
    }

    // ============== BELOW HERE SHOULD NOT NEED TO TOUCH =============

    /// <summary>
    /// Use the ground's bounds to pick a random spawn position.
    /// </summary>
    public Vector3 GetRandomSpawnPos()
    {
        Vector3 randomSpawnPos = Vector3.zero;

        while (true) {
            float randomPosX = Random.Range(-areaBounds.extents.x * academy.spawnAreaMarginMultiplier,
                                areaBounds.extents.x * academy.spawnAreaMarginMultiplier);

            float randomPosZ = Random.Range(-areaBounds.extents.z * academy.spawnAreaMarginMultiplier,
                                            areaBounds.extents.z * academy.spawnAreaMarginMultiplier);

            randomSpawnPos = ground.transform.position + new Vector3(randomPosX, 1f, randomPosZ);

            if (Physics.CheckBox(randomSpawnPos, new Vector3(2.5f, 0.01f, 2.5f)) == false)
            {
                break;
            }
        }

        return randomSpawnPos;
    }

    /// <summary>
    /// Resets the block position and velocities.
    /// </summary>
    void ResetBlocks() {
        foreach (var block in blocks)
        {
            // Get a random position for the block.
            block.transform.position = GetRandomSpawnPos();

            var blockRB = block.GetComponent<Rigidbody>();

            // Reset block velocity back to zero.
            blockRB.velocity = Vector3.zero;

            // Reset block angularVelocity back to zero.
            blockRB.angularVelocity = Vector3.zero;

            block.Reset();
        }
    }
}
