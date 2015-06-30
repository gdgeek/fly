using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class OverCtrl : MonoBehaviour{


		public GameModel _model = null;
		public void fsmPost (string down)
		{
			this.fsm_.post (down);
			Debug.Log ("event:" + down);
		}
		private string prefix_;
		public VoxelText _vText = null;
		public Health _health = null;
		public UIHudText _hudText = null;
		public Logo _logo = null;
		private FSM fsm_;

		public State showState(){


			StateWithEventMap state = TaskState.Create (delegate() {
				TaskList tl = new TaskList();
				if(_model._score.score > _model._score.best){
					_model._score.best = _model._score.score ;
				}
				tl.push (_hudText._score.show(_model._score.score, _model._score.best));

				return tl;//;
			}, fsm_, prefix_ + ".input"); 

			state.onStart += delegate {
				_hudText.open();
						};

			return state;
		}

		public State inputState(){
			
			TaskCircle tapit = null;
			StateWithEventMap state = TaskState.Create (delegate() {

				tapit = _hudText._tapit.open();

				return tapit;//;
			}, fsm_, prefix_ + ".over"); 
			state.addAction ("down", delegate(FSMEvent evt) {
				tapit.forceQuit();
			});

			state.onOver += delegate {
				//_hudText._tapit.clear();
				tapit = null;
			};
			return state;
		}

		public State overState(string next){
				StateWithEventMap swe = TaskState.Create (delegate() {
					TaskList tl = new TaskList();
					tl.push (_hudText._score.clearTask());
					Task task = _logo.resetTask();
					tl.push (task);
					return tl;
				}, fsm_, next); 
				swe.onOver += delegate {
					_health.value = 1.0f;
					GameManager.GetInstance().road.reset();
					_vText.text = "";
				};
				return swe;
		}

		public string createState (FSM fsm, string father, string next)
		{
			fsm_ = fsm;
			prefix_ = father;
			fsm_.addState (prefix_ + ".show", showState(), father);
			fsm_.addState (prefix_ + ".input", inputState(), father);
			fsm_.addState (prefix_ + ".over", overState (next), father);

			return father + ".show";
		}
	}

}
