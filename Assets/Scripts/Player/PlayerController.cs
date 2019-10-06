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

        private List<MovementInterestModifier> _movementInterestModifiers;
        private bool _hasReachedDestination;

        #region Unity Functions

        private void Start() => _movementInterestModifiers = new List<MovementInterestModifier>();

        #endregion

        #region External Functions

        public void RegisterInterestModifier(MovementInterestModifier movementInterestModifier) => _movementInterestModifiers.Add(movementInterestModifier);

        public void UnRegisterInterestModifier(MovementInterestModifier movementInterestModifier) => _movementInterestModifiers.Remove(movementInterestModifier);

        public void SetDestinationStatus(bool hasReachedDestination) => _hasReachedDestination = hasReachedDestination;

        public bool HasReachedDestination => _hasReachedDestination;

        public void MakePlayerMoveToDestination(Vector3 targetPosition)
        {
            float currentBelief = BeliefController.Instance.CurrentBeliefAmount;
            float maxBelief = BeliefController.Instance.MaxBeliefAmount;

            float beliefRatio = currentBelief / maxBelief;
            float mappedClickWeight = ExtensionFunctions.Map(beliefRatio, 0, 1, minPlayerClickWeight, 1);

            float maxModifierWeight = mappedClickWeight;
            MovementInterestModifier maxModifier = null;

            foreach (MovementInterestModifier movementInterestModifier in _movementInterestModifiers)
            {
                if (movementInterestModifier.CanModifierAffect(maxModifierWeight))
                {
                    maxModifierWeight = movementInterestModifier.InterestModifierWeight;
                    maxModifier = movementInterestModifier;
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

        public void SnapAndRevivePlayerToPosition(Vector3 position)
        {
            transform.position = position;
            playerAnimation.ReviveDeath();
        }

        public void PlayFallDeathAnimation() => playerAnimation.PlayFallDeathAnimation();

        public void PlayDeathAnimation() => playerAnimation.PlayDeathAnimation();

        #endregion

        #region Utility Functions

        #endregion
    }
}