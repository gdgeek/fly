using UnityEngine;
using System.Collections;
namespace VoxelTrek{
	public class Health : MonoBehaviour {
		public Heart[] _heart = null;
		public float value {

			set{
				int all = 0;
				for(int i =0; i<_heart.Length; ++i){
					all += _heart[i].all;
				}
				int hp = Mathf.FloorToInt((float)(all) * value);
				foreach(Heart h in _heart){

					h.value = hp;
					hp -= h.all;
				}
			}

			
		}

		
	}
}