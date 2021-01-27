using System.Net;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

namespace TeacherScripts
{
    public class AnswersManagement : MonoBehaviour
    {
        public TeacherMenu MainMenu;
        public RectTransform AnswersList;
        public AnswerCardPrefab AnswerCardExample;
        public Button CheckBtn;

        public GameObject CheckScreen;
        public Text TaskDescription, AnswerText, Grade;
        public Slider GradingSlider;

        private AnswerCardPrefab _cardClicked;

        public void OnBackClick()
        {
            foreach (Transform child in AnswersList)
            {
                Destroy(child.gameObject);
            }

            MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public async void OnEnter()
        {
            _cardClicked = null;
            CheckBtn.interactable = false;
            var answersData = await DataBaseManager.GetAllAnswersSnapshot();

            if (!answersData.Exists)
            {
                Debug.Log("!!! Error while retrieving AnswersData !!!");
                return;
            }

            var successfulQueries = 0;
            foreach (var answerSnapshot in answersData.Children)
            {
                var answer = JsonUtility.FromJson<DataBaseManager.AnswerData>(answerSnapshot.GetRawJsonValue());
                var userQuery = DataBaseManager.LoadUserDataById(answer.UserId);
                var taskQuery = DataBaseManager.LoadTaskDataById(answer.TaskId);

                if (userQuery.Result == null || taskQuery.Result == null) continue;
                var user = userQuery.Result.Value;
                var task = taskQuery.Result.Value;

                var createdCard = Instantiate(AnswerCardExample, AnswersList);
                createdCard.UserMail.text = user.Mail;
                createdCard.Skill.text = Constants.SkillsNames[task.SkillPromoted];
                createdCard.AnswersController = this;
                createdCard.AnswerText = answer.Text;
                createdCard.UserAssigned = user;
                createdCard.TaskAssigned = task;

                successfulQueries++;
            }

            var cardHeight = AnswerCardExample.GetComponent<RectTransform>().sizeDelta.y;
            AnswersList.sizeDelta = new Vector2(AnswersList.sizeDelta.x, cardHeight * successfulQueries);
        }

        public void OnCardClicked(AnswerCardPrefab clickedCard)
        {
            _cardClicked = clickedCard;
            CheckBtn.interactable = true;

            CheckScreen.SetActive(true);
            TaskDescription.text = _cardClicked.TaskAssigned.TaskText;
            AnswerText.text = _cardClicked.AnswerText;

            // Slider settings
            GradingSlider.minValue = 0.0f;
            GradingSlider.maxValue = _cardClicked.TaskAssigned.SkillIncrease;
            GradingSlider.value = 0.0f;
            GradingSlider.onValueChanged.AddListener(SliderChangeListener);
        }

        public async void OnGradeTask()
        {
            // Reacquire user data, as it could've changed during task checking
            var updatedUserQuery = await DataBaseManager.LoadUserDataById(_cardClicked.UserAssigned.Id);
            Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
            var userData = updatedUserQuery.Value;

            var increase = GradingSlider.value;
            var promotedSkillIndex = _cardClicked.TaskAssigned.SkillPromoted;
            var oldSkillValue = userData.Skills[promotedSkillIndex];
            var newSkillValue = oldSkillValue + increase > 100.0f ? 100.0f : oldSkillValue + increase;

            userData.Skills[promotedSkillIndex] = newSkillValue;
            DataBaseManager.SaveUserData(userData);

            // Send an e-mail to user
            SendEmail(
                userData.Mail,
                Constants.SkillsNames[_cardClicked.TaskAssigned.SkillPromoted],
                increase,
                _cardClicked.TaskAssigned.SkillIncrease
                );
            
            OnCancelGrading();
        }

        public void OnCancelGrading()
        {
            GradingSlider.onValueChanged.RemoveAllListeners();
            CheckScreen.SetActive(false);
        }

        private void SliderChangeListener(float val)
        {
            Grade.text = val.ToString("0.#");
        }

        private void SendEmail(string userMail, string skillName, float grade, float maxGrade)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Port = 587
            };

            mail.From = new MailAddress(Constants.AppMail);
            mail.To.Add(new MailAddress(userMail));
            mail.Subject = Constants.MailName;
            mail.Body = Constants.MailText(skillName, grade.ToString("0.#"), maxGrade.ToString("0.#"));

            smtpServer.Credentials = new NetworkCredential("myEmail@gmail.com", "MyPasswordGoesHere");
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtpServer.Send(mail);
        }
    }
}
