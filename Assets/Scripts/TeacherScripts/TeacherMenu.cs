using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeacherScripts
{
    public class TeacherMenu : MonoBehaviour {
        public TasksManagement TasksManager;
        public AnswersManagement AnswersManager;

        private void Start() {
            gameObject.SetActive(true);
            TasksManager.gameObject.SetActive(false);
            AnswersManager.gameObject.SetActive(false);
        }

        public void OnTasksClick() {
            gameObject.SetActive(false);
            TasksManager.gameObject.SetActive(true);
            TasksManager.OnEnter();
        }

        public void OnAnswersClick() {
            gameObject.SetActive(false);
            AnswersManager.gameObject.SetActive(true);
            AnswersManager.OnEnter();
        }

        public void OnLeaveAccountClick() {
            FirebaseAuth.DefaultInstance.SignOut();
            SceneManager.LoadScene(Constants.SnAuth);
        }
    }
}
