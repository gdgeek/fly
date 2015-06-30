using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class Play : MonoBehaviour 
	{

		private Plane sky_ = new Plane(Vector3.forward, 0);
		//private VoxelMesh flyMesh_ = null;
		public VoxelMesh _flyMesh = null;
		public VoxelManager _manager = null;
		public Camera _camera = null;
		public VoxelPool _buttlePool = null;

		public void show ()
		{
			this.gameObject.SetActive (true);
		}

		public Vector3 inCamera (Vector3 position)
		{
			Vector3 sp = _camera.WorldToScreenPoint (position);
			if (sp.x > Screen.width) {
				sp.x = Screen.width;			
			} else if (sp.x < 0) {
				sp.x = 0;			
			}
			if (sp.y > Screen.height) {
				sp.y = Screen.height;			
			} else if (sp.y < 0) {
				sp.y = 0;			
			}
			Vector3 re = _camera.ScreenToWorldPoint (sp);
			return re;
		}

		public Vector3 touch (Vector2 position){ 
			Ray ray = _camera.ScreenPointToRay (position);
			float dist = 0; 
			sky_.Raycast (ray, out dist);
			return ray.GetPoint (dist);    
		}
		public Fly createFly(string name){

			GameObject obj = GameObject.Instantiate (_flyMesh.gameObject);
			obj.gameObject.transform.SetParent (this.transform);
			obj.name = name;
			obj.SetActive (true);
			return obj.GetComponent<Fly> ();

		}
		
		public Buttle createButtle()
		{
			VoxelPoolObject obj = _buttlePool.create ();

			Buttle buttle = obj.GetComponent<Buttle> ();
			return buttle;
		}

	}

}
