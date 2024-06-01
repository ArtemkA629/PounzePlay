using System;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private int _totalScore;
    private int[] _backgroundNumbers;
    private int _activeBackGroundNumber;

    public int TotalScore
    {
        get { return _totalScore; }
        set { _totalScore = Mathf.Clamp(value, 0, int.MaxValue); }
    }

    public int[] BackgroundNumbers
    {
        get { return _backgroundNumbers; }
        set 
        {
            var exception = "Invalid background number";
            foreach (var i in value)
                if (i < 1 || i > 4)
                    throw new Exception(exception);
            if (value.ToHashSet().Count != value.Length)
                throw new Exception(exception);
            _backgroundNumbers = value;
        }
    }

    public int ActiveBackGroundNumber
    {
        get { return _activeBackGroundNumber; }
        set 
        {
            if (value < 1 || value > 4)
                throw new Exception("Invalid background number");
            _activeBackGroundNumber = value;
        }
    }
}
