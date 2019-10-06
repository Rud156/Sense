using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers

{
    public class FallDeath : DecisionPointModifier
    {
        public Vector3 movePlayerToPoint;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            playerController.PlayFallDeathAnimation(movePlayerToPoint);

            return false;
        }
    }
}