using MenuScripts;
using MenuScripts.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SkillPrefabGenerator : MonoBehaviour {
    public ProgressBar ProgressBarController;
    
    public Text Value, Name;
    public Image Icon;
    
    private MainMenuController _menu;
    private TasksController _tasks;
    private int _skillIndex;
    
    public void Fill (
        int skillIndex, 
        float skillValue, 
        float requiredValue, 
        string imgPath, 
        string skillName, 
        MainMenuController menu, 
        TasksController tasks) 
    {
        var floatRepresentation = skillValue.ToString("0.##");
        
        Icon.sprite = Resources.Load<Sprite>(imgPath);
        Value.text = floatRepresentation + "%";
        Name.text = skillName;

        _skillIndex = skillIndex;
        _menu = menu;
        _tasks = tasks;
        
        ProgressBarController.SetProgress(skillValue, requiredValue);
    }
    
    public void FillInactive (
        int skillIndex, 
        float skillValue, 
        float requiredValue, 
        string imgPath, 
        string skillName) 
    {
        var floatRepresentation = skillValue.ToString("0.##");
        
        Icon.sprite = Resources.Load<Sprite>(imgPath);
        Value.text = floatRepresentation + "%";
        Name.text = skillName;

        _skillIndex = skillIndex;
        
        ProgressBarController.SetProgress(skillValue, requiredValue);
    }

    public void OnClick() {
        _tasks.gameObject.SetActive(true);
        _tasks.OnEnter(_skillIndex);
        _menu.OnExit();
        _menu.gameObject.SetActive(false);
    }
}
