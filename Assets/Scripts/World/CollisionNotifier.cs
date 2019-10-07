using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class CollisionNotifier : MonoBehaviour
    {
        public delegate void TriggerEntered(Collider other);
        public delegate void TriggerExited(Collider other);

        public TriggerEntered OnTriggerEntered;
        public TriggerExited OnTriggerExited;

        #region Unity Functions

        private void OnTriggerEnter(Collider other) => OnTriggerEntered?.Invoke(other);

        private void OnTriggerExit(Collider other) => OnTriggerExited?.Invoke(other);

        #endregion
    }
}