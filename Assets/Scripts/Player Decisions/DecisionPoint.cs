﻿using System;
using System.Collections.Generic;
using BeliefSystem;
using Player;
using PlayerDecisions.DecisionItemData;
using PlayerDecisions.DecisionModifiers;
using UI;
using UnityEngine;
using Utils;
using World;

namespace PlayerDecisions
{
    public class DecisionPoint : MonoBehaviour
    {
        [Header("Nearby Decision Points")] public DecisionPoint leftPoint;
        public DecisionPoint rightPoint;
        public DecisionPoint topPoint;
        public DecisionPoint bottomPoint;

        [Header("Point Data")] public Transform decisionPointPosition;
        public float beliefAmount;
        public DecisionPointWorldType decisionPointWorldType;
        public int resistanceLessBeliefReduceAmount = 5;

        [Header("Items")] [TextArea] public List<string> decisionPointDialogue;
        public float resetPlayerAfterTime = 7;

        private CollisionNotifier _collisionNotifier;

        private DecisionItem _decisionItem;
        private DecisionPointModifier _decisionPointModifier;
        private PlayerController _playerController;
        private PlayerSenseController _playerSenseController;

        #region Unity Functions

        private void Start()
        {
            _decisionPointModifier = GetComponent<DecisionPointModifier>();
            _decisionItem = GetComponent<DecisionItem>();
            _collisionNotifier = GetComponentInChildren<CollisionNotifier>();

            _collisionNotifier.OnTriggerEntered += HandleOnTriggerEnter;
            _collisionNotifier.OnTriggerExited += HandleOnTriggerExit;

            // Debug Check for Position
            if (decisionPointPosition == null)
            {
                Debug.LogError($"Warning No Decision Position: {gameObject.name}");
            }
        }

        private void OnDestroy()
        {
            _collisionNotifier.OnTriggerEntered -= HandleOnTriggerEnter;
            _collisionNotifier.OnTriggerExited -= HandleOnTriggerExit;
        }

        private void HandleOnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<PlayerController>();
                _playerSenseController = other.GetComponent<PlayerSenseController>();
                ActivateDecisionPoint();
            }
        }

        private void HandleOnTriggerExit(Collider other)
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

        public DecisionItem GetDecisionItem() => _decisionItem;

        public DecisionPointModifier GetDecisionPointModifier() => _decisionPointModifier;

        #endregion

        #region Utility Functions

        private void ActivateDecisionPoint()
        {
            bool isSafeDecisionPoint = _decisionPointModifier.AffectPlayer(_playerController.transform.position, _playerController, _decisionItem);
            if (isSafeDecisionPoint)
            {
                _playerController.StopPlayerMovement();
                ActivateGoodDecisionPoint();
            }
            else
            {
                _playerController.StopPlayerMovement(true);
                ActivateBadDecisionPoint();
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

            if (!_decisionItem.IsItemCollected())
            {
                BeliefController.Instance.AddBelief(beliefAmount);
            }

            // Use Decision Point Item
            List<string> combinedDialogues = new List<string>(decisionPointDialogue);
            if (!_decisionItem.IsItemCollected())
            {
                combinedDialogues.AddRange(_decisionItem.textWorldObject);
            }

            WorldInfoTextDisplay.Instance.DisplayDialogues(combinedDialogues);

            switch (_decisionItem.decisionItemType)
            {
                case DecisionItemType.Artifact:
                case DecisionItemType.Scroll:
                case DecisionItemType.Coins:
                case DecisionItemType.Runes:
                    break;

                case DecisionItemType.Hearing:
                    _playerSenseController.CollectHearingSense();
                    BeliefController.Instance.ReduceBelief(beliefAmount * 2); // Hacky Fix
                    break;

                case DecisionItemType.HeatProtection:
                    _playerSenseController.CollectHeatResistance();
                    break;

                case DecisionItemType.ColdProtection:
                    _playerSenseController.CollectColdResistance();
                    break;

                case DecisionItemType.GrayScaleSight:
                    _playerSenseController.CollectGrayScaleSight();
                    BeliefController.Instance.ReduceBelief(beliefAmount * 2); // Hacky Fix
                    break;

                case DecisionItemType.ColoredSight:
                    _playerSenseController.CollectColoredSight();
                    BeliefController.Instance.ReduceBelief(beliefAmount);
                    break;

                case DecisionItemType.Death:
                case DecisionItemType.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _decisionItem.MarkItemAsCollected(true);
        }

        private void ActivateBadDecisionPoint()
        {
            BeliefController.Instance.ReduceBelief(beliefAmount);
            DecisionController.Instance.FadeScreenOut();
        }

        #endregion
    }
}