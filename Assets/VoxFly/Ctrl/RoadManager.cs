using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	public class RoadManager : MonoBehaviour {

		private Filter filter_ = new Filter();
		private float degree_ = 1.0f;
		private float length_ = 0.0f;
		public float degree{
			set{
				degree_ = value;
			}
			get{
				return degree_;
			}
		}


		private delegate void MoveFun(float deltaTime);
		private MoveFun moveFun;

		
		public delegate void MoveCallback(float length);
		public float _speed = 1.0f;

		public void reset ()
		{
			length_ = 0.0f;
		}

		public void stop ()
		{
			moveFun = this.doStop;
		}

		public void walk ()
		{
			moveFun = this.doWalk;
		}
		
		public void run ()
		{
			moveFun = this.doRun;
		}
		
		public event MoveCallback onMove;
		
		void Awake(){
			stop();
		}
		public float length{
			get{
				return 	length_;
			}
		}
	
		private void doStop(float deltaTime){
			doMove (0.0f);
		}

		private void doMove(float length){
			length_ += length;
			if (onMove != null) {
				onMove (length_);
			}
		}
		private void doWalk(float deltaTime){
			doMove (deltaTime * 0.5f  * _speed);
		}
		
		private void doRun(float deltaTime){
			doMove (deltaTime * _speed * 4.0f * degree_);
		}


		void Update () {
			moveFun (filter_.interval(Time.deltaTime));
		}
	}
}