using Player;
using PlayerDecisions;
using UnityEngine;
using Utils;

namespace PlayerModifier
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(DecisionPoint))]
    public class PlayerMovementInterestModifier : MonoBehaviour
    {
        [Range(0, 1)] public float interestWeight;
        public PlayerAbility[] requiredAbilities;

        private DecisionPoint _decisionPoint;

        private PlayerController _playerController;
        private PlayerSenseController _playerSenseController;
        private bool _modifierUsed;

        #region Unity Functions

        private void Start() => _decisionPoint = GetComponent<DecisionPoint>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<PlayerController>();
                _playerController.RegisterInterestModifier(this);

                _playerSenseController = other.GetComponent<PlayerSenseController>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController.UnRegisterInterestModifier(this);
                _playerController = null;

                _playerSenseController = null;
            }
        }

        #endregion

        #region External Functions

        public Vector3 GetTargetPosition() => _decisionPoint.GetDecisionPointPosition();

        public bool IsWithinModifierRange() => _playerController != null;

        public bool CanModifierAffect(float lastModifierWeight)
        {
            bool hasRequiredAbilities = false;
            foreach (PlayerAbility requiredAbility in requiredAbilities)
            {
                if ((requiredAbility == PlayerAbility.Hearing && _playerSenseController.CanPlayerHear) ||
                    (requiredAbility == PlayerAbility.GrayScaleSight && _playerSenseController.HasGrayScaleSight) ||
                    (requiredAbility == PlayerAbility.ColoredSight && _playerSenseController.HasColoredSight))
                {
                    hasRequiredAbilities = true;
                    break;
                }
            }

            return _playerController && interestWeight > lastModifierWeight && !_modifierUsed && hasRequiredAbilities;
        }

        public float InterestModifierWeight => interestWeight;

        public void SetModifierUsed() => _modifierUsed = true;

        public void ClearModifierStatus() => _modifierUsed = false;

        #endregion
    }
}