using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class Boss : MonoBehaviour {
		public void fsmPost (string msg)
		{
			this.fsm_.post (msg);
		}

		public Dirver dirver {
			get{
				return this._dirver;
			}
		
		}

		private  FSM fsm_ = new FSM ();
		public Dirver _dirver = null; 
		private State sleepState ()
		{
			StateWithEventMap sleep = new StateWithEventMap ();
		
			sleep.addAction("weakup", "move");
			return sleep;
		}

		private State weakupState ()
		{
			
			StateWithEventMap sleep = new StateWithEventMap ();
			/*sleep.onStart += delegate() {
				this.dirver.fire = true;
			};
			sleep.onOver += delegate() {
				this.dirver.fire = false;
			};*/
			sleep.addAction ("sleep", "sleep");
			return sleep;
		}

		private State moveState (){
			StateWithEventMap state = TaskState.Create (delegate() {
				return _dirver.moveTask(new Vector3(Random.Range(50f, -10f), Random.Range(80f, -10f), 0));
			}, fsm_, "fire");
		
			return state;
		}

		private State fireState ()
		{
			StateWithEventMap state =  TaskState.Create (delegate() {
				return _dirver.moveTask(new Vector3(Random.Range(10f, -50f), Random.Range(80f, -10f), 0));
			}, fsm_, "move");

			return state;
		}
	
		public void initialize(){
			fsm_.addState ("sleep", sleepState ());
			fsm_.addState ("weakup", weakupState ());
			fsm_.addState ("move", moveState (), "weakup");
			fsm_.addState ("fire", fireState (), "weakup");
			fsm_.init ("sleep");
		}
	}
}