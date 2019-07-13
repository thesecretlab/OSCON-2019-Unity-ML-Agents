using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace UnityStandardAssets.Vehicles.Car {
    [RequireComponent(typeof(CarController))]
    public class CarAgent : Agent {
        private CarController carController;
        private Rigidbody rigidBody;
        public Transform resetPoint;

        // NEW STUFF
        private float lapTime = 0;
        private float bestLapTime = 0;
        private bool isCollided = false;
        private bool startLinePassed = false;
        public bool agentIsTraining = false;

        public Transform[] trackWaypoints = new Transform[14];

        // When the object enters the scene
        public void Awake() {
            carController = GetComponent<CarController>();
            rigidBody = GetComponent<Rigidbody>();
        }

        // When the agent requests an action
        // Called every tick to check what the car should do next
        public override void AgentAction(float[] vectorAction, string textAction) {
            float h = vectorAction[0];
            //float v = vectorAction[1];
            carController.Move(h, 1, 0, 0);

            // Once the actions are done, we need to check:
            if(isCollided) {
                // we hit something
                AddReward(-1.0f); // you get a punishment, you get a punishment, we all get punishments!
                Done();
            } else {
                // we did not hit something
                AddReward(0.05f); // what a good car you are!
            }
        }

        public override void AgentReset() {
            // Reset to closest waypoint if we're training
            if(agentIsTraining) {
                float min_distance = 1e+6f;
                int index = 0;
                for(int i = 1; i < trackWaypoints.Length; i++) {
                    float distance = Vector3.SqrMagnitude(trackWaypoints[i].position - transform.position);
                    if(distance < min_distance) {
                        min_distance = distance;
                        index = i;
                    }
                }
                transform.SetPositionAndRotation(trackWaypoints[index-1].position, new Quaternion(0,0,0,0));
                transform.LookAt(trackWaypoints[index].position);
            } else {
                // Reset to beginning if we're NOT training
                lapTime = 0;
                transform.position = resetPoint.position;
                transform.rotation = resetPoint.rotation;
            }

            // No matter whether we're training or not, we also need to:
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            isCollided = false;
        }

        public override void CollectObservations() {
            // Current speed
            //AddVectorObs(rigidBody.velocity.sqrMagnitude / 300.0f);
        }
        
        void FixedUpdate() {
            lapTime += Time.fixedDeltaTime;
        }

        private void Update() {
            // float angle = Mathf.Clamp(steering, -1, 1) * 90f;
            // wheelFrontLeft.localEulerAngles = new Vector3(0f, -90f + angle / 3, 0f);
            // wheelFrontRight.localEulerAngles = new Vector3(0f, -90f + angle / 3, 0f);
        }

        private void OnTriggerEnter(Collider other) {
            // if we hit the start line...
            if(other.CompareTag("StartLine")) {
                if(!startLinePassed) {
                    if (lapTime < bestLapTime) {
                        bestLapTime = lapTime;
                    }
                    Debug.Log("Lap completed: " + lapTime);
                    lapTime = 0;
                    startLinePassed = true;
                }
            } else {
                // we hit a wall...
                isCollided = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            startLinePassed = false;
        }
    }
}