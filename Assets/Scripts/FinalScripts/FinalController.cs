using System.Collections.Generic;
using IntroScripts;

namespace FinalScripts
{
    public class FinalController : StartQuizTasksManager
    {
        private float[] _resultingSkills;
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

        public override void SubmitTask(int qIndex, int qAnswerIndex) {
            QAnswers[qIndex] = qAnswerIndex;
            _submittedTasks.Add(qIndex);
            if (_submittedTasks.Count == _totalTasksCount) {
                AnswerAllTextTip.SetActive(false);
                SubmitAllButton.interactable = true;
            }
        }

        public override void SubmitAll() {
            for (var i = 0; i < _totalTasksCount; i++) {
                SkillPointsGathered[Constants.IntroQuizSkillsPromoted[i]] += QAnswers[i];
            }

            _resultingSkills = new float[6];
            for (var i = 0; i < _resultingSkills.Length; i++) {
                var inPercents = (SkillPointsGathered[i] / (float)Constants.IntroMAXPointsForSkill) * 100.0f;
                _resultingSkills[i] = inPercents;
            }
            
            // TODO Show some feedback
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
}
