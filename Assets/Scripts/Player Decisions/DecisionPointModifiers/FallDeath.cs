using Player;
using PlayerDecisions.DecisionItemData;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers

{
    public class FallDeath : DecisionPointModifier
    {
        public Vector3 movePlayerToPoint;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController, DecisionItem decisionItem)
        {
            playerController.PlayFallDeathAnimation(movePlayerToPoint);

            return false;
        }
    }
}