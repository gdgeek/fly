using UnityEngine;
using System.Collections;

namespace VoxelTrek{
	public class Input : MonoBehaviour {
		public delegate void DoAction();
		
		public event DoAction doDown;
		public event DoAction doUp;
		private float _touchTime = 0.0f;
		private bool isDown_ = false;

		void Awake(){
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

			_touchTime = 0;
			isDown_ = true;
			Debug.Log ("touch start");
			if (doDown != null) {
				doDown ();	
			}
		}
		
		public void onTouchDown(Gesture gesture){
			_touchTime = 0;
		}
		public void onTouchUp(Gesture gesture){
			isDown_ = false;
			Debug.Log ("touch over");
			if (doUp != null) {
				doUp ();
			}
			_touchTime = 0.0f;
		}
		public void Update(){
			
			if (isDown_) {
				
				_touchTime += Time.deltaTime;	
				if (_touchTime > 0.1f) {
					Debug.Log ("touch over!!!");
					if (doUp != null) {
						doUp ();
					}
					isDown_ = false;
					_touchTime = 0.0f;
				}
			}

		}

	}
}