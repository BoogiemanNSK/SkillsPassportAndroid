using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TasksController : MonoBehaviour {
    public MainMenuController Menu;
    public SpecificTaskController[] Tasks;
    
    public Text Title, SkillDescription, TipOfTheDay;
    public Text[] TaskName, TaskTip, TaskTimer;
    public Button[] TaskBtn;

    private bool[] _taskLocked;
    private bool _readyForUpdate;
    private int[] _timeStamps;
    private int _currentSkill;

    private void Start() {
        _readyForUpdate = false;
        _taskLocked = new[] { false, false, false };
    }

    private void Update() {
        // Timer check
        if (!_readyForUpdate) { return; }
        var currentTimeStamp = (int)DateTime.Now.Subtract(Constants.BeginingOfTime).TotalSeconds;

        for (var i = 0; i < _timeStamps.Length; i++) {
            if (currentTimeStamp - _timeStamps[i] > Constants.TimerIntervalSeconds[i]) {
                UnlockTaskButton(i);
            } else {
                LockTaskButton(i);
            }
        }
    }

    public async void OnEnter(int clickedSkill) {
        var tipIndex = new Random().Next(Constants.TipsOfTheDay[clickedSkill].Length);
        Title.text = Constants.SkillsNames[clickedSkill];
        SkillDescription.text = Constants.SkillsDescription[clickedSkill];
        TipOfTheDay.text = Constants.TipsOfTheDay[clickedSkill][tipIndex];
        _currentSkill = clickedSkill;
        
        var userQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(userQuery != null, nameof(userQuery) + " != null");
        _timeStamps = userQuery.Value.TimeStamps;

        _readyForUpdate = true;
    }
    
    public async void OnTaskClick(int clickedTaskType) {
        LockTaskButton(clickedTaskType);
        
        // Set appropriate timer value
        _timeStamps[clickedTaskType] = (int)DateTime.Now.Subtract(Constants.BeginingOfTime).TotalSeconds;
        UpdateRemoteTimeStamps();
        
        // Create random task for skill
        var randomTask = await GetRandomTask(clickedTaskType, _currentSkill);
        if (randomTask == null) {
            Debug.Log("No such tasks left!");
            return;
        }
        
        Tasks[clickedTaskType].gameObject.SetActive(true);
        Tasks[clickedTaskType].OnEnter(randomTask.Value);
        _readyForUpdate = false;
        gameObject.SetActive(false);
    }

    public void OnBackClick() {
        _readyForUpdate = false;
        
        Menu.gameObject.SetActive(true);
        Menu.OnEnter();
        gameObject.SetActive(false);
    }

    private void UnlockTaskButton(int taskType) {
        if (!_taskLocked[taskType]) {
            return;
        }
        _taskLocked[taskType] = false;
        
        TaskName[taskType].gameObject.SetActive(true);
        TaskTip[taskType].gameObject.SetActive(true);
        TaskTimer[taskType].gameObject.SetActive(false);
        TaskBtn[taskType].interactable = true;
    }
    
    private void LockTaskButton(int taskType) {
        if (_taskLocked[taskType]) {
            TaskTimer[taskType].text = FormatTime(Constants.TimerIntervalSeconds[taskType], _timeStamps[taskType]);
            return;
        }
        _taskLocked[taskType] = true;
        
        TaskName[taskType].gameObject.SetActive(false);
        TaskTip[taskType].gameObject.SetActive(false);
        TaskTimer[taskType].gameObject.SetActive(true);
        TaskTimer[taskType].text = FormatTime(Constants.TimerIntervalSeconds[taskType], _timeStamps[taskType]);
        TaskBtn[taskType].interactable = false;
    }

    private async void UpdateRemoteTimeStamps() {
        var userQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(userQuery != null, nameof(userQuery) + " != null");
        var user = userQuery.Value;
        user.TimeStamps = _timeStamps;
        DataBaseManager.SaveUserData(user);
    }

    private static string FormatTime(int interval, int timeStamp) {
        var currentTimeStamp = (int)DateTime.Now.Subtract(Constants.BeginingOfTime).TotalSeconds;
        var seconds = interval - (currentTimeStamp - timeStamp);
        var minutesText = (seconds / 60).ToString();
        var secondsText = (seconds % 60).ToString();
        if (minutesText.Length == 1) { minutesText = "0" + minutesText; }
        if (secondsText.Length == 1) { secondsText = "0" + secondsText; }
        return minutesText + ":" + secondsText;
    }

    private static async Task<DataBaseManager.TaskData?> GetRandomTask(int taskType, int skillType) {
        var tasksData = await DataBaseManager.GetAllTasksSnapshot();
        var solvedData = await DataBaseManager.GetAllSolvedTasksSnapshot();
        
        if (!tasksData.Exists && !solvedData.Exists) {
            Debug.Log("!!! Error while retrieving TaskData !!!");
            return null;
        }
        
        // Get all task of required type
        var appropriateTasks = new List<DataBaseManager.TaskData>();
        foreach (var taskSnapshot in tasksData.Children) {
            var task = JsonUtility.FromJson<DataBaseManager.TaskData>(taskSnapshot.GetRawJsonValue());
            if (task.Type == taskType && task.SkillPromoted == skillType) {
                if (task.Type == 0) {
                    task.Answers = await DataBaseManager.GetTaskAnswers(task.Id);
                    task.CorrectAnswers = await DataBaseManager.GetTaskCorrectAnswers(task.Id);
                }
                appropriateTasks.Add(task);
            }
        }

        // Check that tasks are not solved earlier (not fully solved are accepted)
        foreach (var solvedSnapshot in solvedData.Children) {
            var solved = JsonUtility.FromJson<DataBaseManager.SolvedTask>(solvedSnapshot.GetRawJsonValue());
            if (!DataBaseManager.IsUserId(solved.UserId)) continue;
            foreach (var task in appropriateTasks) {
                if (!solved.TaskId.Equals(task.Id) || solved.TaskProgress < 0.99f) continue;
                appropriateTasks.Remove(task);
                break;
            }
        }

        if (appropriateTasks.Count == 0) {
            return null;
        }
        
        var random = new Random();
        return appropriateTasks[random.Next(appropriateTasks.Count)];
    }

}
