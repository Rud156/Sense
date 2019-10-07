using PlayerDecisions;
using UnityEngine;
using Utils;

namespace PlayerModifier
{
    [RequireComponent(typeof(Collider))]
    public class PlayerMovementInterestModifier : MonoBehaviour
    {
        [Range(0, 1)] public float interestWeight;
        public DecisionPoint decisionPoint;

        private Player.PlayerController _playerController;
        private bool _modifierUsed;

        #region Unity Functions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<Player.PlayerController>();
                _playerController.RegisterInterestModifier(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController.UnRegisterInterestModifier(this);
                _playerController = null;
            }
        }

        #endregion

        #region External Functions

        public Vector3 GetTargetPosition() => decisionPoint.GetDecisionPointPosition();

        public bool IsWithinModifierRange() => _playerController != null;

        public bool CanModifierAffect(float lastModifierWeight) => _playerController && interestWeight > lastModifierWeight && !_modifierUsed;

        public float InterestModifierWeight => interestWeight;

        public void SetModifierUsed() => _modifierUsed = true;

        public void ClearModifierStatus() => _modifierUsed = false;

        #endregion
    }
}