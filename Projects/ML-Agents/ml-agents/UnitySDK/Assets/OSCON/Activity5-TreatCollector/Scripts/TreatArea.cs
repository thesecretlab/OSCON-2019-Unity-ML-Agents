using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TreatArea : Area
{
    public GameObject treatGood;
    public GameObject treatBad;
    public int numTreats;
    public int numBadTreats;
    public bool respawnTreats;
    public float range;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTreat(int numberTreats, GameObject treatType)
    {
        for (int i = 0; i < numberTreats; i++)
        {
            GameObject currentTreat = Instantiate(treatType, new Vector3(Random.Range(-range, range), 1f,
                                                              Random.Range(-range, range)) + transform.position,
                                          Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 90f)));
            currentTreat.GetComponent<TreatLogic>().respawn = respawnTreats;
            currentTreat.GetComponent<TreatLogic>().myArea = this;
        }
    }

    public void ResetTreatArea(GameObject[] agents)
    {
        foreach (GameObject agent in agents)
        {
            if (agent.transform.parent == gameObject.transform)
            {
                agent.transform.position = new Vector3(Random.Range(-range, range), 2f,
                                                       Random.Range(-range, range))
                    + transform.position;
                agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            }
        }

        CreateTreat(numTreats, treatGood);
        CreateTreat(numBadTreats, treatBad);
    }

    public override void ResetArea()
    {
    }
}
