using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private List<Transform> targetPositions;
        [SerializeField] private float speed = 1.0f;
        private int _currentTargetIndex = 0;

        public bool isMoving = true;
        
        private void Update()
        {
            if (isMoving)
                MoveToTarget();
        }

        void MoveToTarget()
        {
            var direction = targetPositions[_currentTargetIndex].position - transform.position;
            var distance = direction.magnitude;
            var movement = direction.normalized * (speed * Time.deltaTime);
            if (movement.magnitude > distance)
            {
                transform.position = targetPositions[_currentTargetIndex].position;
                _currentTargetIndex = (_currentTargetIndex + 1) % targetPositions.Count;
            }
            else
            {
                transform.position += movement;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (targetPositions == null || targetPositions.Count == 0)
                return;
            
            Gizmos.color = Color.red;
            // draw patrol line
            for (int i = 0; i < targetPositions.Count - 1; i++)
            {
                Gizmos.DrawLine(targetPositions[i].position, targetPositions[i + 1].position);
            }

            Gizmos.DrawLine(targetPositions[^1].position, targetPositions[0].position);
        }
    }
}