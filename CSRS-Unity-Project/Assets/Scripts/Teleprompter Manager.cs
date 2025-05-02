using UnityEngine;
using UnityEngine.UI;

public class TeleprompterManager : MonoBehaviour
{
    [Header("UI References")]
    public ScrollRect scrollRect;
    public RectTransform contentTransform;

    [Header("Scroll Settings")]
    public float scrollDefaultSpeed = 100f;
    public float scrollSpeed = 100f;
    public float scrollIncrement = 25f;
    public float backLinesAmount = 500f; // Adjust based on font size or line height

    private Vector2 startingPosition;
    private Vector2 scrollPosition;
    private bool isScrolling = false;
    
    //public float ypos;

    void Start()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        startingPosition = contentTransform.anchoredPosition;
        scrollPosition = startingPosition;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    void Update()
    {
        if (isScrolling)
        {
            scrollPosition.y += scrollSpeed * Time.deltaTime;
            contentTransform.anchoredPosition = scrollPosition;
        }
        //ypos = scrollPosition.y;
    }

    public void ToggleAutoscroll()
    {
        isScrolling = !isScrolling;
    }

    public void ResetContentPosition()
    {
        isScrolling = false;
        scrollPosition = startingPosition;
        contentTransform.anchoredPosition = scrollPosition;
    }

    public void IncreaseScrollSpeed()
    {
        scrollSpeed += scrollIncrement;
    }

    public void DecreaseScrollSpeed()
    {
        scrollSpeed = Mathf.Max(0f, scrollSpeed - scrollIncrement);
    }

    public void ResetScrollSpeed()
    {
        scrollSpeed = scrollDefaultSpeed;
    }

    public void BackALine()
    {
        isScrolling = false;
        if ((scrollPosition.y - backLinesAmount) > startingPosition.y) {
            scrollPosition.y -= backLinesAmount;
            contentTransform.anchoredPosition = scrollPosition;

        } else {
            ResetContentPosition();
        }
    }

    public bool IsScrolling()
{
    return isScrolling;
}

}
