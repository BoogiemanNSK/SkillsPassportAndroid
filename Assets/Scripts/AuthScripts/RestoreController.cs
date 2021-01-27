using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace AuthScripts
{
    public class RestoreController : MonoBehaviour {
    
        public InputField Email;
        public Text Tip;
        public Image TipBar;

        private void Start() {
            Email.onValueChanged.AddListener(delegate {UpdateTip();});
            UpdateTip();
        }

        private void UpdateTip() {
            TipBar.gameObject.SetActive(false);
        }
    
        public void OnRestore() {
            RestoreUser(Email.text);
        }

        private async void RestoreUser(string mail) {
            var successfullyRestored = false;
            var auth = FirebaseAuth.DefaultInstance;

            await auth.SendPasswordResetEmailAsync(mail).ContinueWith(restoreTask => {
                if (restoreTask.IsFaulted || restoreTask.IsCanceled) {
                    Debug.LogWarning($"Failed to send email with {restoreTask.Exception}");
                } else {
                    successfullyRestored = true;
                    Debug.Log($"Successfully sent email to {mail}");
                }
            });

            TipBar.gameObject.SetActive(true);
            TipBar.color = successfullyRestored ? new Color(0, 255, 0, 0.4f) : new Color(255, 0, 0, 0.4f);
            Tip.text = successfullyRestored ? Constants.AuthRestoreSuccess : Constants.AuthRestoreFail;

        }
    
    }
}
