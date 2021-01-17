using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartQuizTasksManager : MonoBehaviour {
    public SwipeController TasksSwipeController;
    public IntroQuizTask QuizTaskExample;
    public IntroController Menu;
    
    public GameObject AnswerAllTextTip;
    public Transform QuestionsTable;
    public Button SubmitAllButton;
    public Text TaskCounter;

    public int[] QAnswers;
    public int[] SkillPointsGathered;

    private HashSet<int> _submittedTasks;
    private int _totalTasksCount;

    private void Start() {
        _totalTasksCount = Constants.IntroQuizQuestions.Length * Constants.IntroQuizQuestions[0].Length;
        _submittedTasks = new HashSet<int>();
        
        SkillPointsGathered = new int[Constants.IntroQuizQuestions.Length];
        QAnswers = new int[_totalTasksCount];
        SubmitAllButton.interactable = false;
        
        InitializeTasks();
    }

    private void Update() {
        TaskCounter.text = (TasksSwipeController.ElementIndex + 1) + "/" + Constants.IntroQuizQuestions.Length;
    }

    public void SubmitTask(int qIndex, int qAnswerIndex) {
        QAnswers[qIndex] = qAnswerIndex;
        _submittedTasks.Add(qIndex);
        if (_submittedTasks.Count == _totalTasksCount) {
            AnswerAllTextTip.SetActive(false);
            SubmitAllButton.interactable = true;
        }
    }

    public async void SubmitAll() {
        for (var i = 0; i < _totalTasksCount; i++) {
            SkillPointsGathered[Constants.IntroQuizSkillsPromoted[i]] += QAnswers[i];
        }
        
        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
        var updatedUser = updatedUserQuery.Value;
        
        for (var i = 0; i < updatedUser.Skills.Length; i++) {
            var inPercents = (SkillPointsGathered[i] / (float)Constants.IntroMAXPointsForSkill) * 100.0f;
            updatedUser.Skills[i] = inPercents;
        }
        
        DataBaseManager.SaveUserData(updatedUser);
        Menu.ShowResults();
    }

    private void InitializeTasks() {
        var iCounter = 0;
        foreach (var t in Constants.IntroQuizQuestions) {
            var tableTransform = Instantiate(QuestionsTable, transform);
            foreach (var t1 in t) {
                var createdTask = Instantiate(QuizTaskExample, tableTransform);
                createdTask.LocalTasksManager = this;
                createdTask.QuestionText.text = t1;
                createdTask.QIndex = iCounter;
                iCounter++;
            }
        }
    }
}
