using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Webview : MonoBehaviour
{
    [SerializeField] private string _link;

    private bool _isRequestSucceed;

    private void Start()
    {
        StartCoroutine(GetRequest());
    }

    public bool TryOnOnboarding()
    {
        return _isRequestSucceed;
    }

    private IEnumerator GetRequest()
    {
        using UnityWebRequest request = UnityWebRequest.Get(_link);
        {
            yield return request.SendWebRequest();
            _isRequestSucceed = (request.isNetworkError || request.isHttpError) == false;
        }
    }
}
