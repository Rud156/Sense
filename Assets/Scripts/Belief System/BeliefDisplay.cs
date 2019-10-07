using UI;
using UnityEngine;
using UnityEngine.UI;

namespace BeliefSystem
{
    public class BeliefDisplay : MonoBehaviour
    {
        public Slider beliefSlider;
        public BeliefController beliefController;
        public ColorFlasher beliefFlasher;

        #region Unity Functions

        private void Start()
        {
            beliefController.OnBeliefChanged += HandleBeliefAmountChanged;
        }

        private void OnDestroy()
        {
            beliefController.OnBeliefChanged -= HandleBeliefAmountChanged;
        }

        #endregion

        #region Utility Functions

        private void HandleBeliefAmountChanged(float currentBeliefAmount, float maxBeliefAmount)
        {
            float displayRatio = currentBeliefAmount / maxBeliefAmount;
            beliefSlider.value = displayRatio;

            beliefFlasher.StartFlashing();
        }

        #endregion
    }
}