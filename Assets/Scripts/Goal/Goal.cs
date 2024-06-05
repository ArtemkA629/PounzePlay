using System;
using UnityEngine;

public class Goal
{
    private readonly Sprite _sprite;
    private readonly int _countAtStart;

    private int _count;

    public Goal(Sprite sprite, int count)
    {
        _sprite = sprite;
        _countAtStart = count;
        _count = count;
    }

    public event Action<int> Collected;
    public event Action Won;

    public Sprite Sprite => _sprite;
    public int CountAtStart => _countAtStart;
    public int Count => _count;

    public void SubtractCount()
    {
        if (_count - 1 < 0)
            throw new Exception("Invalid count");
        _count--;
        Collected?.Invoke(_count);

        if (_count == 0)
            Won?.Invoke();
    }
}
