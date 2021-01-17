using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class CharacterMonitoring : MonoBehaviour {
    public GameObject CharDescPanel, MalePanel, FemalePanel;
    public SwipeController LocalSwipeController;
    public IntroController MenuController;
    public Text CharName, CharDesc;
    public Image[] CharacterImage;

    private int _currentImageSourceIndex;
    private bool _isMale;

    private void Start() {
        _isMale = false;
        _currentImageSourceIndex = -1;
        
        MalePanel.SetActive(false);
        FemalePanel.SetActive(true);
        CharDescPanel.SetActive(false);
    }

    private void Update() {
        var charIndex = LocalSwipeController.ElementIndex;
        var imageSourceIndex = charIndex + (_isMale ? Constants.CharactersImageLink.Length / 2 : 0);
        if (_currentImageSourceIndex == imageSourceIndex) return;
        
        _currentImageSourceIndex = imageSourceIndex;
        CharName.text = Constants.CharactersNames[charIndex];
        for (var i = 0; i < CharacterImage.Length; i++) {
            imageSourceIndex = i + (_isMale ? Constants.CharactersImageLink.Length / 2 : 0);
            CharacterImage[i].sprite = Resources.Load<Sprite>(Constants.CharactersImageLink[imageSourceIndex]);
        }
    }

    public void OnOpenDesc() {
        CharDescPanel.SetActive(true);
        CharDesc.text = Constants.CharactersDesc[LocalSwipeController.ElementIndex];
    }

    public void OnCloseDesc() {
        CharDescPanel.SetActive(false);
    }

    public void OnMaleClick() {
        _isMale = true;
        MalePanel.SetActive(true);
        FemalePanel.SetActive(false);
    }
    
    public void OnFemaleClick() {
        _isMale = false;
        MalePanel.SetActive(false);
        FemalePanel.SetActive(true);
    }
    
    public async void OnSelectCharClick() {
        // Remember Selected Char Image
        var updatedUserQuery = await DataBaseManager.LoadUserData();
        Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");

        var updatedUser = updatedUserQuery.Value;
        updatedUser.CharIndex = _currentImageSourceIndex;
        DataBaseManager.SaveUserData(updatedUser);
        
        MenuController.NextScreen();
    }
    
}
