using UnityEngine;

public class WebLink : MonoBehaviour
{
    public void Open(string path)
    {
        Application.OpenURL(path);
    }
}
