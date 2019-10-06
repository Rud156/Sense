﻿using System.Collections.Generic;
using PlayerModifier;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")] public PlayerMovement playerMovement;
        public float playerClickWeight = 0.3f;

        private List<MovementInterestModifier> _movementInterestModifiers;
        private bool _hasReachedDestination;

        #region Unity Functions

        private void Start()
        {
            _movementInterestModifiers = new List<MovementInterestModifier>();
        }

        #endregion

        #region External Functions

        public void RegisterInterestModifier(MovementInterestModifier movementInterestModifier) => _movementInterestModifiers.Add(movementInterestModifier);

        public void UnRegisterInterestModifier(MovementInterestModifier movementInterestModifier) => _movementInterestModifiers.Remove(movementInterestModifier);

        public void SetDestinationStatus(bool hasReachedDestination) => _hasReachedDestination = hasReachedDestination;

        public bool HasReachedDestination => _hasReachedDestination;

        public void MakePlayerMoveToDestination(Vector3 targetPosition)
        {
            float maxModifierWeight = playerClickWeight;
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

        public void SnapPlayerToPosition(Vector3 position)
        {
        }

        #endregion

        #region Utility Functions

        #endregion
    }
}