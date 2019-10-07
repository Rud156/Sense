using System;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class DecisionPointModifier : MonoBehaviour
    {
        public virtual bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            return true;
        }

        public virtual void ResetModifier()
        {
        }
    }
}