using Audio;
using Player;
using UnityEngine;

namespace PlayerDecisions.DecisionModifiers
{
    public class Death : DecisionPointModifier
    {
        public AudioClip deathAudioClip;

        public override bool AffectPlayer(Vector3 playerPosition, PlayerController playerController)
        {
            SfxAudioManager.Instance.PlaySound(deathAudioClip);
            playerController.PlayDeathAnimation();

            return false;
        }
    }
}