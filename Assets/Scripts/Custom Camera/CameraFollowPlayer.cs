using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace CustomCamera
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        public float lerpSpeed;
        public Vector3 playerOffset;

        private Transform _player;

        private Vector3 _currentPosition;

        #region Unity Functions

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
            _currentPosition = transform.position;
        }

        private void Update()
        {
            if (!_player)
            {
                return;
            }

            Vector3 finalTargetPosition = _player.position;
            finalTargetPosition += playerOffset;

            _currentPosition = Vector3.Lerp(_currentPosition, finalTargetPosition, lerpSpeed * Time.deltaTime);
            transform.position = _currentPosition;
        }

        #endregion
    }
}