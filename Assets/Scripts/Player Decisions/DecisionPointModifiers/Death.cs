using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class Death : DecisionPointModifier
    {
        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            playerController.PlayDeathAnimation();

            return false;
        }
    }
}