using UnityEngine;
using Utils;

namespace PlayerDecisions
{
    public class DecisionPoint : MonoBehaviour
    {
        [Header("Nearby Decision Points")] public DecisionPoint leftPoint;
        public DecisionPoint rightPoint;
        public DecisionPoint topPoint;
        public DecisionPoint bottomPoint;

        [Header("Positions")] public Transform decisionPointPosition;

        #region Unity Functions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                ActivateDecisionPoint();
            }
        }

        #endregion

        #region External Functions

        public Vector3 GetDecisionPointPosition() => decisionPointPosition.position;

        public DecisionPoint LeftDecisionPoint => leftPoint;

        public DecisionPoint RightDecisionPoint => rightPoint;

        public DecisionPoint TopDecisionPoint => topPoint;

        public DecisionPoint BottomDecisionPoint => bottomPoint;

        #endregion

        #region Utility Functions

        private void ActivateDecisionPoint()
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
        }

        #endregion
    }
}