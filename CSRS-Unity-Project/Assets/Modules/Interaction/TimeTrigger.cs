using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    public bool debug = false;
    public bool startTimerOnStart = false;
    private bool isCountingDown = false;

    [SerializeField]
    private float timerDuration = 1.0f;

    public UnityEvent onTimerBegan, onTimerCancelled, onTimerCompleted;

    private void Start()
    {
        if(startTimerOnStart)
            StartTimer();
    }

    public void StartTimer()
    {
        if(debug) Debug.Log("Attempting to start timer");
        if(isCountingDown) return;
        if(debug) Debug.Log("Starting timer");

        isCountingDown = true;
        StartCoroutine(CountDown());
        onTimerBegan.Invoke();
    }

    public void CancelTimer()
    {
        if(debug) Debug.Log("Attempting to cancel timer");

        if(isCountingDown)
        {
            if(debug) Debug.Log("Cancelling timer");
            isCountingDown = false;
            StopAllCoroutines();
            onTimerCancelled.Invoke();
        }
    }

    private IEnumerator CountDown()
    {
        if(debug) Debug.Log("Counting down");
        yield return new WaitForSeconds(timerDuration);
        if(debug) Debug.Log("Timer Completed");
        if(isCountingDown)
        {
            isCountingDown = false;
            onTimerCompleted.Invoke();
        }
    }
}
