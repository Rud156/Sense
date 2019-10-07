using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WorldInfoTextDisplay : MonoBehaviour
    {
        public TextTyper textTyper;
        public Animator textTyperAnimator;
        public float dialogueWaitTime;

        private List<string> _currentDialogues;
        private float _currentDialogueWaitTime;
        private int _currentDialogueIndex;
        private bool _dialogueActive;
        private bool _dialogueComplete;

        private static readonly int DisplayParam = Animator.StringToHash("Display");

        #region Unity Functions

        private void Start() => textTyper.OnTypingCompleted += HandleTextTypingComplete;

        private void OnDestroy() => textTyper.OnTypingCompleted -= HandleTextTypingComplete;

        private void Update()
        {
            if (!_dialogueActive)
            {
                return;
            }

            if (_currentDialogueWaitTime > 0 && _dialogueComplete)
            {
                _currentDialogueWaitTime -= Time.deltaTime;

                if (_currentDialogueWaitTime <= 0)
                {
                    CheckAndSwitchToNextDialogue();
                }
            }
        }

        #endregion

        #region External Functions

        public void DisplayDialogues(List<string> dialogues)
        {
            if (dialogues.Count <= 0)
            {
                return;
            }

            _currentDialogues = dialogues;
            _currentDialogueIndex = 0;
            _currentDialogueWaitTime = dialogueWaitTime;

            _dialogueActive = true;
            _dialogueComplete = false;
        }

        #endregion

        #region Utility Functions

        private void HandleTextTypingComplete()
        {
            _currentDialogueWaitTime = dialogueWaitTime;
            _dialogueComplete = true;
        }

        private void CheckAndSwitchToNextDialogue()
        {
            _currentDialogueIndex += 1;
            _dialogueComplete = false;

            if (_currentDialogueIndex >= _currentDialogues.Count)
            {
                StopAndClearDialogues();
            }
            else
            {
                textTyper.UpdateText(_currentDialogues[_currentDialogueIndex]);
                textTyper.StartTyping();
            }
        }

        private void StopAndClearDialogues()
        {
            _dialogueActive = false;
            textTyperAnimator.SetBool(DisplayParam, false);
        }

        #endregion

        #region Singleton

        private static WorldInfoTextDisplay _instance;
        public static WorldInfoTextDisplay Instance => _instance;

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