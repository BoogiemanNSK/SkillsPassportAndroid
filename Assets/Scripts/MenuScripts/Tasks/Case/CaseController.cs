using UnityEngine;
using UnityEngine.UI;

public class CaseController : SpecificTaskController {

    public GameObject AbswersScreen;
    public InputField AnswerText;
    
    public Text Title, TaskText, TaskDescription;
    public TasksController TasksScreen;

    private DataBaseManager.TaskData _currentTask;
    
    public override void OnEnter(DataBaseManager.TaskData taskData) {
        Title.text = Constants.SkillsNames[taskData.SkillPromoted];
        TaskText.text = taskData.TaskText;
        TaskDescription.text = taskData.Questions[0];
        
        AbswersScreen.SetActive(false);
        _currentTask = taskData;
    }

    public void OnSubmitTask() {
        // Create answer in data base
        var newAnswer = new DataBaseManager.AnswerData {
            TaskId = _currentTask.Id,
            UserId = DataBaseManager.GetPlayerId(),
            Text = AnswerText.text
        };
        DataBaseManager.CreateAnswer(newAnswer);

        // Create entry about this solving action
        var newSolved = new DataBaseManager.SolvedTask {
            TaskId = _currentTask.Id,
            UserId = DataBaseManager.GetPlayerId(),
            TaskProgress = 1.0f
        };
        DataBaseManager.CreateSolvedTask(newSolved);

        gameObject.SetActive(false);
        TasksScreen.gameObject.SetActive(true);
        TasksScreen.OnEnter(_currentTask.SkillPromoted);
    }

    public void OnAnswerQuestionsClick() {
        AbswersScreen.SetActive(true);
    }

    public void OnCloseQuestionsClick() {
        AbswersScreen.SetActive(false);
    }
    
}
