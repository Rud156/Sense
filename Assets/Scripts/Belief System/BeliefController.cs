using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeliefSystem
{
    public class BeliefController : MonoBehaviour
    {
        public float initialBelief;
        public float maxBelief;

        public delegate void BeliefChanged(float currentBelief, float maxBelief);

        public BeliefChanged OnBeliefChanged;

        private float _currentBelief;
        private float _maxBelief;

        #region Unity Functions

        private void Start() => ForceSetBelief();

        #endregion

        #region External Functions

        public float CurrentBeliefAmount => _currentBelief;

        public float MaxBeliefAmount => _maxBelief;

        public void AddBelief(float beliefAmount)
        {
            if (beliefAmount == 0)
            {
                return;
            }

            if (_currentBelief + beliefAmount > _maxBelief)
            {
                _currentBelief = _maxBelief;
            }
            else
            {
                _currentBelief += beliefAmount;
            }

            NotifyBeliefChanged();
        }

        public void ReduceBelief(float beliefAmount)
        {
            if (beliefAmount == 0)
            {
                return;
            }

            if (_currentBelief - beliefAmount < initialBelief)
            {
                _currentBelief = initialBelief;
            }

            else
            {
                _currentBelief -= beliefAmount;
            }

            NotifyBeliefChanged();
        }

        #endregion

        #region Utility Functions

        private void ForceSetBelief()
        {
            _currentBelief = initialBelief;
            _maxBelief = maxBelief;

            NotifyBeliefChanged();
        }

        private void NotifyBeliefChanged()
        {
            OnBeliefChanged?.Invoke(_currentBelief, _maxBelief);
        }

        #endregion

        #region Singleton

        private static BeliefController _instance;
        public static BeliefController Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}