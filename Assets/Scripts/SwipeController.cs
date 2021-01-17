using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour {
    public HorizontalLayoutGroup SwipeGroup;
    public RectTransform ElementExampleRect;
    public float SwipeDetectionPercent = 0.2f;
    public int ElementIndex;

    private Vector2 _fingerDown, _fingerUp;
    private float _spacing, _elementWidth;
    private RectTransform _rectTransform;

    private void Start() {
        _rectTransform = SwipeGroup.GetComponent<RectTransform>();
        _elementWidth = ElementExampleRect.rect.width;
        _spacing = SwipeGroup.spacing;

        SetActiveElement(0);
    }

    private void Update() {
        foreach (var touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                _fingerUp = touch.position;
                _fingerDown = touch.position;
            }

            // Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended) {
                _fingerDown = touch.position;
                OnCheckSwipe();
            }
        }
    }
    
    public void SwipeToLeft() {
        if (ElementIndex > 0) {
            SetActiveElement(ElementIndex - 1);
        }
    }

    public void SwipeToRight() {
        var elementsCount = SwipeGroup.gameObject.transform.childCount;
        if (ElementIndex < elementsCount - 1) {
            SetActiveElement(ElementIndex + 1);
        }
    }

    private void OnCheckSwipe() {
        var elementsCount = SwipeGroup.gameObject.transform.childCount;
        var horizontalValMove = Mathf.Abs(_fingerDown.x - _fingerUp.x);
        if (horizontalValMove > Screen.width * SwipeDetectionPercent) {
            if (_fingerDown.x - _fingerUp.x > 0 && ElementIndex > 0) { // Right swipe
                SetActiveElement(ElementIndex - 1);
            } else if (_fingerDown.x - _fingerUp.x < 0 && ElementIndex < elementsCount - 1) { // Left swipe
                SetActiveElement(ElementIndex + 1);
            }
            _fingerUp = _fingerDown;
        }
    }

    private void SetActiveElement(int index) {
        ElementIndex = index;
        var newX = -1 * index * (_elementWidth + _spacing);
        var localPosition = _rectTransform.localPosition;
        localPosition = new Vector3(newX, localPosition.y, localPosition.z);
        _rectTransform.localPosition = localPosition;
    }
}
