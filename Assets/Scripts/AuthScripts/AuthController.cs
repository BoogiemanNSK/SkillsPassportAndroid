using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthController : MonoBehaviour {
    public GameObject LoginWindow, RegisterWindow, PasswordRestoreWindow;
    public GameObject BlackForeground;
    
    private FirebaseUser _currentUser;

    private async void Start() {
        await DataBaseManager.CheckDatabaseAndInit();
        _currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
        
        if (_currentUser != null) {
            BlackForeground.SetActive(true);
            ExistingUserLogIn();
        } else {
            BlackForeground.SetActive(false);
        }

        OnBackToLogin();    
    }

    public void OnOpenRegisterMenu() {
        LoginWindow.SetActive(false);
        RegisterWindow.SetActive(true);
    }

    public void OnOpenPasswordRestore() {
        LoginWindow.SetActive(false);
        PasswordRestoreWindow.SetActive(true);
    }

    public void OnBackToLogin() {
        LoginWindow.SetActive(true);
        RegisterWindow.SetActive(false);
        PasswordRestoreWindow.SetActive(false);
    }
    
    private async void ExistingUserLogIn() {
        DataBaseManager.SetPlayerId(_currentUser.UserId);
        await DataBaseManager.SaveExists();
        
        // Try to find data in Firebase DB
        var userDataQuery = await DataBaseManager.LoadUserData();
        if (userDataQuery == null) {
            Debug.Log("Cashed user not found in DB");
            return;
        }
        var userData = userDataQuery.Value;
        SceneManager.LoadScene(Constants.UserTypeToSceneName[userData.Type]);
    }
}
