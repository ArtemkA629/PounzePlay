using UnityEngine;

public class MenuLoading : MonoBehaviour
{
    public void SetOnboardingInfo()
    {
        GameSettings.OnBoardingShowed = true;
    }

    private void OnEnable()
    {
        SaveSystem.Load();
    }

    private void OnDisable()
    {
        SaveSystem.Save();
    }

    [ContextMenu("ResetData")]
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
