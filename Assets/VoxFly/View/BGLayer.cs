using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class BGLayer : MonoBehaviour 
	{
		public int _index = 0;
		public Camera _camera = null;
		public float _z = 0;
		private Plane sky_;
		public VoxelPool _pool;
		private float y_ = 0.0f;
		public int _starCount = 10;

		public void roll (float s){
			float r = s * 100.0f;
			if (y_ != 0) {
				this.gameObject.transform.position = new Vector3 (0, (y_ - Mathf.Repeat (r - y_ * _index, 2.0f * y_)), 0);
			}
		}
		private void Awake(){
//			Debug.Log ("!!!!!????");
			sky_ = new Plane(Vector3.forward, _z);
			Ray ray1 = _camera.ScreenPointToRay (new Vector3 (0, 0, 0));
			Ray ray2 = _camera.ScreenPointToRay (new Vector3 (0, Screen.height, 0));	
			float dist1 = 0; 
			sky_.Raycast (ray1, out dist1);
			float dist2 = 0;
			sky_.Raycast (ray2, out dist2);
			y_ = (ray2.GetPoint (dist2) - ray1.GetPoint (dist1)).y; 

		}

		private Star createStar(){
			VoxelPoolObject obj = _pool.create ();
			Star star = obj.GetComponent<Star> ();
			star.setup (this.transform, _camera, sky_);
		
			return star;
		}

		
		void Start () {
			for (int i = 0; i < _starCount; ++i) {
				createStar ();		
			}
			
		}
	}

}
