using UnityEngine;
using UnityEngine.UI;

public class PracticeController : SpecificTaskController {

    public GameObject ResultsScreen;
    public Text ImprovedSkill, NewSkillValue;
    
    public Text Title, TaskText;
    public TasksController TasksScreen;

    private DataBaseManager.TaskData _currentTask;
    
    public override void OnEnter(DataBaseManager.TaskData taskData) {
        Title.text = Constants.SkillsNames[taskData.SkillPromoted];
        TaskText.text = taskData.TaskText;
        
        ResultsScreen.SetActive(false);
        _currentTask = taskData;
    }

    public async void OnSubmitTask() {
        // Create entry about this solving action
        var newSolved = new DataBaseManager.SolvedTask {
            TaskId = _currentTask.Id,
            UserId = DataBaseManager.GetPlayerId(),
            TaskProgress = 1.0f
        };
        DataBaseManager.CreateSolvedTask(newSolved);
        
        // Update user stats
        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
        var userData = updatedUserQuery.Value;
        
        var increase = _currentTask.SkillIncrease;
        var promotedSkillIndex = _currentTask.SkillPromoted;
        var oldSkillValue = userData.Skills[promotedSkillIndex];
        var newSkillValue = oldSkillValue + increase > 100.0f ? 100.0f : oldSkillValue + increase;

        userData.Skills[promotedSkillIndex] = newSkillValue;
        DataBaseManager.SaveUserData(userData);
        
        // Show results screen
        ResultsScreen.SetActive(true);
        ImprovedSkill.text = Constants.SkillsNames[promotedSkillIndex];
        NewSkillValue.text = newSkillValue.ToString(".##") + "%";
    }

    public void OnSkipTask() {
        gameObject.SetActive(false);
        TasksScreen.gameObject.SetActive(true);
        TasksScreen.OnEnter(_currentTask.SkillPromoted);
    }

    public void OnCloseResults() {
        ResultsScreen.SetActive(false);
        gameObject.SetActive(false);
        TasksScreen.gameObject.SetActive(true);
        TasksScreen.OnEnter(_currentTask.SkillPromoted);
    }
    
}
