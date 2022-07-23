using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewStoryScene", menuName ="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite background;
    public StoryScene nextScene;
    public StoryScene prevScene;
    public AudioClip music;

    
}
[System.Serializable]
public class Sentence
{
    public string text;
    public bool ShowQuestion;
    public string question;
    public List<Choice> choices;
    public List<Char> character;
    public Speaker speaker;    
    public AudioClip sound;
}
[System.Serializable]
public class Choice
{
    public string choice_text;
    public int public_id;
    public int local_id;
    public string ChapterName;    
}
[System.Serializable]
public class Char
{
    public bool delete;
    public GameObject character;
    public Vector2 position;
    public float speed;
    public bool smooth;
}
