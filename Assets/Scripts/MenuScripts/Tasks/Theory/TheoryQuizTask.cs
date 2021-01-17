using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TheoryQuizTask : MonoBehaviour {

    public Text Question;
    public Transform AnswersField;

    public TheoryQuizAnswer AnswerExample;

    private TheoryQuizAnswer[] _answers;

    public void Fill(string questionText, string[] answers) {
        Question.text = questionText;
        _answers = new TheoryQuizAnswer[answers.Length];

        var i = 0;
        foreach (var answerText in answers) {
            var answerObject = Instantiate(AnswerExample, AnswersField);
            answerObject.Fill(answerText);
            _answers[i++] = answerObject;
        }
    }

    public bool CorrectlyAnswered(int[] correctAnswers) {
        for (var i = 0; i < _answers.Length; i++) {
            var isCorrect = ContainsValue(i, correctAnswers);
            if (_answers[i].IsChecked() && !isCorrect || !_answers[i].IsChecked() && isCorrect) {
                return false;
            }
        }

        return true;
    }

    private static bool ContainsValue(int value, IEnumerable<int> array) {
        return array.Any(val => val == value);
    }

}
