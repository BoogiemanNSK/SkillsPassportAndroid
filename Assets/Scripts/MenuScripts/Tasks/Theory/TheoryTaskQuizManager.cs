using UnityEngine;
using UnityEngine.UI;

public class TheoryTaskQuizManager : MonoBehaviour {
    public TheoryQuizTask QuizTaskExample;
    
    private TheoryQuizTask[] _tasks;
    
    public int CorrectAnswersCount(int[][] correctAnswers) {
        var count = 0;
        for (var i = 0; i < _tasks.Length; i++) {
            count += _tasks[i].CorrectlyAnswered(correctAnswers[i]) ? 1 : 0;
        }

        return count;
    }

    public void ClearQuestions() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void InitializeTasks(string[] questions, string[][] answers) {
        _tasks = new TheoryQuizTask[questions.Length];
        for (var i = 0; i < questions.Length; i++) {
            var createdTask = Instantiate(QuizTaskExample, transform);
            createdTask.Fill(questions[i], answers[i]);
            _tasks[i] = createdTask;
        }
    }
}
