using System.Collections.Generic;
using UnityEngine;

namespace PlayerDecisions.DecisionItemData
{
    public class DecisionItem : ScriptableObject
    {
        public DecisionItemType decisionItemType;

        [Header("Scrolls")] [TextArea] public List<string> textWorldObject;
    }
}