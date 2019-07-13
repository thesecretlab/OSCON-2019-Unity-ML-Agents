//Put this script on your blue cube.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MazeAgent : Agent
{
    /// <summary>
    /// The ground. The bounds are used to spawn the elements.
    /// </summary>
	public GameObject ground;

    public GameObject area;

    public Transform startPosition;

    public BeepoTreat treat;

    /// <summary>
    /// The area bounds.
    /// </summary>
	[HideInInspector]
    public Bounds areaBounds;

    MazeAcademy academy;

    
    private Rigidbody agentRB;

    public bool useVectorObs;

    RayPerception rayPer;



    void Awake()
    {
        academy = FindObjectOfType<MazeAcademy>(); //cache the academy

        // goals = area.GetComponentsInChildren<CrateDestination>();
        // blocks = area.GetComponentsInChildren<Crate>();
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        agentRB = GetComponent<Rigidbody>();

        rayPer = GetComponent<RayPerception>();

        // Get the ground's bounds
        areaBounds = ground.GetComponent<Collider>().bounds;
        
    }



    public override void CollectObservations()
    {
        if (useVectorObs)
        {
            var rayDistance = 12f;
            float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
            var detectableObjects = new[] { "treat", "wall", "maze" };
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
        }
    }

    /// <summary>
    /// Use the ground's bounds to pick a random spawn position.
    /// </summary>
    public Vector3 GetRandomSpawnPos()
    {
        bool foundNewSpawnLocation = false;
        Vector3 randomSpawnPos = Vector3.zero;
        while (foundNewSpawnLocation == false)
        {
            float randomPosX = Random.Range(-areaBounds.extents.x * academy.spawnAreaMarginMultiplier,
                                areaBounds.extents.x * academy.spawnAreaMarginMultiplier);

            float randomPosZ = Random.Range(-areaBounds.extents.z * academy.spawnAreaMarginMultiplier,
                                            areaBounds.extents.z * academy.spawnAreaMarginMultiplier);
            randomSpawnPos = ground.transform.position + new Vector3(randomPosX, 1f, randomPosZ);
            if (Physics.CheckBox(randomSpawnPos, new Vector3(2.5f, 0.01f, 2.5f)) == false)
            {
                foundNewSpawnLocation = true;
            }
        }
        return randomSpawnPos;
    }


    /// <summary>
    /// Moves the agent according to the selected action.
    /// </summary>
	public void MoveAgent(float[] act)
    {

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(act[0]);

        // Goalies and Strikers have slightly different action spaces.
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);

        agentRB.AddForce(dirToGo * academy.agentRunSpeed,
                         ForceMode.VelocityChange);

    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
	public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Move the agent using the action.
        MoveAgent(vectorAction);

        // Penalty given each step to encourage agent to finish task quickly.
        //AddReward(-1f / agentParameters.maxStep);
    }


    /// <summary>
    /// In the editor, if "Reset On Done" is checked then AgentReset() will
    /// be called automatically anytime we mark done = true in an agent
    /// script.
    /// </summary>
	public override void AgentReset()
    {
        int rotation = Random.Range(0, 4);
        float rotationAngle = rotation * 90f;
        area.transform.Rotate(new Vector3(0f, rotationAngle, 0f));

        //transform.position = GetRandomSpawnPos();
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
        agentRB.velocity = Vector3.zero;
        agentRB.angularVelocity = Vector3.zero;

        
    }

    public void OnCollisionEnter(Collision collision) {
        if(!collision.collider.CompareTag("ground")) {
            if(collision.collider.CompareTag("wall") || collision.collider.CompareTag("maze")) {
                // penalty
                Debug.Log("I hit something that's not the treat!" + collision.collider.name);
                AddReward(-1f);
            }
            
            if(collision.collider.CompareTag("treat")) {
                // reward
                Debug.Log("I found the treat!");
                AddReward(5f);
                Done();
            }
        }
        
    }

}
