using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialCheckScript : MonoBehaviour {
    public GameObject MainMenu, Settings;
    public GameObject BlackForeground;

    private async void Start() {
        var allowToProceed = true;
        BlackForeground.SetActive(true);

        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
        var updatedUser = updatedUserQuery.Value;
        
        if (updatedUser.FirstLaunch) {
            allowToProceed = false;
            SceneManager.LoadScene(Constants.SnIntro);
        }

        if (!updatedUser.PassedTutorial) {
            allowToProceed = false;
            SceneManager.LoadScene(Constants.SnTutorial);
        }

        if (allowToProceed) {
            BlackForeground.SetActive(false);
        }

        Settings.SetActive(false);
        MainMenu.SetActive(true);
    }
}
