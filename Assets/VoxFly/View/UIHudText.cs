using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	public class UIHudText : MonoBehaviour {


		public UIScore _score;
		//public UISpeedup _speedup;
		public UITapIt _tapit;
		public void clear ()
		{
			_score.clear ();
//			_tapit.clear ();
		}
		public void open ()
		{
			//this.gameObject.SetActive (true);
		}

	}

}