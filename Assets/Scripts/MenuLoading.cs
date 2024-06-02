using UnityEngine;

public class MenuLoading : MonoBehaviour
{
    private void OnEnable()
    {
        SaveSystem.Load();
    }

    [ContextMenu("ResetData")]
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
