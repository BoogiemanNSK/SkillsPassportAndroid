using UnityEngine;
using UnityEngine.UI;

namespace TeacherScripts
{
    public class AnswerCardPrefab : MonoBehaviour
    {
        public Text UserMail, Skill;
        public AnswersManagement AnswersController;

        public string AnswerText;
        public DataBaseManager.TaskData TaskAssigned;
        public DataBaseManager.UserData UserAssigned;
        
        public void OnCardClick() {
            AnswersController.OnCardClicked(this);
        }
    }
}
