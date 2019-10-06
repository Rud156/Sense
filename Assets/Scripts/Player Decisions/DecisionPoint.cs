using System;
using System.Collections;
using Player;
using UnityEngine;
using Utils;

namespace PlayerDecisions
{
    [RequireComponent(typeof(DecisionPointModifier))]
    public class DecisionPoint : MonoBehaviour
    {
        [Header("Nearby Decision Points")] public DecisionPoint leftPoint;
        public DecisionPoint rightPoint;
        public DecisionPoint topPoint;
        public DecisionPoint bottomPoint;

        [Header("Positions")] public Transform decisionPointPosition;

        [Header("Items")] public DecisionItem decisionItem;
        [TextArea] public string decisionPointDialogue;
        public float resetPlayerAfterTime = 2;

        private DecisionPointModifier _decisionPointModifier;
        private PlayerController _playerController;

        #region Unity Functions

        private void Start() => _decisionPointModifier = GetComponent<DecisionPointModifier>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<PlayerController>();
                ActivateDecisionPoint();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = null;
            }
        }

        #endregion

        #region External Functions

        public Vector3 GetDecisionPointPosition() => decisionPointPosition.position;

        public DecisionPoint LeftDecisionPoint => leftPoint;

        public DecisionPoint RightDecisionPoint => rightPoint;

        public DecisionPoint TopDecisionPoint => topPoint;

        public DecisionPoint BottomDecisionPoint => bottomPoint;

        public DecisionItem GetDecisionItem() => decisionItem;

        public DecisionPointModifier GetDecisionPointModifier() => _decisionPointModifier;

        #endregion

        #region Utility Functions

        private void ActivateDecisionPoint()
        {
            bool isSafeDecisionPoint = _decisionPointModifier.AffectPlayer(_playerController.transform.position, _playerController);
            if (isSafeDecisionPoint)
            {
                ActivateGoodDecisionPoint();
            }
            else
            {
                StartCoroutine(ActivateBadDecisionPoint());
            }
        }

        private void ActivateGoodDecisionPoint()
        {
            if (leftPoint != null)
            {
                DecisionController.Instance.ActivateLeftArrow();
            }

            if (rightPoint != null)
            {
                DecisionController.Instance.ActivateRightArrow();
            }

            if (topPoint != null)
            {
                DecisionController.Instance.ActivateTopArrow();
            }

            if (bottomPoint != null)
            {
                DecisionController.Instance.ActivateBottomArrow();
            }

            DecisionController.Instance.RegisterDecisionPoint(this);
            DecisionController.Instance.DecrementOffsetOnSuccessPlayer(this);
        }

        private IEnumerator ActivateBadDecisionPoint()
        {
            yield return new WaitForSeconds(resetPlayerAfterTime);
            DecisionController.Instance.RevertToLastCheckPoint();
        }

        #endregion
    }
}