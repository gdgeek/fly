using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	public class LogoCtrl : MonoBehaviour {

		public UIHudText _hudText = null;
		public Logo _logo = null;
		private FSM fsm_ = null;
		private string prefix_ = null;

		public UIScore _score;
	
		public State nomalState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			state.addAction ("down", delegate(FSMEvent evt) {	
				return prefix_+".down";
						});
			state.onStart += delegate() {
				_score.clear();
				_hudText.clear();
						};
			return state;

		}

		public State downState ()
		{
			StateWithEventMap state = TaskState.Create(delegate() {
				Task task = _logo.downTask();
				TempSound.GetInstance().start ();
//				AkSoundEngine.PostEvent("Start", this.gameObject);
				return task;
			}, fsm_, prefix_+".up");

			return state;
			
		}
		
		public State upState ()
		{
			StateWithEventMap state = TaskState.Create(delegate() {

				return _logo.upTask();
			}, fsm_, prefix_+".next");
			
			return state;
		}
		public State nextState (string next)
		{

			StateWithEventMap state = TaskState.Create(delegate() {
				return _logo.nextTask();
			}, fsm_, next);
			
			return state;


		}


		public string createState (FSM fsm, string father, string next)
		{
			prefix_ = father;
			fsm_ = fsm;

			fsm.addState (prefix_+".input", nomalState (), father);
			fsm.addState (prefix_+".down", downState (), father);
			fsm.addState (prefix_+".up", upState (), father);
			fsm.addState (prefix_+".next", nextState (next), father);
			return prefix_+".input";
		}

		public void Start(){
		}


	}
}
