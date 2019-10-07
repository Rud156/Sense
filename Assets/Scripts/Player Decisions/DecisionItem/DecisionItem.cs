using System.Collections.Generic;
using UnityEngine;

namespace PlayerDecisions.DecisionItemData
{
    // Actually Should be A Scriptable Object
    public class DecisionItem : MonoBehaviour
    {
        public DecisionItemType decisionItemType;
        [TextArea] public List<string> textWorldObject;

        private bool _itemCollected;

        #region External Functions

        public void MarkItemAsCollected(bool collectedStatus) => _itemCollected = collectedStatus;

        public bool IsItemCollected() => _itemCollected;

        #endregion
    }
}