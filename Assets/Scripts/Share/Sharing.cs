using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Sharing
{
    public enum ShareResult { Unknown = 0, Shared = 1, NotShared = 2 };

    public delegate void ShareResultCallback(ShareResult result, string shareTarget);

#if !UNITY_EDITOR && UNITY_IOS
	[System.Runtime.InteropServices.DllImport( "__Internal" )]
	private static extern void _Share( string[] files, int filesCount, string subject, string text, string link );
#endif

    private string subject = string.Empty;
    private string text = string.Empty;
    private string title = string.Empty;
    private string url = string.Empty;

    private readonly List<string> files = new List<string>(0);
    private readonly List<string> mimes = new List<string>(0);

    private ShareResultCallback callback;

    public Sharing Clear()
    {
        subject = text = title = url = string.Empty;
        files.Clear();
        mimes.Clear();
        callback = null;
        return this;
    }

    public Sharing SetSubject(string subject)
    {
        this.subject = subject ?? string.Empty;
        return this;
    }

    public Sharing SetText(string text)
    {
        this.text = text ?? string.Empty;
        return this;
    }

    public Sharing SetUrl(string url)
    {
        this.url = url ?? string.Empty;
        return this;
    }

    public Sharing SetTitle(string title)
    {
        this.title = title ?? string.Empty;
        return this;
    }

    public Sharing SetCallback(ShareResultCallback callback)
    {
        this.callback = callback;
        return this;
    }

    public Sharing AddFile(string filePath, string mime = null)
    {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            files.Add(filePath);
            mimes.Add(mime ?? string.Empty);
        }
        else
            Debug.LogError("Share Error: file does not exist at path or permission denied: " + filePath);

        return this;
    }

    public Sharing AddFile(Texture2D texture, string createdFileName = "Image.png")
    {
        if (!texture)
            Debug.LogError("Share Error: Texture does not exist!");
        else
        {
            if (string.IsNullOrEmpty(createdFileName))
                createdFileName = "Image.png";

            bool saveAsJpeg;
            if (createdFileName.EndsWith(".jpeg", System.StringComparison.OrdinalIgnoreCase) || 
                createdFileName.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase))
                saveAsJpeg = true;
            else
            {
                if (!createdFileName.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase))
                    createdFileName += ".png";

                saveAsJpeg = false;
            }

            string filePath = Path.Combine(Application.temporaryCachePath, createdFileName);
            File.WriteAllBytes(filePath, GetTextureBytes(texture, saveAsJpeg));

            AddFile(filePath, saveAsJpeg ? "image/jpeg" : "image/png");
        }

        return this;
    }

    public void Share()
    {
        if (files.Count == 0 && subject.Length == 0 && text.Length == 0 && url.Length == 0)
        {
            Debug.LogWarning("Share Error: attempting to share nothing!");
            return;
        }

#if UNITY_EDITOR
        Debug.Log("Shared!");

        if (callback != null)
            callback(ShareResult.Shared, null);
#elif UNITY_IOS
		SharingResultCallback.Initialize( callback );
		if( files.Count == 0 )
			_Share( new string[0], 0, subject, text, GetURLWithScheme() );
		else
			_Share( files.ToArray(), files.Count, subject, CombineURLWithText(), "" );
#else
		Debug.LogWarning( "NativeShare is not supported on this platform!" );
#endif
    }

    #region Internal Functions
    private string GetURLWithScheme()
    {
        return (url.Length == 0 || url.Contains("://")) ? url : ("https://" + url);
    }

    private string CombineURLWithText()
    {
        if (url.Length == 0 || text.IndexOf(url, System.StringComparison.OrdinalIgnoreCase) >= 0)
            return text;
        else if (text.Length == 0)
            return GetURLWithScheme();
        else
            return string.Concat(text, " ", GetURLWithScheme());
    }

    private byte[] GetTextureBytes(Texture2D texture, bool isJpeg)
    {
        try
        {
            return isJpeg ? texture.EncodeToJPG(100) : texture.EncodeToPNG();
        }
        catch (UnityException)
        {
            return GetTextureBytesFromCopy(texture, isJpeg);
        }
        catch (System.ArgumentException)
        {
            return GetTextureBytesFromCopy(texture, isJpeg);
        }
    }

    private byte[] GetTextureBytesFromCopy(Texture2D texture, bool isJpeg)
    {
        Debug.LogWarning("Sharing non-readable textures is slower than sharing readable textures");

        Texture2D sourceTexReadable = null;
        RenderTexture rt = RenderTexture.GetTemporary(texture.width, texture.height);
        RenderTexture activeRT = RenderTexture.active;

        try
        {
            Graphics.Blit(texture, rt);
            RenderTexture.active = rt;

            sourceTexReadable = new Texture2D(texture.width, texture.height, isJpeg ? TextureFormat.RGB24 : TextureFormat.RGBA32, false);
            sourceTexReadable.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0, false);
            sourceTexReadable.Apply(false, false);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);

            Object.DestroyImmediate(sourceTexReadable);
            return null;
        }
        finally
        {
            RenderTexture.active = activeRT;
            RenderTexture.ReleaseTemporary(rt);
        }

        try
        {
            return isJpeg ? sourceTexReadable.EncodeToJPG(100) : sourceTexReadable.EncodeToPNG();
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            return null;
        }
        finally
        {
            Object.DestroyImmediate(sourceTexReadable);
        }
    }
    #endregion
}