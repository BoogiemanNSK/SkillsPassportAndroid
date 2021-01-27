using AdminScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class AdminMenu : MonoBehaviour {
    public UsersManagement UsersManager;

    private void Start() {
        gameObject.SetActive(true);
        UsersManager.gameObject.SetActive(false);
    }

    public void OnUsersClick() {
        gameObject.SetActive(false);
        UsersManager.gameObject.SetActive(true);
        UsersManager.OnEnter();
    }

    public void OnLeaveAccountClick() {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene(Constants.SnAuth);
    }
}
