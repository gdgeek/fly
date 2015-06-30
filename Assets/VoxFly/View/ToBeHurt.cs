using UnityEngine;
using System.Collections;
namespace VoxelTrek{
	public class ToBeHurt : MonoBehaviour {
		public delegate void ToBeHurtCB (Damage damage);
		private ToBeHurtCB callback_ = null;
		public ToBeHurtCB callback{
			set{
				callback_ = value;
			}
		}
		public void toBeHurtCB(Damage damage){
			if (callback_ != null) {
				this.callback_ (damage);
			}
		}

		
	}

}