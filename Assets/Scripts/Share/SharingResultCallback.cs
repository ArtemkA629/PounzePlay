#if UNITY_EDITOR || UNITY_IOS
using UnityEngine;

public class SharingResultCallback : MonoBehaviour
{
    private static SharingResultCallback instance;
    private Sharing.ShareResultCallback callback;

    public static void Initialize(Sharing.ShareResultCallback callback)
    {
        if (instance == null)
        {
            instance = new GameObject("SharingResultCallback").AddComponent<SharingResultCallback>();
            DontDestroyOnLoad(instance.gameObject);
        }
        else if (instance.callback != null)
            instance.callback(Sharing.ShareResult.Unknown, null);

        instance.callback = callback;
    }

    [UnityEngine.Scripting.Preserve]
    public void OnShareCompleted(string message)
    {
        Sharing.ShareResultCallback _callback = callback;
        callback = null;

        if (_callback != null)
        {
            if (string.IsNullOrEmpty(message))
                _callback(Sharing.ShareResult.Unknown, null);
            else
            {
                Sharing.ShareResult result = (Sharing.ShareResult)(message[0] - '0');
                string shareTarget = message.Length > 1 ? message.Substring(1) : null;

                _callback(result, shareTarget);
            }
        }
    }
}
#endif
