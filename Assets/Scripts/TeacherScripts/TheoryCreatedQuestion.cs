using System;
using UnityEngine;
using UnityEngine.UI;

public class TheoryCreatedQuestion : MonoBehaviour {

    public Transform AnswersPanel;
    public InputField QuestionText, AnswersAmount;
    public TheoryCreatedAnswer CreatedAnswerExample;

    private TheoryCreatedAnswer[] _answers;
    private int _answersAmount;

    public void OnGenerateAnswers() {
        _answersAmount = Convert.ToInt32(AnswersAmount.text);
        _answers = new TheoryCreatedAnswer[_answersAmount];

        for (var i = 0; i < _answersAmount; i++) {
            var newAnswer = Instantiate(CreatedAnswerExample, AnswersPanel);
            _answers[i] = newAnswer;
        }
    }

    public string CollectQuestion() {
        return QuestionText.text;
    }
    
    public string[] CollectAnswers() {
        var answersArray = new string[_answersAmount];
        for (var i = 0; i < _answersAmount; i++) {
            answersArray[i] = _answers[i].AnswerText.text;
        }

        return answersArray;
    }
    
    public int[] CollectCorrectAnswers() {
        var correctAnswers = 0;
        for (var i = 0; i < _answersAmount; i++) {
            if (_answers[i].IsCorrect.isOn) {
                correctAnswers++;
            }
        }

        var j = 0;
        var correctAnswersArray = new int[correctAnswers];
        for (var i = 0; i < _answersAmount; i++) {
            if (_answers[i].IsCorrect.isOn) {
                correctAnswersArray[j++] = i;
            }
        }

        return correctAnswersArray;
    }

}
