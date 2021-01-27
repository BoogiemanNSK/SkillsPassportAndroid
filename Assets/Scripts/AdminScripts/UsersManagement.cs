using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdminScripts
{
    public class UsersManagement : MonoBehaviour {
        public GameObject MainMenu;
        public RectTransform UserList;
        public UserCardPrefab UserCardExample;
        public Button ChangeRole;
        public Text ButtonText;
    
        private UserCardPrefab _cardClicked;

        public async void OnEnter() {
            _cardClicked = null;
            ChangeRole.interactable = false;
            var userData = await DataBaseManager.GetAllUsersSnapshot();

            if (!userData.Exists) {
                Debug.Log("!!! Error while retrieving UserData !!!");
                return;
            }

            var nonAdminUserCount = 0;
            foreach (var userSnapshot in userData.Children) {
                var user = JsonUtility.FromJson<DataBaseManager.UserData>(userSnapshot.GetRawJsonValue());
                if (user.Type == 2) continue;
                nonAdminUserCount++;
                
                var createdCard = Instantiate(UserCardExample, UserList);
                createdCard.ID.text = user.Id;
                createdCard.EMail.text = user.Mail;
                createdCard.Role.text = Constants.UserTypesNames[user.Type];
                createdCard.UserType = user.Type;
                createdCard.UsersController = this;
            }
            
            var cardHeight = UserCardExample.GetComponent<RectTransform>().sizeDelta.y;
            UserList.sizeDelta = new Vector2(UserList.sizeDelta.x, cardHeight * nonAdminUserCount);
        }
    
        public void OnCardClicked(UserCardPrefab clickedCard) {
            _cardClicked = clickedCard;
            ChangeRole.interactable = true;
            ButtonText.text = Constants.UserTypeToButtonText[clickedCard.UserType];
        }

        public async void OnChangeRoleClick() {
            // Re-select user card (since selection drops by default)
            EventSystem.current.SetSelectedGameObject(_cardClicked.gameObject);
            
            var userRole = _cardClicked.UserType;
            userRole = userRole == 0 ? 1 : 0;

            var updatedUserQuery = await DataBaseManager.LoadUserDataById(_cardClicked.ID.text);

            /* DEBUG */
            if (updatedUserQuery == null) {
                Debug.Log("User not found!");
                return;
            }

            var updatedUser = updatedUserQuery.Value;
            updatedUser.Type = userRole;
            DataBaseManager.SaveUserData(updatedUser, updatedUser.Id);
            
            // Update UI after successful db update
            _cardClicked.UserType = userRole;
            _cardClicked.Role.text = Constants.UserTypesNames[userRole];
            ButtonText.text = Constants.UserTypeToButtonText[userRole];
        }

        public void OnBackClick() {
            foreach (Transform child in UserList) {
                Destroy(child.gameObject);
            }
            MainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
