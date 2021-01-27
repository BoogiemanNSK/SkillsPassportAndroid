using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AuthScripts
{
    public class LoginController : MonoBehaviour {
    
        public InputField Email, Password;
        public Image TipBar;
        public Text Tip;

        private void Start() {
            Email.onValueChanged.AddListener(delegate { ClearTip(); });
            Password.onValueChanged.AddListener(delegate { ClearTip(); });
            Tip.gameObject.SetActive(true);
            TipBar.color = new Color(255, 0, 0, 0.6f);
            ClearTip();
        }

        private void ClearTip() {
            TipBar.gameObject.SetActive(false);
        }

        public void OnLogin() {
            LoginUser(Email.text, Password.text);
        }

        private async void LoginUser(string mail, string password) {
            var successfulLogin = false;
            var auth = FirebaseAuth.DefaultInstance;
            await auth.SignInWithEmailAndPasswordAsync(mail, password).ContinueWith(loginTask => {

                if (loginTask.IsFaulted || loginTask.IsCanceled) {
                    Debug.LogWarning($"Failed to register task with {loginTask.Exception}");
                } else {
                    successfulLogin = true;
                    Debug.Log($"Successfully login user {loginTask.Result.Email}");
                    DataBaseManager.SetPlayerId(loginTask.Result.UserId);
                }
            
            });

            // Try to find data in Firebase DB
            var userDataQuery = await DataBaseManager.LoadUserData();
            if (successfulLogin && userDataQuery != null) {
                var userData = userDataQuery.Value;
                SceneManager.LoadScene(Constants.UserTypeToSceneName[userData.Type]);
            } else {
                TipBar.gameObject.SetActive(true);
                Tip.text = Constants.AuthUserNotFound;
            }
        }
    
    }
}
