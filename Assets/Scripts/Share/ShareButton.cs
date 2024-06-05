using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class ShareButton : MonoBehaviour
{
    public void Share(string url)
    {
        new Sharing().AddFile(url).SetText("Share PounzePlay!").Share();
    }
}
