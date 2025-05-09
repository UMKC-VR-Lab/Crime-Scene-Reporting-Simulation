using System;
using Unity.Mathematics;
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
    public Slider incrementAmountSlider;
    public Text labelToggleAutoscroll;
    public Text labelScrollSpeed;
    public Text labelIncrementAmount;

    [Header("Scroll Settings")]
    public float scrollDefaultSpeed = 100f;
    public float scrollSpeed = 100f;
    public float scrollIncrement = 25f;
    public float incrementAmount; // How much to increment BEFORE considering lineLength variable
    private float lineLength = 172.5f; // Exact length of 1 line in the teleprompter at 150 font size (Length is 1.15x the Font Size)
    private float lineIncrement; // How many lines are incremented based on font size or line height
    private Vector2 startingPosition;
    private Vector2 scrollPosition;
    private bool isScrolling = false;
    
    //public float ypos;

    void Start()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();
        lineIncrement = lineLength * 3;
        startingPosition = contentTransform.anchoredPosition;
        scrollPosition = startingPosition;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        //SetIncrement(5);
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

    public void IncreaseIncrement()
    {
        incrementAmount += 1;
        UpdateIncrementAmountText();
    }

    public void DecreaseIncrement()
    {
        incrementAmount = Mathf.Max(0f, incrementAmount - 1);
        UpdateIncrementAmountText();
    }

    public void SetIncrement(float incrementNum)
    {
        incrementAmount = incrementNum > 0 ? incrementNum : 1;
        ConvertIncrementToLines(incrementAmount);
        UpdateIncrementAmountText();
    }

    public void SetIncrementBySlider()
    {
        incrementAmount = incrementAmountSlider.value;
        ConvertIncrementToLines(incrementAmount);
        UpdateIncrementAmountText();
    }

    public void ConvertIncrementToLines(float incrementAmount)
    {
        lineIncrement = lineLength * incrementAmount;
    }

    public void RewindByIncrement()
    {
        isScrolling = false;
        ConvertIncrementToLines(incrementAmount);
        UpdateToggleAutoscrollText();
        if ((scrollPosition.y - lineIncrement) > startingPosition.y) {
            scrollPosition.y -= lineIncrement;
            contentTransform.anchoredPosition = scrollPosition;

        } else {
            ResetContentPosition();
        }
    }

    public void ForwardByIncrement()
    {
        isScrolling = false;
        ConvertIncrementToLines(incrementAmount);
        UpdateToggleAutoscrollText();
        scrollPosition.y += lineIncrement;
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

    public void UpdateIncrementAmountText()
    {
        labelIncrementAmount.text = incrementAmount.ToString() + (incrementAmount == 1 ? " Line" : " Lines");
    }
}
