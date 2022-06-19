using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceMenuController : MonoBehaviour
{   [Header("Controllers")]
    [SerializeField] GameController gameController;
    [Header("Story")]
    [SerializeField] GameObject choice_menu;
    [SerializeField] GameObject button_prefab;
    [SerializeField] StorySceneInfo[] stories;
    

    private void Start()
    {       
        choice_menu.SetActive(false);
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
        choice_menu.SetActive(false);
        
    }
    public void NextChoices(List<Choice> choices)
    {
        
        if(choices == null)
            return;

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
    public void DeleteChoices()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ChoiceButton");

        foreach(GameObject button in buttons)
        {
            GameObject.Destroy(button,1);
        }
    }
}

[Serializable]
public class StorySceneInfo
{
    public string title;
    public StoryScene scene;
}
