using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelPoolObject : MonoBehaviour {
		public delegate void DoAction(VoxelPoolObject obj);
		
		public event DoAction doEnable;
		public event DoAction doDisable;


		public void OnDisable(){
//			Debug.Log ("on disable");
			if (doDisable != null) {
				doDisable(this);			
			}
		}

		public void OnEnable(){

			
		//	Debug.Log ("on enabled");
			if (doEnable != null) {
				doEnable(this);			
			}
		}
	}
}