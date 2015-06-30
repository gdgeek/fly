using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GDGeek;
namespace VoxelTrek{
	public class Dirver : MonoBehaviour {

		public Trigger _trigger = null;
		public RoadManager _road = null;
		private Filter filter_ = new Filter();
		private Vector3 position_;
		private Vector3 old_;
		public Vector3 position {
			get{
				return position_;
			}
			set{
				position_ = value;
			}

		}


		
		public Health _health = null;
		//public VoxelPool _buttlePool;
		public void reset ()
		{
			this._engine.health = this._engine.maxHealth;
			_health.value = _engine.health / _engine.maxHealth;

		}

		public Task dieTask ()
		{
			
			//VoxelBlow b = new VoxelBlow ();
			//b.mesh = this.fly_.gameObject.GetComponent<VoxelMesh>();
			Task die = new TaskWait(0.3f);
			TaskManager.PushFront (die, delegate {
				this.fly_._boom.emission();
				this.fly_._mesh._mesh.gameObject.SetActive(false);
				this.fly_.jet = false;
			});
			return die;
		}

		public bool isDie {
			get{
				return (_engine.health <= 0f);
			}

		}


		public Tween.Method _method = Tween.Method.Linear;
		public Engine _engine = null;
		public Task moveTask (Vector3 position)
		{
			TweenTask task = new TweenTask (delegate {
				Tween tween = TweenLocalPosition.Begin(this.fly_.gameObject, Vector3.Distance(position,this.fly_.gameObject.transform.localPosition)*0.01f, position);
				tween.method = _method;
				return tween;
			});
			return task;
		}
	
		private Fly fly_ = null;
	
		public void Awake(){
			position_ = Vector3.zero;
		}
		public Fly fly {
			set{

				fly_ = value;
				this.fly_.transform.position = position_;
				old_ = position_;

			}
			get{
				return fly_;
			}
		}


		public void move (Vector3 pos)
		{
			this.position_ = pos;
			if (this.fly_ != null && this.fly_.transform.position != this.position_) {
				this.fly_.transform.position = position_;	
			}
		}
		public void doControl(){
			_trigger.shooting (this.fly_.gameObject.GetComponentInChildren<Gun>());
//			this.fire = true;

			Task t = fly_.downTask ();
			_road.run ();// = 4.0f;
		
			TaskManager.Run (t);
		}
		public void doFree(){
			_trigger.close ();
//			this.fire = false;
			TaskManager.Run (fly_.upTask ());
			_road.walk ();// = 1.0f;
		}
		public void hurt ()
		{
			TempSound.GetInstance ().hurt ();
//			AkSoundEngine.PostEvent("Hurt",this.fly_.gameObject);//
			_engine.health -= 1f;
			if (_engine.health < 0f) {
				_engine.health = 0f;			
			}
			_health.value = _engine.health / _engine.maxHealth;
		}

	
		public void doDie (){
			this._trigger.close ();
		}

		public void Update(){
			if (isDie) {
				return;			
			}

			
			this.fly_._shifting (filter_.interval((this.old_.x - this.position_.x) * Time.deltaTime) * 500);
			if (this.old_ != this.position_) {
				this.old_ = position_;	
			}

		
		}


	}
}