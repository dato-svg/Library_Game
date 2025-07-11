using UnityEngine;
using UnityEngine.AI;
using System;

namespace BaseScripts.NavMovement
{
    public class ReadersMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float stoppingDistance = 0.5f;

        private Action onArrival;
        private bool isMoving = false;

        private void Update()
        {
            if (!isMoving) return;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= stoppingDistance)
            {
                isMoving = false;
                onArrival?.Invoke();
            }
        }

        public void MoveTo(Vector3 targetPosition, Action onComplete = null)
        {
            isMoving = true;
            onArrival = onComplete;
            navMeshAgent.SetDestination(targetPosition);
        }

        public void SetSpeed(float speed)
        {
            navMeshAgent.speed = speed;
        }
    }
}