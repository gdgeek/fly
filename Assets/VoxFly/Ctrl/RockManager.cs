using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class RockManager : MonoBehaviour {


		public Camera _camera = null;
		public VoxelPool _pool = null;
		private Plane sky_ = new Plane(Vector3.forward, 0);

		public void _doAction (float position, int number)
		{
			VoxelPoolObject obj = _pool.create ();
			Rock rock = obj.GetComponent<Rock> ();
			float where = 0.1f * Screen.width * (1.0f - position) + Screen.width*0.9f * position;
			Ray ray1 = _camera.ScreenPointToRay (new Vector3 (where, Screen.height * 1.1f, 0));
		
			float dist1 = 0; 
			sky_.Raycast (ray1, out dist1);
			
			
			Ray ray2 = _camera.ScreenPointToRay (new Vector3 (where, -Screen.height *0.1f, 0));
			float dist2 = 0; 
			sky_.Raycast (ray2, out dist2);
			
			
			rock.setup (ray1.GetPoint (dist1), ray2.GetPoint (dist2), number -1);
		}

		public void clear(){
			Rock[] rock = this.gameObject.GetComponentsInChildren<Rock>();
			if (rock != null) {

				for(int i = 0; i< rock.Length; i++){
					rock[i].gameObject.SetActive(false);
				}			
			}
		}



	}
}
