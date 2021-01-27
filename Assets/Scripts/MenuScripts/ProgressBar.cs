using System;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class ProgressBar : MonoBehaviour {
    public RectTransform Requirement, Background, Bar;

    public Image Progress;

    public float HorizontalOffset = 12;

    public void SetProgress(float skillValue, float requiredValue) {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        var sizeDelta = Background.sizeDelta;
        var aspect = Camera.main.aspect;
        
        sizeDelta = new Vector2((float) (sizeDelta.x - Math.Sqrt(HorizontalOffset / aspect)), sizeDelta.y);
        Background.sizeDelta = sizeDelta;

        skillValue /= 100.0f;
        requiredValue /= 100.0f;
        var rect = Bar.rect;
        var maxWidth = rect.width;
        
        var position = Requirement.localPosition;
        position = new Vector3((float) (requiredValue * maxWidth - Math.Sqrt(HorizontalOffset / aspect)), position.y, position.z);
        Requirement.localPosition = position;
        
        Bar.sizeDelta = new Vector2((float) (skillValue * maxWidth - Math.Sqrt(HorizontalOffset / aspect)), Bar.sizeDelta.y);
        Progress.color = skillValue >= requiredValue ? Color.green : Color.yellow;
    }
}
