using UnityEngine;
using UnityEngine.UI;

public class UsersManagement : MonoBehaviour {
    public GameObject MainMenu, UserList;
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
        
        foreach (var userSnapshot in userData.Children) {
            var user = JsonUtility.FromJson<DataBaseManager.UserData>(userSnapshot.GetRawJsonValue());
            if (user.Type == 2) continue;
            
            var createdCard = Instantiate(UserCardExample, UserList.transform);
            createdCard.ID.text = user.Id;
            createdCard.EMail.text = user.Mail;
            createdCard.Role.text = Constants.UserTypesNames[user.Type];
            createdCard.UserType = user.Type;
            createdCard.UsersController = this;
        }
    }
    
    public void OnCardClicked(UserCardPrefab clickedCard) {
        _cardClicked = clickedCard;
        ChangeRole.interactable = true;
        ButtonText.text = Constants.UserTypeToButtonText[clickedCard.UserType];
    }

    public async void OnChangeRoleClick() {
        var userRole = _cardClicked.UserType;
        userRole = userRole == 0 ? 1 : 0;

        _cardClicked.Role.text = Constants.UserTypesNames[userRole];
        ButtonText.text = Constants.UserTypeToButtonText[userRole];

        var updatedUserQuery = await DataBaseManager.LoadUserDataById(_cardClicked.ID.text);

        /* DEBUG */
        if (updatedUserQuery == null) {
            Debug.Log("User not found!");
            return;
        }

        var updatedUser = updatedUserQuery.Value;
        updatedUser.Type = userRole;
        DataBaseManager.SaveUserData(updatedUser, updatedUser.Id);
    }

    public void OnBackClick() {
        foreach (Transform child in UserList.transform) {
            Destroy(child.gameObject);
        }
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
