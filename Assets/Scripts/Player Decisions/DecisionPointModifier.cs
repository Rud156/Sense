using UnityEngine;

namespace PlayerDecisions
{
    public abstract class DecisionPointModifier : MonoBehaviour
    {
        protected abstract void AffectPlayer();
    }
}