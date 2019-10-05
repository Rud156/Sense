using System;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace MiniMap
{
    public class MiniMapController : MonoBehaviour
    {
        [Header("Renderers")] public RectTransform miniMapRectTransform;
        public MiniMapClicker miniMapClicker;

        [Header("Target Converters")] public float worldToMapMultiplier = 0.045f;
        public float hitDistance;
        public LayerMask targetLayerMask;

        [Header("Player")] public PlayerController playerController;

        private Rect _miniMapRectangle;
        private Renderer _previousSelectedRenderer;

        #region Unity Functions

        private void Start()
        {
            miniMapClicker.OnMiniMapClicked += HandleMiniMapClicked;

            Vector3[] vertices = new Vector3[4];
            miniMapRectTransform.GetWorldCorners(vertices);

            _miniMapRectangle = new Rect
            {
                min = new Vector2(vertices[0].x, vertices[0].y),
                max = new Vector2(vertices[2].x, vertices[2].y)
            };
        }

        private void OnDestroy()
        {
            miniMapClicker.OnMiniMapClicked -= HandleMiniMapClicked;
        }

        #endregion

        #region Utility Functions

        private void HandleMiniMapClicked(PointerEventData eventData)
        {
            if (eventData.button != ControlConstants.MouseButton || !playerController.HasReachedDestination)
            {
                return;
            }

            float xDistance = _miniMapRectangle.center.x - eventData.position.x;
            float yDistance = _miniMapRectangle.center.y - eventData.position.y;

            if (Physics.Raycast(transform.position, Vector3.down, out var hit, hitDistance, targetLayerMask))
            {
                Vector3 hitPosition = hit.point;
                hitPosition.x -= (xDistance * worldToMapMultiplier);
                hitPosition.z -= (yDistance * worldToMapMultiplier);

                Vector3 rayCastPosition = new Vector3(hitPosition.x, transform.position.y, hitPosition.z);

                if (Physics.Raycast(rayCastPosition, Vector3.down, out var secondaryHit, hitDistance, targetLayerMask))
                {
                    playerController.MakePlayerMoveToDestination(secondaryHit.point);
                }
            }
        }

        #endregion
    }
}