using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextTyper : MonoBehaviour
    {
        [TextArea] public string displayText;
        public float characterDelay;

        public delegate void TypingCompleted();
        public TypingCompleted OnTypingCompleted;

        private bool _isTyping;
        private float _currentTypingTimer;
        private int _currentCharacterIndex;

        private TextMeshProUGUI displayObject;

        #region Unity Functions

        private void Start() => displayObject = GetComponent<TextMeshProUGUI>();

        private void Update()
        {
            if (!_isTyping)
            {
                return;
            }

            _currentTypingTimer -= Time.deltaTime;
            if (_currentTypingTimer <= 0)
            {
                AddText(displayText[_currentCharacterIndex]);
                _currentTypingTimer = characterDelay;

                _currentCharacterIndex += 1;
                if (_currentCharacterIndex >= displayText.Length)
                {
                    StopTyping();
                }
            }
        }

        #endregion

        #region External Functions

        public void StartTyping()
        {
            SetText("");
            _isTyping = true;
            _currentTypingTimer = 0;
            _currentCharacterIndex = 0;
        }

        public void ForceComplete()
        {
            SetText(displayText);
            StopTyping();
        }

        public void UpdateText(string text) => displayText = text;

        #endregion

        #region Utility Functions

        private void StopTyping()
        {
            _isTyping = false;
            NotifyTypingCompleted();
        }

        private void NotifyTypingCompleted() => OnTypingCompleted?.Invoke();

        private void SetText(string text) => displayObject.text = text;

        private void AddText(string text) => displayObject.text += text;

        private void AddText(char character) => displayObject.text += character;

        #endregion
    }
}