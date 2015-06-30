using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class Length : MonoBehaviour {
		public VoxelText _vText = null;
		public void Start(){
			_vText.text = "1";
		}

	}
}