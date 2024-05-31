using System;
using UnityEngine;

public static class GameSettings
{
    public static Sprite GoalSprite;
    public static BowlType BowlType;

    public static int GoalCount { get; private set; }
    public static int TotalScore { get; private set; }

    public static void SetGoal(int goal)
    {
        if (goal < 0)
            throw new Exception("Invalid goal");
        GoalCount = goal;
    }

    public static void SetTotalScore(int score)
    {
        if (score < 0)
            throw new Exception("Invalid score");
        TotalScore = score;
    }
}
