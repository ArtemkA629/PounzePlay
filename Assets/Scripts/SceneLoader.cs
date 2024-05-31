using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Load(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }
}
