using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class SettingsController : MonoBehaviour {
    public MainMenuController MainMenu;
    public GameObject ConfirmationScreen;
    
    public void OnBackClick() {
        MainMenu.gameObject.SetActive(true);
        MainMenu.OnEnter();
        gameObject.SetActive(false);
    }

    public void OnConfirm() {
        PlayerPrefs.DeleteAll();
        DataBaseManager.ClearUserAnswers(DataBaseManager.GetPlayerId());
        DataBaseManager.ClearUserSolved(DataBaseManager.GetPlayerId());
        DataBaseManager.DeleteUserChar(DataBaseManager.GetPlayerId());
        SceneManager.LoadScene(Constants.SnIntro);
    }

    public void OnCancel() {
        ConfirmationScreen.SetActive(false);
    }

    public void OnResetClick() {
        ConfirmationScreen.SetActive(true);
    }

    public void OnLogOutClick() {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene(Constants.SnAuth);
    }
}
