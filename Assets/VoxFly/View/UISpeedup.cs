using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class UISpeedup : MonoBehaviour {
		
		public VoxelText _speed;
		public VoxelText _up;
		public Task show(){
			TaskList tl = new TaskList ();
			tl.push(_speed.setTextTask("SPEED"));
			tl.push(_up.setTextTask("UP!"));
			tl.push (new TaskWait (0.5f));
			tl.push (_speed.setTextTask (""));
			tl.push (_up.setTextTask (""));
			return tl;
		}
	}
}
