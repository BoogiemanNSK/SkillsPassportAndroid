using TeacherScripts;
using UnityEngine;
using UnityEngine.UI;

public class TaskCardPrefab : MonoBehaviour {
    public Text Name, Type;
    public TasksManagement TasksController;
    public string TaskId;

    public void OnCardClick() {
        TasksController.OnCardClicked(this);
    }
}
