using UnityEngine;
using UnityEngine.UI;

public class TeleprompterManager : MonoBehaviour
{
    [Header("UI References")]
    public ScrollRect scrollRect;
    public RectTransform contentTransform;

    [Header("Scroll Settings")]
    public float scrollSpeed = 0.05f;
    public float scrollIncrement = 0.01f;
    public float backLinesAmount = 50f; // Adjust based on font size or line height

    private Vector2 startingPosition;
    private Vector2 scrollPosition;
    private bool isScrolling = false;

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
    }

    public void ToggleAutoscroll()
    {
        isScrolling = !isScrolling;
    }

    public void ResetContentPosition()
    {
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

    public void BackALine()
    {
        isScrolling = false;
        scrollPosition.y -= backLinesAmount;
        contentTransform.anchoredPosition = scrollPosition;
    }

    public bool IsScrolling()
{
    return isScrolling;
}

}
