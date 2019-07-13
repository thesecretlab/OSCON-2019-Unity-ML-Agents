using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class TreatAcademy : Academy
{
    [HideInInspector]
    public GameObject[] agents;
    [HideInInspector]
    public TreatArea[] listArea;

    public int totalScore;
    public Text scoreText;
    public override void AcademyReset()
    {
        ClearObjects(GameObject.FindGameObjectsWithTag("goodTreat"));
        ClearObjects(GameObject.FindGameObjectsWithTag("badTreat"));

        agents = GameObject.FindGameObjectsWithTag("agent");
        listArea = FindObjectsOfType<TreatArea>();
        foreach (TreatArea ba in listArea)
        {
            ba.ResetTreatArea(agents);
        }

        totalScore = 0;
    }

    void ClearObjects(GameObject[] objects)
    {
        foreach (GameObject treat in objects)
        {
            Destroy(treat);
        }
    }

    public override void AcademyStep()
    {
        scoreText.text = string.Format(@"Score: {0}", totalScore);
    }
}
