using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class ItemFallFromTop : DecisionPointModifier
    {
        public List<Rigidbody> fallItems;
        public float makeItemsDestroyAfterTime;
        public GameObject destroyEffectPrefab;

        private List<Vector3> _initialItemsPositions;
        private float _currentFallTime;
        private bool _itemsThrown;

        #region Unity Functions

        private void Start()
        {
            _initialItemsPositions = new List<Vector3>();

            foreach (Rigidbody fallItem in fallItems)
            {
                _initialItemsPositions.Add(fallItem.position);
            }
        }

        private void Update()
        {
            if (!_itemsThrown)
            {
                return;
            }

            _currentFallTime -= Time.deltaTime;
            if (_currentFallTime <= 0)
            {
                SpawnEffectAndRemoveItems();
                _itemsThrown = false;
            }
        }

        #endregion

        #region External Functions

        public override void ResetModifier()
        {
            base.ResetModifier();

            for (int i = 0; i < fallItems.Count; i++)
            {
                Vector3 initialPosition = _initialItemsPositions[i];
                fallItems[i].position = initialPosition;
                fallItems[i].gameObject.SetActive(true);
            }
        }

        #endregion

        #region Utility Functions

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            foreach (Rigidbody fallItem in fallItems)
            {
                fallItem.isKinematic = false;
            }

            _itemsThrown = true;
            _currentFallTime = makeItemsDestroyAfterTime;

            return false;
        }

        private void SpawnEffectAndRemoveItems()
        {
            foreach (Rigidbody fallItem in fallItems)
            {
                Vector3 itemPosition = fallItem.position;
                Instantiate(destroyEffectPrefab, itemPosition, Quaternion.identity);

                fallItem.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}