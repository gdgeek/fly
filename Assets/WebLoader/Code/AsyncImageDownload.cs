using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
#if !UNITY_WEBPLAYER
public class AsyncImageDownload : MonoBehaviour {


	public Texture placeholder;
	public static AsyncImageDownload  Instance = null;
	private string path = Application.persistentDataPath+"/ImageCache/" ;
	public static AsyncImageDownload CreateSingleton()
	{
		if (!Directory.Exists(Application.persistentDataPath+"/ImageCache/")) {
				Directory.CreateDirectory(Application.persistentDataPath+"/ImageCache/");
		}
				GameObject obj = new GameObject ();
			obj.AddComponent<AsyncImageDownload> ();
		AsyncImageDownload loader= obj.GetComponent<AsyncImageDownload>();
		Instance=loader;
		loader.placeholder=Resources.Load("placeholder") as Texture;
		return loader;
	
	}


	public  void SetAsyncImage(string url,RawImage image){
		image.texture = placeholder;

		if (!File.Exists (path + url.GetHashCode())) {
				//如果之前不存在缓存文件
			StartCoroutine (DownloadImage (url, image));
		
		}
		else {
			StartCoroutine(LoadLocalImage(url,image));
		
		}
	}
	IEnumerator  DownloadImage(string url,RawImage image){
		Debug.Log("downloading new image:"+path+url.GetHashCode());
		WWW www = new WWW (url);
		yield return www;
		Texture2D texture = www.texture;
		byte[] pngData = texture.EncodeToPNG(); 
		File.WriteAllBytes (path + url.GetHashCode (), pngData);
		//File.WriteAllBytes
		//F
		File.WriteAllBytes(path+url.GetHashCode(), pngData); 
		image.texture = texture;
	}
	IEnumerator  LoadLocalImage(string url,RawImage image){
		string filePath = "file:///" + path + url.GetHashCode ();
		Debug.Log("getting local image:"+filePath);
		WWW www = new WWW (filePath);
		yield return www;
		//直接贴图
		image.texture = www.texture;

	}

}
#endif