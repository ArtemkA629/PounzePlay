using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void Load()
    {
        GameSettings.OnBoardingShowed = Convert.ToBoolean(PlayerPrefs.GetInt(SaveSystemConstants.OnboardingShowed));
        GameSettings.SetTotalScore(PlayerPrefs.GetInt(SaveSystemConstants.TotalScore));
        GameSettings.SetGoal(PlayerPrefs.GetInt(SaveSystemConstants.GoalCount));
        Background startBackground = null;
        var backgrounds = Resources.FindObjectsOfTypeAll<Background>();
        foreach (var background in backgrounds)
        {
            if (PlayerPrefs.GetInt(background.Info.Name) == 1)
                GameSettings.AvailiableBackgrounds.Add(background.Info);
            if (background.Info.Name == PlayerPrefs.GetString(SaveSystemConstants.CurrentBackground))
                GameSettings.CurrentBackground = background.Info;
            if (background.Info.ScoreToBuy == 0)
                startBackground = background;
        }

        if (GameSettings.GoalCount == 0)
            GameSettings.SetGoal(GoalConstants.AddingCount);

        if (GameSettings.CurrentBackground != null)
            return;

        GameSettings.CurrentBackground = startBackground.Info;
        GameSettings.AvailiableBackgrounds.Add(startBackground.Info);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(SaveSystemConstants.OnboardingShowed, Convert.ToInt32(GameSettings.OnBoardingShowed));
        PlayerPrefs.SetInt(SaveSystemConstants.TotalScore, GameSettings.TotalScore);
        PlayerPrefs.SetInt(SaveSystemConstants.GoalCount, GameSettings.GoalCount);
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        PlayerPrefs.SetString(SaveSystemConstants.CurrentBackground, GameSettings.CurrentBackground.Name);
        foreach (var background in GameSettings.AvailiableBackgrounds)
            PlayerPrefs.SetInt(background.Name, 1);
    }
}
