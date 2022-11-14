using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Swipes
{ Up, Down, Left, Right }

namespace Aezakmi
{
    public class InputManager : GloballyAccessibleBase<InputManager>
    {
        [HideInInspector] public bool IsTouching { get { return Input.GetMouseButton(0); } }
        [HideInInspector] public bool isClickingUI;

        [HideInInspector] public Vector2 startPosition;
        [HideInInspector] public Vector2 currentPosition;
        [HideInInspector] public Vector2 endPosition;

        [SerializeField] public Swipes? swipeDirection = null;

        private void Update()
        {
            swipeDirection = null;

            if (!IsTouching)
                return;

            isClickingUI = IsPointerOverUIObject();
            DetectTouchPositions();
        }

        private void DetectTouchPositions()
        {
            currentPosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
                startPosition = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                endPosition = Input.mousePosition;
                DetectSwipes();
            }
        }

        private void DetectSwipes()
        {
            Vector2 _swipeDirection = (endPosition - startPosition).normalized;

            float positiveX = Mathf.Abs(_swipeDirection.x);
            float positiveY = Mathf.Abs(_swipeDirection.y);

            if (positiveX > positiveY)
            {
                swipeDirection = (_swipeDirection.x > 0) ? Swipes.Right : Swipes.Left;
            }
            else
            {
                swipeDirection = (_swipeDirection.y > 0) ? Swipes.Up : Swipes.Down;
            }
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
