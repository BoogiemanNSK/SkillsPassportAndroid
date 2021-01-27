using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace AuthScripts
{
    public class RegisterController : MonoBehaviour {
    
        public InputField Email, Password, Password2;
        public Text Tip;
        public Image TipBar;

        private void Start() {
            Email.onValueChanged.AddListener(delegate {UpdateTip();});
            Password.onValueChanged.AddListener(delegate {UpdateTip();});
            Password2.onValueChanged.AddListener(delegate {UpdateTip();});
        }

        private void UpdateTip() {
            if (Password.text != Password2.text) {
                TipBar.gameObject.SetActive(true);
                Tip.color = new Color(255, 0, 0, 0.4f);
                Tip.text = Constants.AuthPasswordsDontMatch;
            } else {
                TipBar.gameObject.SetActive(false);
            }
        }

        public void OnRegister() {
            RegisterUser(Email.text, Password.text);
        }

        private async void RegisterUser(string mail, string password) {
            var successfullyRegistered = false;
            var auth = FirebaseAuth.DefaultInstance;
            await auth.CreateUserWithEmailAndPasswordAsync(mail, password).ContinueWith(registerTask => {
                if (registerTask.IsFaulted || registerTask.IsCanceled) {
                    Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
                } else {
                    successfullyRegistered = true;
                    Debug.Log($"Successfully registered user {registerTask.Result.Email}");

                    // Create new database entry with user data
                    var newUser = new DataBaseManager.UserData {
                        Type = 0,
                        Mail = mail,
                        Id = registerTask.Result.UserId,
                        FirstLaunch = true,
                        CharIndex = 0,
                        Skills = new float[]{0, 0, 0, 0, 0, 0},
                        SkillCasePassed = new[]{false, false, false, false, false, false},
                        TimeStamps = new[]{0, 0}
                    };

                    DataBaseManager.SetPlayerId(registerTask.Result.UserId);
                    DataBaseManager.SaveUserData(newUser);
                }
            });

            TipBar.gameObject.SetActive(true);
            TipBar.color = successfullyRegistered ? new Color(0, 255, 0, 0.4f) : new Color(255, 0, 0, 0.4f);
            Tip.text = successfullyRegistered ? Constants.AuthRegistrationSuccess : Constants.AuthRegistrationFail;
        }

    }
}
