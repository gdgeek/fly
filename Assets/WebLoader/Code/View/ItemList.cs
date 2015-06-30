using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek.WebVox{
	public class ItemList : MonoBehaviour {
		public Item _phototype;
		public RectTransform _this = null;
		//private 
		private List<Item> list_ = new List<Item> ();
		public void addItem(string title, string iconUrl, string mesh, string message, int like, string postman){
			RectTransform rrt = _phototype._rt;
			Item item = (Item)GameObject.Instantiate (_phototype);
			item.gameObject.transform.parent = this.transform;
			item.gameObject.SetActive (true);
			item.transform.localPosition = Vector3.zero;
			item.transform.localScale = Vector3.one;
			RectTransform rt = item._rt;
			rt.localPosition = rrt.localPosition +  new Vector3(0, list_.Count * -150, 0);
			rt.sizeDelta = rrt.sizeDelta;
			list_.Add (item);
			item.setup (title, mesh, iconUrl);
			refresh ();
		}
		public void refresh(){
			_this.sizeDelta = new Vector2 (_this.sizeDelta.x, list_.Count * 150);

		}
		

	}
}
