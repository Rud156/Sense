using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;

namespace PlayerDecisions
{
    public class DecisionPoint : MonoBehaviour
    {
        [Header("Nearby Decision Points")]
        public DecisionPoint leftPoint;
        public DecisionPoint rightPoint;
        public DecisionPoint topPoint;
        public DecisionPoint bottomPoint;

        [Header("Positions")] public Transform decisionPointPosition;

        private bool _isPlayerInside;

        #region Unity Functions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _isPlayerInside = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _isPlayerInside = false;
            }
        }

        private void Update()
        {
            if (!_isPlayerInside)
            {
                return;
            }
        }

        #endregion

        #region External Functions

        public Vector3 GetDecisionPointPosition() => decisionPointPosition.position;

        #endregion
    }
}