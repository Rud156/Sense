using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniMap
{
    public class MiniMapClicker : MonoBehaviour, IPointerClickHandler
    {
        public delegate void MiniMapClicked(PointerEventData eventData);
        public MiniMapClicked OnMiniMapClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnMiniMapClicked?.Invoke(eventData);
        }
    }
}