using Player;
using UnityEngine;
using Utils;

namespace PlayerDecisions
{
    public class DecisionController : MonoBehaviour
    {
        [Header("Control Display")] public Animator leftArrowAnimator;
        public Animator rightArrowAnimator;
        public Animator topArrowAnimator;
        public Animator bottomArrowAnimator;

        [Header("Player")] public PlayerController playerController;

        private bool _leftArrowAllowed;
        private bool _rightArrowAllowed;
        private bool _topArrowAllowed;
        private bool _bottomArrowAllowed;

        private static readonly int EnableParam = Animator.StringToHash("Enabled");

        private DecisionPoint _currentDecisionPoint;

        #region Unity Functions

        private void Start() => ClearAllDecisionData();

        private void Update()
        {
            if (Input.GetButtonDown(ControlConstants.LeftButton) && _leftArrowAllowed)
            {
                playerController.MakePlayerMoveToDestination(_currentDecisionPoint.LeftDecisionPoint.GetDecisionPointPosition());
            }
            else if (Input.GetButtonDown(ControlConstants.RightButton) && _rightArrowAllowed)
            {
                playerController.MakePlayerMoveToDestination(_currentDecisionPoint.RightDecisionPoint.GetDecisionPointPosition());
            }
            else if (Input.GetButtonDown(ControlConstants.TopButton) && _topArrowAllowed)
            {
                playerController.MakePlayerMoveToDestination(_currentDecisionPoint.TopDecisionPoint.GetDecisionPointPosition());
            }
            else if (Input.GetButtonDown(ControlConstants.BottomButton) && _bottomArrowAllowed)
            {
                playerController.MakePlayerMoveToDestination(_currentDecisionPoint.BottomDecisionPoint.GetDecisionPointPosition());
            }
        }

        #endregion

        #region External Functions

        public void RegisterDecisionPoint(DecisionPoint decisionPoint) => _currentDecisionPoint = decisionPoint;

        public void ActivateLeftArrow()
        {
            _leftArrowAllowed = true;
            leftArrowAnimator.SetBool(EnableParam, true);
        }

        public void ActivateRightArrow()
        {
            _rightArrowAllowed = true;
            rightArrowAnimator.SetBool(EnableParam, true);
        }

        public void ActivateTopArrow()
        {
            _topArrowAllowed = true;
            topArrowAnimator.SetBool(EnableParam, true);
        }

        public void ActivateBottomArrow()
        {
            _bottomArrowAllowed = true;
            bottomArrowAnimator.SetBool(EnableParam, true);
        }

        public void ClearAllDecisionData()
        {
            leftArrowAnimator.SetBool(EnableParam, false);
            rightArrowAnimator.SetBool(EnableParam, false);
            topArrowAnimator.SetBool(EnableParam, false);
            bottomArrowAnimator.SetBool(EnableParam, false);

            _currentDecisionPoint = null;
        }

        #endregion

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