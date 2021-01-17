using UnityEngine;
using UnityEngine.UI;

public class TheoryQuizAnswer : MonoBehaviour {

    public Toggle AnswerToggle;
    public Text AnswerText;

    public void Fill(string answerText) {
        AnswerToggle.isOn = false;
        AnswerText.text = answerText + ";";
    }

    public bool IsChecked() {
        return AnswerToggle.isOn;
    }

}
