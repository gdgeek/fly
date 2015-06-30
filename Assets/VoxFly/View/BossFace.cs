using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class BossFace : MonoBehaviour {


		//public 

		public VoxelEmitter _emitter = null;
		public Trigger _trigger = null;
		public VoxelTalkManager _talk;
		private FSM fsm_ = new FSM(true);
		public Gun _gun = null;
		public Camera _camera = null;
		private Plane plane_ = new Plane(Vector3.forward, 0);
		private int health_ = 3;
		public ToBeHurt _toBeHurt = null;
		private bool alive_ = true;
		private Vector3 position_;
		private Quaternion quaternion_;

		public bool alive{

			get{
				return alive_;
			}
		}

		public void toBeHurtCB(Damage damage){
			if (health_ > 0) {
				this.fsm_.post ("hurt");
				TempSound.GetInstance().hurt();
			} else {
				
				this.fsm_.post ("die");
				TempSound.GetInstance().boom();
			}
		}


		public void Awake(){
			position_ = _talk._head.gameObject.transform.localPosition;
			quaternion_ = _talk._head.gameObject.transform.localRotation;

			_toBeHurt = _talk._head._face.gameObject.GetComponent<ToBeHurt>();
			if (_toBeHurt == null) {
				_toBeHurt = _talk._head._face.gameObject.AddComponent<ToBeHurt>();
			}
			_toBeHurt.callback = this.toBeHurtCB;
		}
		private State sleepState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
		
			
		
			state.addAction ("running", "loading");
			return state;
		}


		private State runningState ()
		{
			//RunningState rs = new RunningState();
			StateWithEventMap state = new StateWithEventMap ();
			state.onStart += delegate {
			//	Debug.Log ("s running");
				_trigger.shooting(_gun);
						};
			state.onOver += delegate {
				//Debug.Log ("e running");
				_trigger.close();

						};

			state.addAction ("hurt", delegate(FSMEvent evt) {
				Debug.Log ("!hurt");
				this.health_--;
			});

			state.addAction("die", "die");
			state.addAction("clear", "clear");
			return state;
		}
		public void running(){
			this.fsm_.post("running");
		}
		public void clear ()
		{
			this.fsm_.post ("clear");
		}
		private State showState ()
		{
			StateWithEventMap state = TaskState.Create(delegate() {
				//TaskList tl = new TaskList();
				health_ = 5;
				alive_ = true;
				TaskSet ts = new TaskSet();
				TweenTask tt = new TweenTask(delegate {
					return TweenLocalPosition.Begin(_talk._head.gameObject, 0.5f, new Vector3(0,40,0));

				});

				TweenTask tt2 = new TweenTask(delegate {
					return TweenRotation.Begin(_talk._head.gameObject, 0.5f, Quaternion.Euler(new Vector3(-45,0,0)));
					
				});
				ts.push (tt);
				ts.push (tt2);
				return ts;
			}, fsm_, "move");
			return state;

		}

		private State loadingState ()
		{
			StateWithEventMap state = TaskState.Create(delegate() {
				
				return _talk._head.openTask();
				}, fsm_, "show");
			return state;
		}
		private Vector3 nextPosition(float x, float y){

			Ray ray1 = _camera.ViewportPointToRay (new Vector3 (x, y, 0));
			
			float dist1 = 0; 
			plane_.Raycast (ray1, out dist1);
			return ray1.GetPoint (dist1);
		}
		private State  moveState ()
		{
			Tween tween = null;
			StateWithEventMap state = TaskState.Create(delegate() {
				TaskSet ts = new TaskSet();
				TweenTask tt = new TweenTask(delegate {
					tween = TweenWorldPosition.Begin(_talk._head.gameObject, 0.5f, nextPosition(Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f)));
					return tween;
				});

				ts.push (tt);
				return ts;
			}, fsm_, "move");
			state.onOver += delegate {
				
				tween.enabled = false;
			};
			return state;

		}

		private State dieState (){
			StateWithEventMap state = TaskState.Create(delegate() {
				

				Task task = _talk.closeTask();
				TaskManager.PushFront(task, delegate {
					_emitter.emission();

				});
				TaskManager.PushBack(task, delegate {
					alive_ = false;
					
					_talk._head.gameObject.transform.localPosition = position_;
					_talk._head.gameObject.transform.localRotation = quaternion_;

				});
				return task;
			}, fsm_, "sleep");

			return state;
		}

		private State clearState (){
			StateWithEventMap state = TaskState.Create(delegate() {
				Task task = new Task();

				TaskManager.PushBack(task, delegate {
					_talk.close();
					alive_ = false;
					Debug.LogWarning("position" + position_);
					Debug.LogWarning("quaternion_" + quaternion_);
					_talk._head.gameObject.transform.localPosition = position_;
					_talk._head.gameObject.transform.localRotation = quaternion_;
				});
				return task;
			}, fsm_, "sleep");
			
			return state;
		}


		public void Start(){

			fsm_.addState("sleep", sleepState());
			fsm_.addState("running", runningState());

			fsm_.addState("show", showState(), "running");
			fsm_.addState("move", moveState(), "running");
			fsm_.addState("loading", loadingState(), "running");
			fsm_.addState("clear", clearState());
			fsm_.addState ("die", dieState ());/**/
			fsm_.init ("sleep");

		}

	}
}

