using MenuScripts.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TheoryController : SpecificTaskController {
    public GameObject QuestionsSheet, ResultsScreen;
    public TasksController TasksScreen;
    public TheoryTaskQuizManager TaskManager;
    public Text TheoryText, QuestionsAnswered, SkillImprovedName, NewSkillValue;
    public RectTransform TextBox;
    public Text Title, TasksCount;

    private DataBaseManager.TaskData _currentTask;
    public override void OnEnter(DataBaseManager.TaskData task) {
        Title.text = Constants.SkillsNames[task.SkillPromoted];
        TheoryText.text = task.TaskText;
        TaskManager.InitializeTasks(task.Questions, task.Answers);

        _currentTask = task;
        TextBox.sizeDelta = new Vector2(TextBox.sizeDelta.x, task.TaskText.Length * Constants.ScrollCharToSizeScale);
        QuestionsSheet.SetActive(false);
        ResultsScreen.SetActive(false);
    }

    public void OnAnswerQuestionsClick() {
        QuestionsSheet.SetActive(true);
        TasksCount.text = "Вопросы: [" + _currentTask.Questions.Length + "]";
    }

    public void OnCloseQuestionsClick() {
        QuestionsSheet.SetActive(false);
    }

    public async void OnSubmitClick() {// Update user stats
        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
        var userData = updatedUserQuery.Value;
        
        var promotedSkillIndex = _currentTask.SkillPromoted;
        var correctAnswerCount = TaskManager.CorrectAnswersCount(_currentTask.CorrectAnswers);
        var newPercentage = correctAnswerCount / _currentTask.Questions.Length;
        var oldSkillValue = userData.Skills[promotedSkillIndex];
        var increase = _currentTask.SkillIncrease * newPercentage;
        var newSkillValue = oldSkillValue + increase > 100.0f ? 100.0f : oldSkillValue + increase;
        
        // Check if task was solved and update its progress if needed
        var solved = await DataBaseManager.FindSolvedTask(DataBaseManager.GetPlayerId(), _currentTask.Id);
        if (solved == null) {
            // Create entry about this solving action
            var newSolved = new DataBaseManager.SolvedTask {
                TaskId = _currentTask.Id,
                UserId = DataBaseManager.GetPlayerId(),
                TaskProgress = newPercentage
            };
            DataBaseManager.CreateSolvedTask(newSolved);
            
            // Update user stats
            userData.Skills[promotedSkillIndex] = newSkillValue;
            DataBaseManager.SaveUserData(userData);
        } else {
            var solvedTask = solved.Value;
            if (newPercentage > solvedTask.TaskProgress) {
                // Update user stats
                increase = _currentTask.SkillIncrease * (newPercentage - solvedTask.TaskProgress);
                newSkillValue = oldSkillValue + increase > 100.0f ? 100.0f : oldSkillValue + increase;
                userData.Skills[promotedSkillIndex] = newSkillValue;
                DataBaseManager.SaveUserData(userData);
                
                // Update solved status
                solvedTask.TaskProgress = newPercentage;
                DataBaseManager.UpdateSolvedData(solvedTask);
            } 
        }
        
        // Show results screen
        TaskManager.ClearQuestions();
        ResultsScreen.SetActive(true);
        
        QuestionsAnswered.text = correctAnswerCount + "/" + _currentTask.Questions.Length;
        SkillImprovedName.text = Constants.SkillsNames[promotedSkillIndex];
        NewSkillValue.text = newSkillValue.ToString(".##") + "%";
    }

    public void OnCloseResults() {
        ResultsScreen.SetActive(false);
        gameObject.SetActive(false);
        TasksScreen.gameObject.SetActive(true);
        TasksScreen.OnEnter(_currentTask.SkillPromoted);
    }
}
