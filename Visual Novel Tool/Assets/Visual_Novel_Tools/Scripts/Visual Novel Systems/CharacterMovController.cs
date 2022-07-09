using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovController : MonoBehaviour
{
    [SerializeField] private GameObject CharHolder;
    [SerializeField] private GameObject CharImage;

    [SerializeField] public Sprite CharSprite;

    private int width = Screen.width;
    private int height = Screen.height;

    private void Start()
    {
        CharHolder = this.gameObject;
        CharImage = this.gameObject.GetComponentInChildren<Image>().gameObject;
    }
}
