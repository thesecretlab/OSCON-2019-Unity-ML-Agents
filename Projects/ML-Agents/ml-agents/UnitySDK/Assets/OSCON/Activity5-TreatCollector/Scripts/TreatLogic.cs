using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatLogic : MonoBehaviour {

    public bool respawn;
    public TreatArea myArea;

    // Use this for initialization
    void Start () {
        
    }

    void FixedUpdate () {
        gameObject.transform.Rotate(new Vector3(1, 0, 0), 0.5f);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void OnEaten() {
        if (respawn) 
        {
            transform.position = new Vector3(Random.Range(-myArea.range, myArea.range), 
                                             transform.position.y + 3f, 
                                             Random.Range(-myArea.range, myArea.range));
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
