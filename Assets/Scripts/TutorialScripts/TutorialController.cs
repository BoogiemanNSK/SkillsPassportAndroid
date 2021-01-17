using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

    public GameObject[] Screens;
    public GameObject QuestionsSheet;
    
    private int _currentScreen;

    private void Start() {
        _currentScreen = 0;
        for (var i = 1; i < Screens.Length; i++) {
            Screens[i].SetActive(false);
        }
        Screens[0].SetActive(true);
    }

    public async void SkipTutorial() {
        var user = await DataBaseManager.LoadUserData();
        if (user == null) {
            Debug.Log("Why the hell user is null, tell me now???");
            return;
        }

        var userObj = user.Value;
        userObj.PassedTutorial = true;
        DataBaseManager.SaveUserData(userObj);
        
        SceneManager.LoadScene(Constants.SnMain);
    }

    public void NextScreen() {
        Screens[_currentScreen].SetActive(false);
        _currentScreen++;
        Screens[_currentScreen].SetActive(true);
    }

    public void OpenQuestions() {
        QuestionsSheet.SetActive(true);
    }
    
    public void CloseQuestions() {
        QuestionsSheet.SetActive(false);
    }
    
}
