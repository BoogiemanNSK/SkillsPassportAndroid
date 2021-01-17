using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {
    public GameObject[] IntroScreens;
    public IntroQuizResults ResultsScreen;

    private int _currentScreen;
    private DataBaseManager.UserData _savedUser;

    private void Start() {
        _currentScreen = 0;
        IntroScreens[0].SetActive(true);
        for (var i = 1; i < IntroScreens.Length; i++) {
            IntroScreens[i].SetActive(false);
        }
    }

    public void NextScreen() {
        IntroScreens[_currentScreen].SetActive(false);
        _currentScreen++;
        IntroScreens[_currentScreen].SetActive(true);
    }

    public async void ShowResults() {
        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
        _savedUser = updatedUserQuery.Value;
        
        NextScreen();
        ResultsScreen.OnEnter(_savedUser, IsSufficientToPass(_savedUser));
    }

    public void OnFinishClick() {
        _savedUser.FirstLaunch = false;
        DataBaseManager.SaveUserData(_savedUser);
        SceneManager.LoadScene(IsSufficientToPass(_savedUser) ? Constants.SnFinal : Constants.SnMain);
    }

    public bool IsSufficientToPass(DataBaseManager.UserData savedUser) {
        for (var i = 0; i < savedUser.Skills.Length; i++) {
            if (savedUser.Skills[i] < Constants.RequiredSkillsToPass[savedUser.CharIndex / 2][i]) {
                return false;
            }
        }
        return true;
    }
}
