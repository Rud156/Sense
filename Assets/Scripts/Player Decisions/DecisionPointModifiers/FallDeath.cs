using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers

{
    public class FallDeath : DecisionPointModifier
    {
        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            playerController.PlayFallDeathAnimation();

            return false;
        }
    }
}