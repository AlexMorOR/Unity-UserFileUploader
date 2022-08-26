using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public Image avatarImage;

    public void UpdateAvatar()
    {
        FileUploaderHelper.RequestFile((path) => 
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            StartCoroutine(UploadImage(path));    
        });
    }

    IEnumerator UploadImage(string path)
    {
        Texture2D texture;

        using (UnityWebRequest imageWeb = new UnityWebRequest(path, UnityWebRequest.kHttpVerbGET))
        {

            imageWeb.downloadHandler = new DownloadHandlerTexture();

            yield return imageWeb.SendWebRequest();

            texture = ((DownloadHandlerTexture)imageWeb.downloadHandler).texture;
        }

        avatarImage.sprite = Sprite.Create(
            texture, 
            new Rect(0.0f, 0.0f, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f));
    }
}
