using UnityEngine;
using System.Collections;
namespace GDGeek.Gal{
	public class State : MonoBehaviour {

		private StateWithEventMap state_ = null;
		public string _nextState = "";

		public GDGeek.Gal.TaskFactory _taskFactory = null;
		private GDGeek.StateWithEventMap create(FSM fsm){
			StateWithEventMap state = null;
			if (_taskFactory != null) {
				state = TaskState.Create (delegate {
					return _taskFactory.create ();
				}, fsm, _nextState);
			} else {
				state = new GDGeek.StateWithEventMap ();	
			}

			return state;
		}
		public GDGeek.State getState(FSM fsm) {

			if(state_ == null){
				state_ = create(fsm);
			}
			return state_;

		}
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
