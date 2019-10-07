using System.Collections.Generic;
using BeliefSystem;
using PlayerModifier;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")] public PlayerMovement playerMovement;
        public float minPlayerClickWeight = 0.3f;
        public PlayerAnimation playerAnimation;

        private List<PlayerMovementInterestModifier> _playerMovementInterestModifiers;
        private bool _hasReachedDestination;

        #region Unity Functions

        private void Start() => _playerMovementInterestModifiers = new List<PlayerMovementInterestModifier>();

        #endregion

        #region External Functions

        public void RegisterInterestModifier(PlayerMovementInterestModifier playerMovementInterestModifier) =>
            _playerMovementInterestModifiers.Add(playerMovementInterestModifier);

        public void UnRegisterInterestModifier(PlayerMovementInterestModifier playerMovementInterestModifier) =>
            _playerMovementInterestModifiers.Remove(playerMovementInterestModifier);

        public void SetDestinationStatus(bool hasReachedDestination) => _hasReachedDestination = hasReachedDestination;

        public bool HasReachedDestination => _hasReachedDestination;

        public void MakePlayerMoveToDestination(Vector3 targetPosition)
        {
            float currentBelief = BeliefController.Instance.CurrentBeliefAmount;
            float maxBelief = BeliefController.Instance.MaxBeliefAmount;

            float beliefRatio = currentBelief / maxBelief;
            float mappedClickWeight = ExtensionFunctions.Map(beliefRatio, 0, 1, minPlayerClickWeight, 1);

            float maxModifierWeight = mappedClickWeight;
            PlayerMovementInterestModifier maxModifier = null;

            foreach (PlayerMovementInterestModifier playerMovementInterestModifier in _playerMovementInterestModifiers)
            {
                if (playerMovementInterestModifier.CanModifierAffect(maxModifierWeight))
                {
                    maxModifierWeight = playerMovementInterestModifier.InterestModifierWeight;
                    maxModifier = playerMovementInterestModifier;
                }
            }

            if (!maxModifier)
            {
                playerMovement.MovePlayerToPosition(targetPosition);
            }
            else
            {
                playerMovement.MovePlayerToPosition(maxModifier.GetTargetPosition());
                maxModifier.SetModifierUsed();
            }
        }

        public void StopPlayerMovement() => playerMovement.StopPlayerMovement();

        public void SnapAndRevivePlayerToPosition(Vector3 position)
        {
            transform.position = position;
            playerAnimation.ReviveDeath();
        }

        public void PlayFallDeathAnimation(Vector3 startFallPosition)
        {
            playerAnimation.PlayFallDeathAnimation();
            transform.position = startFallPosition;
        }

        public void PlayDeathAnimation() => playerAnimation.PlayDeathAnimation();

        #endregion

        #region Utility Functions

        #endregion
    }
}