using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	class Star : MonoBehaviour{
		private float flicker_ = 0;
		private float time_ = 0;
		public Color _from;
		public Color _to;
		public void Awake(){
			flicker_ = Random.Range(0.1f, 3f);		
		}
		public void setup(Transform tran, Camera cam, Plane sky){
			this.transform.SetParent (tran);
			float x = Random.Range (0.0f, Screen.width);
			float y = Random.Range (0.0f, Screen.height);
			Ray ray = cam.ScreenPointToRay (new Vector3 (x, y, 0));
			float dist = 0; 
			sky.Raycast (ray, out dist);
			Vector3 p = ray.GetPoint (dist);
			this.transform.position = p;  
			this.gameObject.SetActive (true);
		}
		public void Update(){
			time_ += Time.deltaTime;
			if (time_ > flicker_) {
				flicker_ = Random.Range(1f, 3f);
				time_ = 0;
				Tween tween = TweenStar.Begin(this.GetComponent<MeshRenderer>(), Random.Range(0.5f, 1f), _to);
				tween.onFinished += delegate(Tween t) {
					MeshRenderer renderer = this.GetComponent<MeshRenderer>();
					renderer.material.color = _from;
				};
				//MeshRenderer renderer = this.GetComponent<MeshRenderer>();
				//renderer.material.color = Color.black;
			}
		}
	}

}
