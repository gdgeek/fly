using UnityEngine;
using System.Collections;
using System.IO;
using GDGeek;
using VoxelTrek.LevelMode;

namespace VoxelTrek{
	public class FlyCtrl : MonoBehaviour {


		public GameModel _model;
		public Logo _logo = null;
		public LevelManager _level;
		public void doMove(Vector3 position){
			FSMEvent evt = new FSMEvent ();
			evt.obj = _view._play.inCamera (position + this._dirver.position);
			evt.msg = "move";
			this.fsm_.postEvent (evt);
		}
		public void doFree ()
		{
			this.fsm_.post("free");
		}

		public void doControl ()
		{
			this.fsm_.post("control");
		}


		public VoxelPool _buttlePool;


		public View _view = null;
		public Dirver _dirver = null;
		private FSM fsm_ = null;
		private string prefix_ = null;

		private void doHurt (Damage damage)
		{
			this.fsm_.post("hurt");
		}

		private Task loadTask(){
			TaskList tl = new TaskList ();
			Task load = new Task ();
			
			this._view._play.show();
			Fly fly = _view._play.createFly("hero");
			_dirver.position = new Vector3(0, -100, 0);
			_dirver.fly = fly;
			fly.doHurt += doHurt;
			_dirver.reset();

			load.init = delegate {
				TempSound.GetInstance ().comeIn ();
//				AkSoundEngine.PostEvent ("ComeIn", this.gameObject);
						};
			load.update = delegate(float d) {
				_dirver.move(_dirver.position + new Vector3(0, d*120, 0));
				};
			load.isOver = delegate() {
					if(_dirver.position.y > -60){
						return true;
					}
					return false;
			};
			load.shutdown = delegate {
				_dirver.move(new Vector3(0, -60, 0));
			};
			tl.push (_logo.openTask());
			tl.push (_dirver.fly.downTask ());
			tl.push (load);
			tl.push (_dirver.fly.upTask ());
			TaskManager.PushBack (tl, delegate {
				GameManager.GetInstance().road.walk();
				_level.building ();
						});
			return tl;
		}
	
		private State loadState(){
			StateWithEventMap state = TaskState.Create (delegate {
				return new TaskPack(loadTask);
			},this.fsm_, prefix_ + ".input");

			return state;
		}
		private State playState(){
			StateWithEventMap play = new StateWithEventMap ();
		
			play.addAction ("move", delegate(FSMEvent evt) {
				this._dirver.move((Vector3)(evt.obj));
			});
		
			
			play.addAction ("control", delegate (FSMEvent evt){
				this._level.clickDown();
				this._dirver.doControl ();
			});
			play.addAction ("free", delegate (FSMEvent evt){
				this._dirver.doFree ();
			});
			return play;
		}

		private State inputState(){
			StateWithEventMap play = new StateWithEventMap ();
			play.addAction ("hurt", delegate(FSMEvent evt) {
				TempSound.GetInstance().hurt ();
				this._dirver.hurt();
				if(this._dirver.isDie){
					return prefix_ + ".die";
				}

	
				return "";
			});
			play.onOver += delegate {
				this._dirver.doDie();
			};
			return play;
		}


		
		
		private State dieState ()
		{
			StateWithEventMap die = TaskState.Create (delegate() {
				TempSound.GetInstance().boom ();
				Task task = this._dirver.dieTask();
				TaskManager.PushBack(task, delegate {
					GameObject.DestroyImmediate(_dirver.fly.gameObject);
				});
				return task;
			},this.fsm_, prefix_ + ".unload");
			return die;
		}
		

		
		
		private State unloadState (string next)
		{

			

			StateWithEventMap unload = TaskState.Create (delegate() {
				Task task = _logo.closeTask();
				TaskManager.PushFront(task, delegate {
					GameManager.GetInstance().road.stop ();
				});
				TaskManager.PushBack(task, delegate {
					_level.unbuild();
				});
				return task;
			},this.fsm_, next);
			unload.onOver += delegate {
				_model._score.score = Mathf.FloorToInt(GameManager.GetInstance().road.length);
			};
			return unload;
		}
		

	

		public string createState (FSM fsm, string father, string next)
		{
			fsm_ = fsm;
			prefix_ = father;
			fsm_.addState (prefix_ + ".game", new State(), father);
			fsm_.addState (prefix_ + ".load", loadState (), father);
			fsm_.addState (prefix_ + ".play", playState (), father);
			fsm_.addState (prefix_ + ".input", inputState (), prefix_ + ".play");
			fsm_.addState (prefix_ + ".die", dieState (), father);
			fsm_.addState (prefix_ + ".unload", unloadState (next), father);
			return father + ".load";
		}
	
		/*
		void Awake(){
			
			_buttlePool.doEnable += delegate(VoxelPoolObject obj) {
				Buttle buttle = obj.GetComponent<Buttle>();
				buttle.blow = delegate(Fly f){
					FSMEvent evt = new FSMEvent();
					evt.msg = "blow";
					evt.obj = f;
					this.fsm_.postEvent(evt);
				};
			};
		}

	*/

	


	}
}
