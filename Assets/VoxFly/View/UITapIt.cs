using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class UITapIt : MonoBehaviour {
		public VoxelText _text;
		public TaskCircle open(){
		
			TaskCircle tc = new TaskCircle ();
			tc.push (_text.setTextTask ("Tap>  "));
			tc.push (_text.setTextTask ("Tap > "));
			tc.push (_text.setTextTask ("Tap  >"));
			TaskManager.PushBack (tc, delegate {
				_text.setText ("");
						});
			return tc;
		}
	
		

	}
}
