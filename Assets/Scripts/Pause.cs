using UnityEngine;

public class Pause : MonoBehaviour
{
    public void Stop()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
