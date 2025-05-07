using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class TeleprompterManager : MonoBehaviour
{
    [Header("UI References")]
    public ScrollRect scrollRect;
    public RectTransform contentTransform;
    public Slider scrollSpeedSlider;

    [Header("Scroll Settings")]
    public float scrollDefaultSpeed = 100f;
    public float scrollSpeed = 100f;
    public float scrollIncrement = 25f;
    public float lineAdjustAmount; // Adjust based on font size or line height
    public Text labelToggleAutoscroll;
    public Text labelScrollSpeed;
    private float lineLength = 172.5f; // Exact length of 1 line in the teleprompter at 150 font size (Length is 1.15x the Font Size)
    private Vector2 startingPosition;
    private Vector2 scrollPosition;
    private bool isScrolling = false;
    
    //public float ypos;

    void Start()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();
        lineAdjustAmount = lineLength * 5;
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
        UpdateToggleAutoscrollText();
        scrollPosition = startingPosition;
        contentTransform.anchoredPosition = scrollPosition;
    }
    public void RewindSpecifiedLineAmount()
    {
        isScrolling = false;
        UpdateToggleAutoscrollText();
        if ((scrollPosition.y - lineAdjustAmount) > startingPosition.y) {
            scrollPosition.y -= lineAdjustAmount;
            contentTransform.anchoredPosition = scrollPosition;

        } else {
            ResetContentPosition();
        }
    }

    public void ForwardSpecifiedLineAmount()
    {
        isScrolling = false;
        UpdateToggleAutoscrollText();
        scrollPosition.y += lineAdjustAmount;
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

    public void UpdateScrollSpeed()
    {
        scrollSpeed = Mathf.Round(scrollSpeedSlider.value / 10) * 10;
        UpdateScrollSpeedText();
    }
    public void ResetScrollSpeed()
    {
        scrollSpeedSlider.value = scrollDefaultSpeed;
        //scrollSpeed = scrollDefaultSpeed;
        UpdateScrollSpeedText();
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
        labelScrollSpeed.text = multiplier.ToString("0.0") + "x";
    }


}
