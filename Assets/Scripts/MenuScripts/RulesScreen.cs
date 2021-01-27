using MenuScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesScreen : MonoBehaviour {

    public MainMenuController Menu;
    
    public void OnBackClick() {
        Menu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RepeatTutorial() {
        SceneManager.LoadScene(Constants.SnTutorial);
    }
    
}
