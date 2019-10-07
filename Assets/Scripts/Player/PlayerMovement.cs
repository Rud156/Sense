using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Controllers")] public PlayerController playerController;
        public NavMeshAgent playerAgent;

        [Header("Affect Data")] public float minRotationAffectDistance = 5;
        public float rotationRate = 5;
        public float walkingSpeed = 14;
        public float runningSpeed = 21;
        public float minDistanceBeforeRunning = 100;

        private Vector3 _targetPosition;
        private bool _isPlayerRunning;

        private bool _playerStopped;

        #region Unity Functions

        private void Start()
        {
            _targetPosition = transform.position;
            playerController.SetDestinationStatus(true);

            playerAgent.speed = walkingSpeed;
        }

        private void Update()
        {
            if (_playerStopped)
            {
                return;
            }

            MovePlayerTowardsTarget();
            RotatePlayerTowardsTarget();
        }

        #endregion

        #region External Functions

        public void MovePlayerToPosition(Vector3 position)
        {
            if (Vector3.Distance(position, transform.position) > minDistanceBeforeRunning)
            {
                _isPlayerRunning = true;
                playerAgent.speed = runningSpeed;
            }
            else
            {
                _isPlayerRunning = false;
                playerAgent.speed = walkingSpeed;
            }

            _targetPosition = position;
            _playerStopped = false;
        }

        public void StopPlayerMovement(bool forceStop = false)
        {
            playerAgent.ResetPath();

            _playerStopped = true;
        }

        public float GetWalkingSpeed() => walkingSpeed;

        public float GetRunningSpeed() => runningSpeed;

        public bool IsPlayerRunning() => _isPlayerRunning;

        #endregion

        #region Utility Functions

        private void MovePlayerTowardsTarget()
        {
            if (!playerAgent.isOnNavMesh)
            {
                return;
            }

            playerAgent.SetDestination(_targetPosition);
            playerController.SetDestinationStatus(!playerAgent.hasPath);
        }

        private void RotatePlayerTowardsTarget()
        {
            float distanceToTarget = Vector3.Distance(_targetPosition, transform.position);
            if (distanceToTarget < minRotationAffectDistance)
            {
                return;
            }

            Vector3 lookDirection = _targetPosition - transform.position;
            lookDirection.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationRate * Time.deltaTime);
        }

        #endregion
    }
}