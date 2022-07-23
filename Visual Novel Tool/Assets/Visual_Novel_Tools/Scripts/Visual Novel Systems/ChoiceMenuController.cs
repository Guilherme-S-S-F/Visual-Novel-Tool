using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceMenuController : MonoBehaviour
{   [Header("Controllers")]
    [SerializeField] GameController gameController;
    [Header("objects")]
    [SerializeField] GameObject choice_menu;
    [SerializeField] GameObject question_holder;
    [SerializeField] GameObject clickDisablePanel;
    [Header("Story")]
    [SerializeField] GameObject button_prefab;
    [SerializeField] StorySceneInfo[] stories;
    private TextMeshProUGUI question_Text;
    

    private void Start()
    {
        question_Text = question_holder.GetComponentInChildren<TextMeshProUGUI>();
        HideHolders();
    }
    [SerializeField]
    public void SelectedChoice(string name)
    {
        StoryScene scene = null;
        foreach(StorySceneInfo story in stories)
        {
            if(story.title == name)
            {
                scene = story.scene;
            }
        }        
        if (scene != null)
        {
            gameController.currentScene.nextScene = scene;            
        }
        else
        {
            Debug.LogError("Scene not exist error.");
        }
        
        gameController.NextSentece();
        gameController.choiceFlag = false;
        DeleteChoices();
        HideHolders();

    }
    // Takes the next choice from the storyscene if isn't null
    public void NextChoices(List<Choice> choices)
    {
        
        if(choices == null)
            return;

        clickDisablePanel.SetActive(true);
        choice_menu.SetActive(true);        

        foreach (Choice choice in choices)
        {
            GameObject button_object = Instantiate(button_prefab, choice_menu.transform);
            button_object.GetComponentInChildren<TextMeshProUGUI>().text = choice.choice_text;

            Button button = button_object.GetComponent<Button>();
            if (choice.ChapterName != null)
            {
                button.onClick = new Button.ButtonClickedEvent();
                button.onClick.AddListener(() => SelectedChoice(choice.ChapterName));
            }

        }
        gameController.choiceFlag = true;
    }
    // Takes the next choice from the storyscene if isn't null and give the question on the top
    public void NextChoices(List<Choice> choices, string question)
    {

        if (choices == null)
            return;

        ShowHolders();

        question_Text.text = question;

        foreach (Choice choice in choices)
        {
            GameObject button_object = Instantiate(button_prefab, choice_menu.transform);
            button_object.GetComponentInChildren<TextMeshProUGUI>().text = choice.choice_text;

            Button button = button_object.GetComponent<Button>();
            if (choice.ChapterName != null)
            {
                button.onClick = new Button.ButtonClickedEvent();
                button.onClick.AddListener(() => SelectedChoice(choice.ChapterName));
            }

        }
        gameController.choiceFlag = true;
    }
    public void DeleteChoices()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ChoiceButton");

        foreach(GameObject button in buttons)
        {
            GameObject.Destroy(button,1);
        }
    }
    #region Holders
    private void HideHolders()
    {
        clickDisablePanel.SetActive(false);
        choice_menu.SetActive(false);
        question_holder.SetActive(false);
    }
    private void ShowHolders()
    {
        clickDisablePanel.SetActive(true);
        choice_menu.SetActive(true);
        question_holder.SetActive(true);
    }
    #endregion
}

[Serializable]
public class StorySceneInfo
{
    public string title;
    public StoryScene scene;
}
