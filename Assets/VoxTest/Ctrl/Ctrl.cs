using UnityEngine;
using System.Collections;
using GDGeek;


namespace Test{
	public class Ctrl : MonoBehaviour {
		private FSM fsm_ = new FSM();
		public View _view = null;
		private State loadState ()
		{
			StateWithEventMap ts = TaskState.Create (delegate() {
				return new Task();
						}, fsm_, "begin");
			return ts;
		}
		
		private State beginState ()
		{
			StateWithEventMap ts = TaskState.Create (delegate() {
				return _view._transformer.showTask();
			}, fsm_, "begin");
			return ts;
		}
		
		private State s2State ()
		{
			return new State ();
		}

		
		private State s10State ()
		{
			return new State ();
		}

		
		private State s12State ()
		{
			return new State ();
		}

		private State s0State ()
		{
			return new State ();
		}


		
		private State s2ToS1State ()
		{
			return new State ();
		}

		
		private State s1ToS0State ()
		{
			return new State ();
		}
		
		private State s0ToS1State ()
		{
			return new State ();
		}
		
		private State s1ToS2State ()
		{
			return new State ();
		}
		
		private State gameState ()
		{
			return new State ();
		}
		// Use this for initialization
		void Start () {
			
			fsm_.addState ("game", gameState ());
			fsm_.addState ("load", loadState (), "game");
			fsm_.addState ("begin", beginState (), "game");
			fsm_.addState ("state2", s2State (), "game");
			fsm_.addState ("state2_to_state1", s2ToS1State (), "game");
			fsm_.addState ("state1_0", s10State (), "game");
			fsm_.addState ("state1_to_state0", s1ToS0State (), "game");
			fsm_.addState ("state0", s0State (), "game");
			fsm_.addState ("state0_to_state1", s0ToS1State (), "game");
			fsm_.addState ("state1_2", s12State (), "game");
			fsm_.addState ("state1_to_state2", s1ToS2State (), "game");
			fsm_.init ("load");
		}

	}
}