using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerDecisions
{
    public class DecisionController : MonoBehaviour
    {
        [Header("Control Display")] public Image leftArrowImage;
        public Image rightArrowImage;
        public Image topArrowImage;
        public Image bottomArrowImage;



        #region Singleton

        private static DecisionController _instance;

        public static DecisionController Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}