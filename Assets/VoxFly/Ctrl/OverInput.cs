using UnityEngine;
using System.Collections;

namespace VoxelTrek{
	public class OverInput : MonoBehaviour {
		public OverCtrl _over;
		
		void OnEnable () {
			EasyTouch.On_TouchStart += onTouchStart;
			EasyTouch.On_TouchUp += onTouchUp;
		}
		void OnDisable(){
			UnsubscribeEvent();
		}
		
		void OnDestroy(){
			UnsubscribeEvent();
		}
		
		void UnsubscribeEvent(){
			EasyTouch.On_TouchStart -= onTouchStart;
			EasyTouch.On_TouchUp -= onTouchUp;
		}
		
		public void onTouchStart(Gesture gesture){
			this._over.fsmPost("down");
		}
		
		public void onTouchUp(Gesture gesture){
			
			this._over.fsmPost("up");
		}
	}
}