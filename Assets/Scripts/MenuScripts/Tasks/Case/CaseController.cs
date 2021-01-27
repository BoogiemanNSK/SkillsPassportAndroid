using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts.Tasks.Case
{
    public class CaseController : SpecificTaskController {

        public GameObject AbswersScreen, ResultsScreen;
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
        
            // Mark that player completed case task for this skill
            SetPlayerCaseSolved();

            ResultsScreen.SetActive(true);
        }

        public void OnCloseResults()
        {
            ResultsScreen.SetActive(false);
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
    
        private async void SetPlayerCaseSolved()
        {
            var userQuery = await DataBaseManager.LoadUserData();
            Debug.Assert(userQuery != null, nameof(userQuery) + " != null");
            var user = userQuery.Value;
            user.SkillCasePassed[_currentTask.SkillPromoted] = true;
            DataBaseManager.SaveUserData(user);
        }
    
    }
}
