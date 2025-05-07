using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleList : MonoBehaviour
{
    public enum Mode { FIFO, LIFO }

    public List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };
    public Mode mode = Mode.FIFO;
    public bool isCycling = false;

    private Coroutine cycleRoutine;

    public void StartCycling()
    {
        if (!isCycling)
        {
            isCycling = true;
            cycleRoutine = StartCoroutine(Cycle());
        }
    }

    public void StopCycling()
    {
        if (isCycling)
        {
            isCycling = false;
            StopCoroutine(cycleRoutine);
        }
    }

    public void ToggleMode()
    {
        mode = mode == Mode.FIFO ? Mode.LIFO : Mode.FIFO;
    }

    public void AddNumber()
    {
        int nextVal = 0;
        if(numbers.Count > 0)
        {
            nextVal = numbers[numbers.Count - 1] + 1;
        }
        numbers.Add(nextVal);
    }

    IEnumerator Cycle()
    {
        while (isCycling && numbers.Count > 0)
        {
            int value;

            if (mode == Mode.FIFO)
            {
                value = numbers[0];
                numbers.RemoveAt(0);
            }
            else // LIFO
            {
                int last = numbers.Count - 1;
                value = numbers[last];
                numbers.RemoveAt(last);
            }

            Debug.Log($"Cycled: {value}");

            yield return new WaitForSeconds(1f);
        }

        isCycling = false;
    }
}
