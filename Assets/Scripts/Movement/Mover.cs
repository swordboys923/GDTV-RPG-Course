using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction {

        private NavMeshAgent agent;
        private Animator animator;
        private ActionScheduler actionScheduler;

        private void Awake() {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
            //if (Input.GetMouseButton(0)) {
            //    MoveToCursor();
            //}
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        private void MoveToCursor() {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out hit);
            if(hasHit) {
                MoveTo(hit.point);
            }
        }

        public void Cancel() {
            agent.isStopped = true;
        }

        public void MoveTo(Vector3 destination) {
            agent.destination = destination;
            agent.isStopped = false;
        }


        private void UpdateAnimator() {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = Mathf.Abs(localVelocity.z);
            animator.SetFloat("forwardSpeed", speed);
        }
    }    
}
