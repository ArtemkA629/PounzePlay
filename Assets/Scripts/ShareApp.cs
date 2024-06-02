using System.Collections;
using System.IO;
using UnityEngine;

public class ShareApp : MonoBehaviour
{
    public void ShareScore()
    {
        StartCoroutine(TakeScreenShotAndShare());
    }

    private IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();

        var tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx); 

        new NativeShare()
            .AddFile(path)
            .SetText("SHARE APP")
            .Share();
    }
}
