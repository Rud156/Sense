using UnityEngine;
using Utils;

namespace PlayerModifier
{
    [RequireComponent(typeof(SphereCollider))]
    public class MovementInterestModifier : MonoBehaviour
    {
        [Range(0, 1)] public float interestWeight;
        public float interestRange;

        private SphereCollider _sphereCollider;
        private Player.PlayerController _playerController;
        private bool _modifierUsed;

        #region Unity Functions

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = interestRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, interestRange);
        }

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

        public Vector3 GetTargetPosition() => transform.position;

        public bool IsWithinModifierRange() => _playerController != null;

        public bool CanModifierAffect(float lastModifierWeight) => _playerController && interestWeight > lastModifierWeight && !_modifierUsed;

        public float InterestModifierWeight => interestWeight;

        public void SetModifierUsed() => _modifierUsed = true;

        public void ClearModifierStatus() => _modifierUsed = false;

        #endregion
    }
}