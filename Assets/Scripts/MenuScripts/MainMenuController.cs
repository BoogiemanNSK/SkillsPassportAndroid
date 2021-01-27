using MenuScripts.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject FinishingScreen;
        public SettingsController SettingsScreen;
        public SkillPrefabGenerator SkillDescExample;
        public TasksController TaskMenuController;
        public RulesScreen Rules;

        public GameObject CharDesciption;
        public Text CharText, CharTitle;
        public Image CharacterImage, DescCharImage;
        public Transform SkillsGrid;

        private DataBaseManager.UserData _userData;
    
        private async void Start() {
            var updatedUserQuery = await DataBaseManager.LoadUserData();
            Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
            _userData = updatedUserQuery.Value;
        
            // If first launch -> next code may cause null exception
            if (_userData.FirstLaunch || !_userData.PassedTutorial) { return; }
        
            CharacterImage.sprite = Resources.Load<Sprite>(Constants.CharactersShortImageLink[_userData.CharIndex]);
            SettingsScreen.gameObject.SetActive(false);
            TaskMenuController.gameObject.SetActive(false);
            OnEnter();
        }

        public async void OnEnter() {
            // Re-read user info
            var updatedUserQuery = await DataBaseManager.LoadUserData();
            Debug.Assert(updatedUserQuery != null, nameof(updatedUserQuery) + " != null");
            _userData = updatedUserQuery.Value;
        
            for (var i = 0; i < _userData.Skills.Length; i++) {
                var createdSkill = Instantiate(SkillDescExample, SkillsGrid);
                var charIndexWithoutGender = _userData.CharIndex % (Constants.CharactersImageLink.Length / 2);
            
                createdSkill.Fill(
                    i,
                    _userData.Skills[i],
                    Constants.RequiredSkillsToPass[charIndexWithoutGender][i],
                    Constants.SkillsImageLink[i],
                    Constants.SkillsNames[i],
                    this,
                    TaskMenuController
                );
            }
        
            // Check if passport can be acquired
            if (IsSufficientToGetPassport()) 
                FinishingScreen.SetActive(true);
        }

        public void OnFinish()
        {
            OnExit();
            SceneManager.LoadScene(Constants.SnFinal);
        }
        
        public void OnExit() {
            foreach (Transform child in SkillsGrid) {
                Destroy(child.gameObject);
            }
        }
    
        public void OnSettingsClick() {
            SettingsScreen.gameObject.SetActive(true);
            OnExit();
            gameObject.SetActive(false);
        }

        public void OnRulesClick() {
            Rules.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnCloseFinishingScreen()
        {
            FinishingScreen.SetActive(false);
        }

        public void OnOpenDescription() {
            CharDesciption.SetActive(true);
            CharText.text = Constants.CharactersDesc[_userData.CharIndex / 2];
            CharTitle.text = Constants.CharactersNames[_userData.CharIndex / 2];
            DescCharImage.sprite = Resources.Load<Sprite>(Constants.CharactersShortImageLink[_userData.CharIndex]);
        }
        
        private bool IsSufficientToGetPassport() {
            for (var i = 0; i < _userData.Skills.Length; i++) {
                if (_userData.Skills[i] < Constants.RequiredSkillsToPass[_userData.CharIndex / 2][i]
                    || !_userData.SkillCasePassed[i]) {
                    return false;
                }
            }
            return true;
        }
    
    }
}
