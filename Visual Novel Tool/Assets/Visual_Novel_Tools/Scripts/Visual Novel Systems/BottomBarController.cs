using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI nameText;    

    public AudioSystem audioSystem;
    public ChoiceMenuController choiceMenuController;

    public int max_sentence_viewed = 0;
    public int sentence_counter = 0;

    private int sentence_index = -1;
    private StoryScene currentScene;
    private State state = State.Completed;


    [Range(0.01f,1f)][SerializeField]float typingSpeed = 0.05f;
    public float TypingSpeed
    {
        get { return typingSpeed; }
        set { typingSpeed = value; }
    }

    private enum State
    {
        Playing,
        Completed
    }
    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;        
        sentence_index = -1;
        if (currentScene.music != null)
        {
            audioSystem.PlayMusic(currentScene.music);
        }
    }
    public void PlayLastScene(StoryScene scene)
    {
        currentScene = scene;
        sentence_index = scene.sentences.Count;
    }
    
    public void PlayNextSentence()
    {
        StartCoroutine(TypingText(currentScene.sentences[++sentence_index].text));
        nameText.text = currentScene.sentences[sentence_index].speaker.name;
        nameText.color = currentScene.sentences[sentence_index].speaker.textColor;
        sentence_counter++;

        //If the sentence had a sound, the audiosource will play the sound.
        if (currentScene.sentences[sentence_index].sound != null)
        {
            audioSystem.SoundMusic(currentScene.sentences[sentence_index].sound);
        }
        //If the sentence had choices, will show the choice menu with the choices. If not will disable the choice menu.
        if (currentScene.sentences[sentence_index].choices != null && currentScene.sentences[sentence_index].choices.Count != 0)
        {
            // if this choice have to show or not the question on top
            if (currentScene.sentences[sentence_index].ShowQuestion)
            {
                choiceMenuController.NextChoices(currentScene.sentences[sentence_index].choices,
                    currentScene.sentences[sentence_index].question);
            }
            else
            {
                choiceMenuController.NextChoices(currentScene.sentences[sentence_index].choices);
            }
        }
        
    }
    public void PlayPreviousSentence()
    {        
        if (sentence_index -1 != -1)
        {
            StartCoroutine(TypingText(currentScene.sentences[--sentence_index].text));
            nameText.text = currentScene.sentences[sentence_index].speaker.name;
            nameText.color = currentScene.sentences[sentence_index].speaker.textColor;
            sentence_counter--;
            if (currentScene.sentences[sentence_index].sound != null)
            {
                audioSystem.SoundMusic(currentScene.sentences[sentence_index].sound);
            }
        }
    }
    public bool IsCompleted()
    {
        return state == State.Completed;
    }
    public bool IsLastSentence()
    {
        return sentence_index + 1 == currentScene.sentences.Count;
    }
    public bool IsFirstSentence()
    {
        return sentence_index == 0;
    }
    private IEnumerator TypingText(string text)
    {
        barText.text = "";
        state = State.Playing;
        int wordIndex = 0;

        while(state != State.Completed)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(typingSpeed);
            if(++wordIndex == text.Length)
            {
                state = State.Completed;
            }
        }
    }
}
