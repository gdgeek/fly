using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GDGeek.WebVox
{
	public class Item: MonoBehaviour 
	{
		public RectTransform _rt = null;
		public RawImage _icon;
		public Text _text;
		private string mesh_;
		


		
		public string mesh {
			get{
				return mesh_;
			}
		}
		public void setup(string title, string mesh, string iconUrl){
			if (!this.gameObject.activeInHierarchy) {
				return;			
			}
			_text.text = title;
			mesh_ = mesh;
			if (!string.IsNullOrEmpty(iconUrl)) {
				Debug.Log (iconUrl);
				StartCoroutine(loadIcon(iconUrl));	
			}
		}
		private IEnumerator loadIcon(string iconUrl)
		{
			Debug.Log (iconUrl +"!!");
			WWW www = new WWW(iconUrl);
			yield return www;

			this._icon.texture = www.texture;
		}


	
	}



}
