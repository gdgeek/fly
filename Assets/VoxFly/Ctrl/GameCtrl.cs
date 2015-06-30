using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	public class GameCtrl : MonoBehaviour {
		private FSM fsm_ = new FSM();
		public LogoCtrl _logoCtrl = null;
		public FlyCtrl _playCtrl = null;
		public OverCtrl _overCtrl = null;
	

		public Logo _logo = null;
		public void fsmPost (string msg)
		{
			fsm_.post(msg);
		}

		private State logoState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			state.onStart += delegate() {
				_logoCtrl.gameObject.SetActive(true);
				TempBGM.GetInstance().start();
			};
			state.onOver += delegate() {
				_logoCtrl.gameObject.SetActive(false);
				TempBGM.GetInstance().play();
			};
			return state;
		}
		private State playState(){
			StateWithEventMap state = new StateWithEventMap ();
			state.onStart += delegate() {
				_playCtrl.gameObject.SetActive(true);
						};
			state.onOver += delegate() {
				_playCtrl.gameObject.SetActive(false);
				
//				BGM.GetInstance().stop();
						};
			return state;
		}

		private State overState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			state.onStart += delegate() {
				_overCtrl.gameObject.SetActive(true);
			};
			state.onOver += delegate() {
				_overCtrl.gameObject.SetActive(false);
			};
			return state;
		}

		public void Start(){
		
			fsm_.addState ("logo", _logoCtrl.createState(this.fsm_, "logo", "play"), logoState ());
			fsm_.addState ("play", _playCtrl.createState (this.fsm_, "play", "over"), playState());
			fsm_.addState ("over", _overCtrl.createState(this.fsm_, "over", "logo"), overState());
//xxxxx			Debug.Log ("!!!!!!");
			fsm_.init ("logo");

		}

	}
}
