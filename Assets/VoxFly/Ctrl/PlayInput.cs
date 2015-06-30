using UnityEngine;
using System.Collections;
namespace VoxelTrek{
	public class PlayInput : MonoBehaviour {
		public FlyCtrl _ctrl = null;
		public Play _play = null;

		private Vector3 _touchPosition = Vector3.zero;
		//private bool isDown_ = false;
		//private float _touchTime = 0.0f;
		void Awake(){
			EasyTouch.On_TouchStart += onTouchStart;
			EasyTouch.On_TouchDown += onTouchDown;
			EasyTouch.On_TouchUp += onTouchUp;

		}
		void OnEnable () {
			EasyTouch.On_TouchStart += onTouchStart;
			EasyTouch.On_TouchDown += onTouchDown;
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
			EasyTouch.On_TouchDown -= onTouchDown;
			EasyTouch.On_TouchUp -= onTouchUp;
		}
		public void onTouchStart(Gesture gesture){
			_ctrl.doControl ();
			_touchPosition = _play.touch (gesture.position);// + _ctrl._dirver.fly.gameObject.transform.position;
			//_touchTime = 0;
			//isDown_ = true;
		}
		
		public void onTouchDown(Gesture gesture){
			Vector3 position = _play.touch (gesture.position);
			if (_touchPosition != position) {

				_ctrl.doMove(position - _touchPosition );
				_touchPosition = position;
			}
		}
		public void Update(){
			/*if (_touchTime > 0.1f) {
				_ctrl.doFree ();	
				isDown_ = false;
			}
			if (isDown_) {
				_touchTime += Time.deltaTime;
			}*/
		}
		public void onTouchUp(Gesture gesture){
			_ctrl.doFree ();	
		}
	}
}