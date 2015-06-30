using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class Begin : MonoBehaviour 
	{
		public Rigidbody _logo = null;
		public void hide ()
		{
			
			this.gameObject.SetActive(false);
		}
		public void reset(){
			
			_logo.AddForce (new Vector3 (0, -8000, 0));
		}
		public void show ()
		{
			this.gameObject.SetActive(true);
			reset();

		}
	}

}
