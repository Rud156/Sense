using UnityEngine;

namespace PlayerDecisions
{
    public class DecisionItem : ScriptableObject
    {
        public DecisionItemType decisionItemType;

        [Header("Scrolls")] [TextArea] public string textWorldObject;

        [Header("Coins")] public int itemCount;
    }
}