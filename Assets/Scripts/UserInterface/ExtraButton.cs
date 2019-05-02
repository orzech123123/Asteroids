using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class ExtraButton : Button
    {
        public delegate void PointerDownEvent();
        public event PointerDownEvent onPointerDown;
        public delegate void PointerUpEvent();
        public event PointerUpEvent onPointerUp;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (onPointerDown != null)
            {
                onPointerDown();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (onPointerUp != null)
            {
                onPointerUp();
            }
        }
    }
}