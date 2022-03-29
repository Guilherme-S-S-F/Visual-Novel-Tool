using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI nameText;

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
    }
    public void PlayNextSentence()
    {
        StartCoroutine(TypingText(currentScene.sentences[++sentence_index].text));
        nameText.text = currentScene.sentences[sentence_index].speaker.name;
        nameText.color = currentScene.sentences[sentence_index].speaker.textColor;
    }
    public bool IsCompleted()
    {
        return state == State.Completed;
    }
    public bool IsLastSentence()
    {
        return sentence_index + 1 == currentScene.sentences.Count;
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
