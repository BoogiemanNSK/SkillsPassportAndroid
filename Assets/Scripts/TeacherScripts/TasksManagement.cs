using UnityEngine;
using UnityEngine.UI;

namespace TeacherScripts
{
    public class TasksManagement : MonoBehaviour {
        public NewTask NewTaskMenu;
        public TeacherMenu MainMenu;
        public Button Edit, Delete;
        public TaskCardPrefab TaskCardExample;
        public Transform TaskList;

        private TaskCardPrefab _clickedTask;

        public async void OnEnter() {
            _clickedTask = null;
            Edit.interactable = false;
            Delete.interactable = false;
            var tasksData = await DataBaseManager.GetAllTasksSnapshot();
        
            if (!tasksData.Exists) {
                Debug.Log("!!! Error while retrieving TaskData !!!");
                return;
            }
        
            foreach (var taskSnapshot in tasksData.Children) {
                var task = JsonUtility.FromJson<DataBaseManager.TaskData>(taskSnapshot.GetRawJsonValue());
                if (!DataBaseManager.IsUserId(task.CreatorId)) continue;
            
                var createdTask = Instantiate(TaskCardExample, TaskList);
                createdTask.Name.text = task.TaskName;
                createdTask.Type.text = Constants.TaskTypesNames[task.Type];
                createdTask.TasksController = this;
                createdTask.TaskId = task.Id;
            }
        }
    
        public void OnCardClicked(TaskCardPrefab clickedTask) { 
            _clickedTask = clickedTask;
            // Edit.interactable = true;
            Delete.interactable = true;
        }
    
        public void OnNewTaskClick() {
            foreach (Transform child in TaskList) {
                Destroy(child.gameObject);
            }
        
            NewTaskMenu.gameObject.SetActive(true);
            NewTaskMenu.OnEnter();
        }
    
        public void OnEditTaskClick() {
            // TODO
        }
    
        public void OnDeleteTaskClick() {
            // DataBaseManager.DeleteTask(_clickedTask.TaskId);
            Destroy(_clickedTask.gameObject);
        }
    
        public void OnBackClick() {
            foreach (Transform child in TaskList) {
                Destroy(child.gameObject);
            }
            MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
