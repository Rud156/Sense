using Player;
using Scene.GameOver;
using UnityEngine;
using Utils;

namespace Scene.Main
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinalArtifactController : MonoBehaviour
    {
        private PlayerSenseController _playerSenseController;
        private PlayerController _playerController;

        #region Unity Funtions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                _playerController = other.GetComponent<PlayerController>();
                _playerController.StopPlayerMovement();

                _playerSenseController = other.GetComponent<PlayerSenseController>();

                CheckAndSwitchScene();
            }
        }

        #endregion

        #region Utility Functions

        private void CheckAndSwitchScene()
        {
            if (_playerSenseController.CanPlayerHear && _playerSenseController.HasColoredSight)
            {
                GameOverSceneData.PlayerCollectedAllAbilities = true;
            }
            else
            {
                GameOverSceneData.PlayerCollectedAllAbilities = false;
            }

            GameMainController.Instance.SwitchToGameOverScene();
        }

        #endregion
    }
}