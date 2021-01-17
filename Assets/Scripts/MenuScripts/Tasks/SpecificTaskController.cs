using UnityEngine;

public abstract class SpecificTaskController : MonoBehaviour {

    public abstract void OnEnter(DataBaseManager.TaskData taskData);

}
