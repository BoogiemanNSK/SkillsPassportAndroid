using UnityEngine;
using UnityEngine.UI;

namespace AdminScripts
{
    public class UserCardPrefab : MonoBehaviour {
        public Text ID, EMail, Role;
        public UsersManagement UsersController;
        public int UserType;

        public void OnCardClick() {
            UsersController.OnCardClicked(this);
        }
    }
}
