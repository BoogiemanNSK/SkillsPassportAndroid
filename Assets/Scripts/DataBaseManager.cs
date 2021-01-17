using System;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

public static class DataBaseManager {
    
    private static FirebaseDatabase _database;
    private static string _playerId;
    
    public static async Task CheckDatabaseAndInit() {
        if (_database != null) { return; }
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
            } else {
                _database = FirebaseDatabase.DefaultInstance;
                Debug.Log("Successfully connected to Firebase");
            }
        });
    }
    
    public static void SetPlayerId(string newId) {
        _playerId = newId;
    }

    public static string GetPlayerId() {
        return _playerId;
    }

    public static bool IsUserId(string id) {
        return id.Equals(_playerId);
    }

    /* DATA MODELS DEFINITIONS */

    public struct UserData {
        public string Id;
        public string Mail;
        public bool FirstLaunch;
        public bool PassedTutorial;
        public int Type;
        public int CharIndex;
        public int[] TimeStamps;
        public float[] Skills;
    }
    
    public struct TaskData {
        public string Id;
        public string CreatorId;
        public string TaskName;
        public string TaskText;
        public string[] Questions;
        public string[][] Answers;
        public int Type;
        public int SkillPromoted;
        public int[][] CorrectAnswers;
        public float SkillIncrease;
    }
    
    public struct AnswerData {
        public string Id;
        public string TaskId;
        public string UserId;
        public string Text;
    }

    public struct SolvedTask {
        public string Id;
        public string TaskId;
        public string UserId;
        public float TaskProgress;
    }

    /* USER DATA INTERACTIONS */
    
    public static void SaveUserData(UserData user) {
        _database.GetReference(Constants.DBUsersPath + "/" + _playerId).SetRawJsonValueAsync(JsonUtility.ToJson(user));
    }
    
    public static void SaveUserData(UserData user, string userId) {
        _database.GetReference(Constants.DBUsersPath + "/" + userId).SetRawJsonValueAsync(JsonUtility.ToJson(user));
    }

    public static async Task<UserData?> LoadUserData() {
        var dataSnapshot = await _database.GetReference(Constants.DBUsersPath + "/" + _playerId).GetValueAsync();
        if (!dataSnapshot.Exists) { return null; }
        return JsonUtility.FromJson<UserData>(dataSnapshot.GetRawJsonValue());
    }
    
    public static async Task<UserData?> LoadUserDataById(string userId) {
        var dataSnapshot = await _database.GetReference(Constants.DBUsersPath + "/" + userId).GetValueAsync();
        if (!dataSnapshot.Exists) { return null; }
        return JsonUtility.FromJson<UserData>(dataSnapshot.GetRawJsonValue());
    }

    public static async Task<DataSnapshot> GetAllUsersSnapshot() {
        var dataSnapshot = await _database.GetReference(Constants.DBUsersPath).GetValueAsync();
        return dataSnapshot;
    }

    public static async Task<bool> SaveExists() {
        var dataSnapshot = await _database.GetReference(Constants.DBUsersPath + "/" + _playerId).GetValueAsync();
        return dataSnapshot.Exists;
    }

    // Method to update user timestamps
    public static async void DeleteUserChar(string userId) {
        var user = await LoadUserDataById(userId);
        if (user == null) { return; }

        var userUpdated = user.Value;
        userUpdated.TimeStamps = new[] {0, 0, 0};
        userUpdated.Skills = new[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
        userUpdated.FirstLaunch = true;
        
        SaveUserData(userUpdated, userId);
    }
    
    /* TASK DATA INTERACTIONS */
    
    public static async Task<DataSnapshot> GetAllTasksSnapshot() {
        var dataSnapshot = await _database.GetReference(Constants.DBTasksPath).GetValueAsync();
        return dataSnapshot;
    }

    // TODO Maybe try to insert objects instead of arrays
    // The only way to get two-dim array from DB
    public static async Task<string[][]> GetTaskAnswers(string taskId) {
        var dataSnapshot = await _database.GetReference(Constants.DBTasksPath + "/" + taskId + "/" +
                                                        Constants.DBTaskAnswersField).GetValueAsync();
        var answersArray = new string[dataSnapshot.Children.Count()][];

        var i = 0;
        foreach (var child in dataSnapshot.Children) {
            answersArray[i] = new string[child.Children.Count()];

            var j = 0;
            foreach (var answer in child.Children) {
                answersArray[i][j] = Convert.ToString(answer.Value);
                j++;
            }

            i++;
        }

        return answersArray;
    }
    
    // The only way to get two-dim array from DB
    public static async Task<int[][]> GetTaskCorrectAnswers(string taskId) {
        var dataSnapshot = await _database.GetReference(Constants.DBTasksPath + "/" + taskId + "/" +
                                                        Constants.DBTaskCorrectAnswersField).GetValueAsync();
        var answersArray = new int[dataSnapshot.Children.Count()][];

        var i = 0;
        foreach (var child in dataSnapshot.Children) {
            answersArray[i] = new int[child.Children.Count()];

            var j = 0;
            foreach (var answer in child.Children) {
                answersArray[i][j] = Convert.ToInt32(answer.Value);
                j++;
            }

            i++;
        }

        return answersArray;
    }
    
    public static async void CreateTask(TaskData task) {
        var newEntryId = _database.GetReference(Constants.DBTasksPath).Push().Key;

        task.Id = newEntryId;
        await _database.GetReference(Constants.DBTasksPath + "/" + newEntryId).SetRawJsonValueAsync(JsonUtility.ToJson(task));
    }
    
    public static void UpdateTask(TaskData task, string taskId) {
        _database.GetReference(Constants.DBTasksPath + "/" + taskId).SetRawJsonValueAsync(JsonUtility.ToJson(task));
    }
    
    public static void DeleteTask(string taskId) {
        _database.GetReference(Constants.DBTasksPath + "/" + taskId).RemoveValueAsync();
    }
    
    /* ANSWERS INTERACTION */
    
    public static async Task<AnswerData[]> GetAllAnswers() {
        var dataSnapshot = await _database.GetReference(Constants.DBAnswersPath).GetValueAsync();
        return !dataSnapshot.Exists ? null : JsonUtility.FromJson<AnswerData[]>(dataSnapshot.GetRawJsonValue());
    }
    
    public static async void CreateAnswer(AnswerData answer) {
        var newEntryId = _database.GetReference(Constants.DBAnswersPath).Push().Key;

        answer.Id = newEntryId;
        await _database.GetReference(Constants.DBAnswersPath + "/" + newEntryId).SetRawJsonValueAsync(JsonUtility.ToJson(answer));
    }
    
    public static void DeleteAnswer(string answerId) {
        _database.GetReference(Constants.DBAnswersPath + "/" + answerId).RemoveValueAsync();
    }

    public static async void ClearUserAnswers(string userId) {
        var answersData = await GetAllAnswers();
        foreach (var answer in answersData) {
            if (answer.UserId.Equals(userId)) {
                DeleteAnswer(answer.Id);
            }
        }
    }
    
    /* SOLVED TASKS INTERACTIONS */
    
    public static async Task<DataSnapshot> GetAllSolvedTasksSnapshot() {
        var dataSnapshot = await _database.GetReference(Constants.DBSolvedTasksPath).GetValueAsync();
        return dataSnapshot;
    }
    
    public static async void CreateSolvedTask(SolvedTask solved) {
        var newEntryId = _database.GetReference(Constants.DBSolvedTasksPath).Push().Key;

        solved.Id = newEntryId;
        await _database.GetReference(Constants.DBSolvedTasksPath + "/" + newEntryId).SetRawJsonValueAsync(JsonUtility.ToJson(solved));
    }

    public static async void ClearUserSolved(string userId) {
        var solvedData = await GetAllSolvedTasksSnapshot();
        var solvedTasks = new SolvedTask[0];
        
        if (solvedData.Exists) { solvedTasks = JsonUtility.FromJson<SolvedTask[]>(solvedData.GetRawJsonValue()); }
        foreach (var solved in solvedTasks) {
            if (solved.UserId.Equals(userId)) {
                await _database.GetReference(Constants.DBSolvedTasksPath + "/" + solved.Id).RemoveValueAsync();
            }
        }
    }

    public static async Task<SolvedTask?> FindSolvedTask(string userId, string taskId) {
        var solvedData = await GetAllSolvedTasksSnapshot();
        var solvedTasks = new SolvedTask[0];
        
        if (solvedData.Exists) { solvedTasks = JsonUtility.FromJson<SolvedTask[]>(solvedData.GetRawJsonValue()); }
        foreach (var solved in solvedTasks) {
            if (solved.TaskId.Equals(taskId) && solved.UserId.Equals(userId)) {
                return solved;
            }
        }

        return null;
    }
    
    public static void UpdateSolvedData(SolvedTask solved) {
        _database.GetReference(Constants.DBSolvedTasksPath + "/" + solved.Id).SetRawJsonValueAsync(JsonUtility.ToJson(solved));
    }
    
}
