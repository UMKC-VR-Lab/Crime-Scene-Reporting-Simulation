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
    public Text labelToggleAutoscroll;
    public Text labelScrollSpeed;
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
        UpdateToggleAutoscrollText();
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
        UpdateScrollSpeedText();
    }

    public void DecreaseScrollSpeed()
    {
        scrollSpeed = Mathf.Max(0f, scrollSpeed - scrollIncrement);
        UpdateScrollSpeedText();
    }

    public void ResetScrollSpeed()
    {
        scrollSpeed = scrollDefaultSpeed;
        UpdateScrollSpeedText();
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

    public void UpdateToggleAutoscrollText()
    {
        if (labelToggleAutoscroll != null)
            labelToggleAutoscroll.text = isScrolling ? "Pause Autoscroll" : "Start Autoscroll";
    }

    public void UpdateScrollSpeedText()
    {
        float multiplier = scrollSpeed / 100f;
        labelScrollSpeed.text = multiplier.ToString("0.00") + "x";
    }


}
