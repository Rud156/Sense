using PlayerDecisions;
using PlayerDecisions.DecisionItemData;
using UnityEngine;

namespace Items
{
    public class ItemsCollectibleController : MonoBehaviour
    {
        #region External Functions

        public void RemoveItemFromPlayerInventory(DecisionItem decisionItem)
        {
            // TODO: Implement this function
        }

        #endregion

        #region Singleton

        private static ItemsCollectibleController _instance;

        public static ItemsCollectibleController Instance => _instance;

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