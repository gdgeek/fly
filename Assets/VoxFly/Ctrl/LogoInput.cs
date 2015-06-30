using UnityEngine;
using System.Collections;

namespace VoxelTrek{
	public class LogoInput : MonoBehaviour {
		public GameCtrl _game;

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
			this._game.fsmPost("down");
		}

		public void onTouchUp(Gesture gesture){

			this._game.fsmPost("up");
		}
	}
}