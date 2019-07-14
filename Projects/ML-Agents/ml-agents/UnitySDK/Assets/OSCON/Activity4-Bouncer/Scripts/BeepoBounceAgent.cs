using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BeepoBounceAgent: Agent 
{
    [Header("Bouncer Specific")]
    public GameObject bodyObject;
    Rigidbody rb;
    Vector3 lookDir;
    public float strength = 10f;
    float jumpCooldown;
    int numberJumps = 20;
    int jumpLeft = 20;

    public override void InitializeAgent()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        lookDir = Vector3.zero;
    }

    public override void CollectObservations()
    {
        // add obs here
    }

    public override void AgentAction(float[] vectorAction, string textAction)
	{
        // add actions here
    }

    public override void AgentReset()
    {
        gameObject.transform.localPosition = new Vector3(
            (1   - 2 * Random.value) *5, 2, (1 - 2 * Random.value)*5);
        rb.velocity = default(Vector3);
   
        jumpLeft = numberJumps;
    }

    public override void AgentOnDone()
    {

    }

    private void FixedUpdate()
    {
        // add fixed update
    }

    private void Update()
    {
        if (lookDir.magnitude > float.Epsilon)
        {
            bodyObject.transform.rotation = Quaternion.Lerp(bodyObject.transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 10f);
        }
    }
}
