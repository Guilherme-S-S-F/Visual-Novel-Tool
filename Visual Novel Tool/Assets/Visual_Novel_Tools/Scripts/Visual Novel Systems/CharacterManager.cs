using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]Character character;
    [SerializeField] GameObject body;
    RectTransform root;
    Image charImage;

    public Vector2 anchorPadding { get { return this.GetComponent<RectTransform>().anchorMax - this.GetComponent<RectTransform>().anchorMin; } }

    private void Start()
    {
        character = GetComponent<Character>();
        body = this.transform.GetChild(0).gameObject;
        charImage = GetComponentInChildren<Image>();
        ReajustImage();
        root = this.GetComponent<RectTransform>();
    }   
    void ReajustImage()
    {
        body.transform.localScale = new Vector2(character.XScale, character.YScale);
    }

    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        StopMoving();
        moving = this.StartCoroutine(Moving(Target, speed, smooth));
    }
    public void StopMoving()
    {
        if (isMoving)
        {
            this.StopCoroutine(moving);
        }
        moving = null;
    }
    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;
        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);

        speed *= Time.deltaTime;

        while(root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }
}
