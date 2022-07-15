using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool mouseAboveText;

    public bool choiceFlag = false;
    public bool pause = false;
    public GameObject choiceMenu;
    public StoryScene currentScene;
    public BottomBarController bottomBar;    
    public BackgroundController backgroundController;
    public CharacterManager characterManager;

    public Vector2 Position;
    public bool smooth;
    public float speed;

    private void Start()
    {
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.background);
        NextSentece();

    }
    private void Update()
    {
        if (!pause)
        {
            if (Input.GetKeyDown(KeyCode.Space) || ClickOnTextMenu())
            {
                NextSentece();
                bottomBar.max_sentence_viewed++;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                PreviousSentence();
            }
            else if (Input.mouseScrollDelta.y > 0 && bottomBar.sentence_counter <= bottomBar.max_sentence_viewed)
            {
                NextSentece();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                characterManager.MoveTo(Position, speed, smooth);
            }
        }
    }
    
    public void NextSentece()
    {
        if (bottomBar.IsCompleted())
        {
            if (bottomBar.IsLastSentence())
            {
                currentScene = currentScene.nextScene;
                bottomBar.PlayScene(currentScene);
                backgroundController.SwitchImage(currentScene.background);
            }
            bottomBar.PlayNextSentence();
        }
    }
    private void PreviousSentence()
    {
        if (bottomBar.IsCompleted())
        {
            if (bottomBar.IsFirstSentence())
            {

                if (currentScene.prevScene != null)
                {
                    currentScene = currentScene.prevScene;
                    bottomBar.PlayLastScene(currentScene);
                    backgroundController.SwitchImage(currentScene.background);
                }
            }
            bottomBar.PlayPreviousSentence();            
        }
    }
    public void MouseEnter()
    {
        mouseAboveText = true;
    }
    public void MouseExit()
    {
        mouseAboveText = false;
    }

    private bool ClickOnTextMenu()
    {
        bool boolean = false;
        if (Input.GetMouseButtonDown(0) && mouseAboveText)
        {
            boolean = true;
        }
        return boolean;
    }
}
