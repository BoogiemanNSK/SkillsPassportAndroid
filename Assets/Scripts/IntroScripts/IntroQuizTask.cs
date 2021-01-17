using UnityEngine;
using UnityEngine.UI;

public class IntroQuizTask : MonoBehaviour {
    public StartQuizTasksManager LocalTasksManager;
    public Toggle[] T;
    
    public Text QuestionText;
    
    public int QIndex;

    public void OnToggleClick(int qAnswerIndex) {
        if (T[qAnswerIndex].isOn)
            LocalTasksManager.SubmitTask(QIndex, qAnswerIndex);
    }
}
