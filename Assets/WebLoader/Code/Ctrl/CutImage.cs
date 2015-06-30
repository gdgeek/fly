using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GDGeek;
using GDGeek.WebVox;

public class CutImage : MonoBehaviour {

	public Camera _camera = null;
	//public RawImage _image = null;
	public Texture2D _texture  = null;
	private bool cutOver_ = true;
	void Start () {
		TaskManager.Run (execute ());
	}
	public Task execute(){
		TaskList tl = new TaskList ();
		tl.push (cutTask ());
		tl.push (uploadTask ());
		return tl;
	}
	public Task cutTask(){
		Task task = new Task ();
		task.init = delegate {
			this.cutOver_ = false;
			this.StartCoroutine(cutImage ());
				};
		task.isOver = delegate {
			return this.cutOver_;
		};

		return task;
	}
	public Task uploadTask(){
		return new TaskPack(delegate {
			WebLoaderTask<WebUploadInfo> web = new WebLoaderTask<WebUploadInfo> ("http://gdgeek.com/www/index.php/voxel/upload");
			web.onSucceed += delegate(WebUploadInfo info) {
			};
			web.onError += delegate(string msg) {
				Debug.Log(msg);
			};
			web.pack.addField("image", "llalaa");
			web.pack.addBinaryData ("post", _texture.EncodeToPNG ());
			return web;
		});
	
	}

	IEnumerator cutImage(){
		_texture = new Texture2D ((int)(_camera.rect.width * Screen.width), (int)(_camera.rect.height * Screen.height), TextureFormat.RGB24, false);
		Rect rect = new Rect (_camera.rect.x * Screen.width, _camera.rect.y * Screen.height,_camera.rect.width * Screen.width, _camera.rect.height * Screen.height);
		yield return new WaitForEndOfFrame();
		_texture.ReadPixels (rect, 0, 0, false);
		_texture.Apply ();
		yield return _texture;
		cutOver_ = true;
	}
}
