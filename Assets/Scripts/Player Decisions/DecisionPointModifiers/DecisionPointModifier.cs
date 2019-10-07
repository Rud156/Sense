using System;
using Player;
using PlayerDecisions.DecisionItemData;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class DecisionPointModifier : MonoBehaviour
    {
        public virtual bool AffectPlayer(Vector3 playerPosition, PlayerController playerController, DecisionItem decisionItem)
        {
            return true;
        }

        public virtual void ResetModifier()
        {
        }
    }
}