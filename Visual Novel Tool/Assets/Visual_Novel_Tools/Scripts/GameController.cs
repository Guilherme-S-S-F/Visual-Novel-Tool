using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;

    private void Start()
    {
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.background);
        NextSentece();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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
    }
    
    private void NextSentece()
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
}
