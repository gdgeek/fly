using UnityEngine;
using System.Collections;


namespace GDGeek.WebVox{

	public class WebToCtrl : MonoBehaviour {
		public Ctrl _ctrl = null;
		public void selectMesh(string mesh){
			_ctrl.selectMesh(mesh);
		}

		public void pushJson(string json){
			_ctrl.pushJson(json);
		}
	}
}
