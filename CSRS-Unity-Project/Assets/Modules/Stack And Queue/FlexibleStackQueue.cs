using System;
using System.Collections.Generic;

[System.Serializable]
public class FlexibleStackQueue<T>
{
    private readonly LinkedList<T> _list = new();
    public enum Mode { FIFO, LIFO }
    public Mode _mode;

    public FlexibleStackQueue(Mode mode)
    {
        _mode = mode;
    }

    public void ToggleMode()
    {
        _mode = _mode == Mode.FIFO ? Mode.LIFO : Mode.FIFO;
    }

    public void Enqueue(T item)
    {
        _list.AddLast(item);
    }

    public T Dequeue()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException("Structure is empty.");

        T item;
        if (_mode == Mode.FIFO)
        {
            item = _list.First.Value;
            _list.RemoveFirst();
        }
        else // LIFO
        {
            item = _list.Last.Value;
            _list.RemoveLast();
        }

        return item;
    }

    public int Count => _list.Count;

    public override string ToString()
    {
        return string.Join(", ", _list);
    }
}
