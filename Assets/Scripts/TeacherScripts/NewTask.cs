using System;
using TeacherScripts;
using UnityEngine;
using UnityEngine.UI;

public class NewTask : MonoBehaviour {

    public TasksManagement ParentScreen;

    // Common Fields
    public GameObject IntroScreen;
    public GameObject[] TaskScreen;
    public InputField TaskTitle, ImproveAmount;
    public Dropdown ImprovedSkill;
    public Button Theory, Practice, Case;
    
    // Theory Fields
    public Transform QuestionsPanel;
    public GameObject QuestionsCreationScreen;
    public InputField TheoryText, QuestionsCount;
    public TheoryCreatedQuestion CreatedQuestionExample;

    private int _theoryQuestionsCount;
    private TheoryCreatedQuestion[] _theoryQuestions;
    
    // Practice Fields
    public InputField PracticeText;
    
    // Case Fields
    public InputField CaseText, CaseTaskDescription;
    
    // Task Creation Fields
    public string TaskName;
    public string TaskText;
    public string[] Questions;
    public string[][] Answers;
    public int Type;
    public int SkillPromoted;
    public int[][] CorrectAnswers;
    public float SkillIncrese;

    public void OnEnter() {
        Theory.interactable = false;
        Practice.interactable = false;
        Case.interactable = false;
        TaskTitle.onValueChanged.AddListener(delegate { OnPossibleUnlock(); });
        ImproveAmount.onValueChanged.AddListener(delegate { OnPossibleUnlock(); });
    }

    public void OnPossibleUnlock() {
        if (TaskTitle.text.Length == 0 || ImproveAmount.text.Length == 0) return;
        Theory.interactable = true;
        Practice.interactable = true;
        Case.interactable = true;
    }

    public void OnTaskCreate(int taskType) {
        TaskName = TaskTitle.text;
        SkillPromoted = ImprovedSkill.value;
        SkillIncrese = Convert.ToSingle(ImproveAmount.text);
        Type = taskType;

        TaskScreen[taskType].SetActive(true);
        IntroScreen.SetActive(false);
    }

    public void OnGenerateQuesitons() {
        _theoryQuestionsCount = Convert.ToInt32(QuestionsCount.text);
        _theoryQuestions = new TheoryCreatedQuestion[_theoryQuestionsCount];

        for (var i = 0; i < _theoryQuestionsCount; i++) {
            var newQuestion = Instantiate(CreatedQuestionExample, QuestionsPanel);
            _theoryQuestions[i] = newQuestion;
        }
    }

    public void OnSubmitQuestions() {
        
    }

    public void OnCancel() {
        TaskTitle.text = "";
        ImproveAmount.text = "";
        TheoryText.text = "";
        QuestionsCount.text = "";
        PracticeText.text = "";
        CaseText.text = "";
        CaseTaskDescription.text = "";
        
        TaskTitle.onValueChanged.RemoveAllListeners();
        ImproveAmount.onValueChanged.RemoveAllListeners();
        
        ParentScreen.OnEnter();
        gameObject.SetActive(false);
    }

    public void OnSubmitTask() {
        
        OnCancel();
    }
    
}
