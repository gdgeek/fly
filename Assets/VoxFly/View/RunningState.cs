using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	class RunningState:State 
	{
	
		public  override string postEvent(FSMEvent evt){

			Debug.Log("###" + evt.msg);
			return "";
		}
	}

}

