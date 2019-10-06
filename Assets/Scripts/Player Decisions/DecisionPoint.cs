using System;
using System.Collections;
using System.Collections.Generic;
using BeliefSystem;
using Player;
using PlayerDecisions.DecisionItemData;
using PlayerDecisions.DecisionModifiers;
using UI;
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

        [Header("Point Data")] public Transform decisionPointPosition;
        public float beliefAmount;
        public DecisionPointWorldType decisionPointWorldType;
        public int resistanceLessBeliefReduceAmount;

        [Header("Items")] public DecisionItem decisionItem;
        [TextArea] public List<string> decisionPointDialogue;
        public float resetPlayerAfterTime = 2;

        private DecisionPointModifier _decisionPointModifier;
        private PlayerController _playerController;
        private PlayerSenseController _playerSenseController;

        #region Unity Functions

        private void Start() => _decisionPointModifier = GetComponent<DecisionPointModifier>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<PlayerController>();
                _playerSenseController = other.GetComponent<PlayerSenseController>();
                ActivateDecisionPoint();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = null;
                _playerSenseController = null;
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

            switch (decisionPointWorldType)
            {
                case DecisionPointWorldType.Hot:
                {
                    if (!_playerSenseController.HasHeatResistance)
                    {
                        BeliefController.Instance.ReduceBelief(resistanceLessBeliefReduceAmount);
                    }
                }
                    break;

                case DecisionPointWorldType.Normal:
                    break;

                case DecisionPointWorldType.Cold:
                {
                    if (!_playerSenseController.HasColdResistance)
                    {
                        BeliefController.Instance.ReduceBelief(resistanceLessBeliefReduceAmount);
                    }
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
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

            BeliefController.Instance.AddBelief(beliefAmount);

            // Use Decision Point Item
            List<string> combinedDialogues = new List<string>(decisionPointDialogue);
            combinedDialogues.AddRange(decisionItem.textWorldObject);
            WorldInfoTextDisplay.Instance.DisplayDialogues(combinedDialogues);

            switch (decisionItem.decisionItemType)
            {
                case DecisionItemType.Artifact:
                case DecisionItemType.Scroll:
                case DecisionItemType.Coins:
                case DecisionItemType.Runes:
                    break;

                case DecisionItemType.Hearing:
                    _playerSenseController.CollectHearingSense();
                    break;

                case DecisionItemType.HeatProtection:
                    _playerSenseController.CollectHeatResistance();
                    break;

                case DecisionItemType.ColdProtection:
                    _playerSenseController.CollectColdResistance();
                    break;

                case DecisionItemType.GrayScaleSight:
                    _playerSenseController.CollectGrayScaleSight();
                    break;

                case DecisionItemType.ColoredSight:
                    _playerSenseController.CollectColoredSight();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator ActivateBadDecisionPoint()
        {
            BeliefController.Instance.ReduceBelief(beliefAmount);

            yield return new WaitForSeconds(resetPlayerAfterTime);
            DecisionController.Instance.RevertToLastCheckPoint();
        }

        #endregion
    }
}