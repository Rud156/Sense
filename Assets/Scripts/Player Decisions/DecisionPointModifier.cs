using System;
using Player;
using UnityEngine;

namespace PlayerDecisions
{
    public abstract class DecisionPointModifier : MonoBehaviour
    {
        public abstract bool AffectPlayer(Vector3 playerPosition, PlayerController playerController);

        public virtual void ResetModifier()
        {
        }
    }
}