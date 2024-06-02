using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void Load()
    {
        GameSettings.SetTotalScore(PlayerPrefs.GetInt(SaveSystemConstants.TotalScore));
        GameSettings.SetGoal(PlayerPrefs.GetInt(SaveSystemConstants.GoalCount));
        Background startBackground = null;
        var backgrounds = Resources.FindObjectsOfTypeAll<Background>();
        foreach (var background in backgrounds)
        {
            if (PlayerPrefs.GetInt(background.name) == 1)
                GameSettings.AvailiableBackgrounds.Add(background);
            if (background.name == PlayerPrefs.GetString(SaveSystemConstants.CurrentBackground))
                GameSettings.CurrentBackground = background;
            if (background.ScoreToBuy == 0)
                startBackground = background;
        }

        if (GameSettings.GoalCount == 0)
            GameSettings.SetGoal(GoalConstants.AddingCount);

        if (GameSettings.CurrentBackground != null)
            return;

        GameSettings.CurrentBackground = startBackground;
        GameSettings.AvailiableBackgrounds.Add(startBackground);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(SaveSystemConstants.TotalScore, GameSettings.TotalScore);
        PlayerPrefs.SetInt(SaveSystemConstants.GoalCount, GameSettings.GoalCount);
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        PlayerPrefs.SetString(SaveSystemConstants.CurrentBackground, GameSettings.CurrentBackground.name);
        foreach (var background in GameSettings.AvailiableBackgrounds)
            PlayerPrefs.SetInt(background.name, 1);
    }
}
