using UnityEngine;
using UnityEngine.UI;

public class TeleprompterManager : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.05f;
    private bool isScrolling = false;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        // Automatically scrolls the text on Teleprompter when isScrolling is true
        if (isScrolling){
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }

    // Function that starts or stops the teleprompter on Push Button select
    public void ToggleTeleprompter(){
        if (isScrolling){
            isScrolling = false;
        } else {
            isScrolling = true;
        }
    }
}
