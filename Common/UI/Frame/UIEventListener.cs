using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chaopeng.QiTianXuan.UIFrame
{
    public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public event Action<PointerEventData> OnPointerClickEvent;
        public event Action<PointerEventData> OnPointerDownEvent;
        public event Action<PointerEventData> OnPointerEnterEvent;
        public event Action<PointerEventData> OnPointerExitEvent;
        public event Action<PointerEventData> OnPointerUpEvent;




         void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke(eventData);
        }

         void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke(eventData);
        }

         void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke(eventData);

        }

         void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke(eventData);
 

        }
    }
}
