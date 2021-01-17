using UnityEngine;

public class AnswersManagement : MonoBehaviour {
    public AdminMenu MainMenu;

    public void OnBackClick() {
        MainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
