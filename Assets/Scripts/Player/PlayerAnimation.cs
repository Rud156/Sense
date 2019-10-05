using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Animation")] public Animator playerAnimator;
        public NavMeshAgent playerAgent;
        public PlayerMovement playerMovement;

        [Header("Idle")] public float idleChangeTime;
        public float idleLerpRate;

        [Header("Locomotion")] public float locomotionLerpRate;
        public float minAnimationAffectValue = 0.2f;

        private float _walkingSpeed;
        private float _runningSpeed;

        private static readonly int IdleParam = Animator.StringToHash("Idle Random");
        private float _idleSwitchTimer;
        private float _targetIdleValue;
        private float _currentIdleValue;

        private static readonly int MovementParam = Animator.StringToHash("Movement Speed");
        private float _currentMovementSpeed;
        private float _targetMovementSpeed;

        #region Unity Functions

        private void Start()
        {
            _walkingSpeed = playerMovement.GetWalkingSpeed();
            _runningSpeed = playerMovement.GetRunningSpeed();

            _targetIdleValue = 0;
        }

        private void Update()
        {
            UpdateIdleAnimation();
            UpdateLocomotionAnimation();
        }

        #endregion

        #region Utility Functions

        private void UpdateIdleAnimation()
        {
            _idleSwitchTimer -= Time.deltaTime;

            if (_idleSwitchTimer <= 0)
            {
                _idleSwitchTimer = idleChangeTime;
                _targetIdleValue = Random.value;
            }

            _currentIdleValue = Mathf.Lerp(_currentIdleValue, _targetIdleValue, idleLerpRate * Time.deltaTime);
            playerAnimator.SetFloat(IdleParam, _currentIdleValue);
        }

        private void UpdateLocomotionAnimation()
        {
            if (playerAgent.velocity.sqrMagnitude == 0)
            {
                playerAnimator.SetFloat(MovementParam, 0);
                _targetMovementSpeed = _walkingSpeed;
            }
            else
            {
                _targetMovementSpeed = playerMovement.IsPlayerRunning() ? _runningSpeed : _walkingSpeed;
                _targetMovementSpeed = ExtensionFunctions.Map(_targetMovementSpeed, _walkingSpeed, _runningSpeed, minAnimationAffectValue, 1);
                _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, _targetMovementSpeed, locomotionLerpRate * Time.deltaTime);

                playerAnimator.SetFloat(MovementParam, _currentMovementSpeed);
            }
        }

        #endregion
    }
}