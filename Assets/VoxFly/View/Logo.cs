using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class Logo : MonoBehaviour {

	
		public VoxelMesh _title1;
		public VoxelMesh _title2;
		public VoxelMesh[] _curtain;
		public VoxelEmitter _enitter;
		
		public float _downTime;
		public float _upTime;
		public Tween.Method _downMethod;
		public Tween.Method _resetMethod;
		public Tween.Method _upMethod;
		public Tween.Method _curtainMethod;
		void Start () {
		
		}

		public Task upTask ()
		{
			TweenTask tt = new TweenTask (delegate() {
				Tween tween =  TweenLocalPosition.Begin(this.gameObject, _upTime, new Vector3(0, 0, 30));
				tween.method = _upMethod;
				return tween;
			});
			TaskManager.PushFront (tt, delegate() {
				_title2.gameObject.SetActive(true);
				_title1.gameObject.SetActive(false);
				_enitter.emission();
						});
			return tt;
		}
		public Task resetTask(){
			TweenTask tt = new TweenTask (delegate() {
				this.gameObject.transform.localPosition = new Vector3(3.5f, 0, 0);
				Tween tween =  TweenLocalPosition.Begin(this.gameObject, 1.0f, new Vector3(0, 0, 30));
				tween.method = _resetMethod;
				return tween;
			});
			TaskManager.PushFront (tt, delegate() {
				_title2.gameObject.SetActive(false);
				_title1.gameObject.SetActive(true);
				//_enitter.emission();
			});
			return tt;		
		}

		public Task downTask ()
		{
			TweenTask tt = new TweenTask (delegate() {
				Tween tween =  TweenLocalPosition.Begin(this.gameObject, _downTime, new Vector3(0,0, 31));
				tween.method = _downMethod;
				return tween;
			});
		
			return tt;
		}
		public Task openCurtainTask(int i){
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait();
			tw.setAllTime(0.2f*i + 0.5f);
			tl.push (tw);
			TweenTask tq = new TweenTask (delegate() {
				Tween tween =  TweenLocalPosition.Begin(_curtain[i].gameObject, 1.0f, new Vector3(_curtain[i].gameObject.transform.localPosition.x, -30, _curtain[i].gameObject.transform.localPosition.z));
				tween.method = _curtainMethod;
				return tween;
			});
			tl.push (tq);
			return tl;
		}

		public Task closeCurtainTask(int i){
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait();
			tw.setAllTime(0.2f*(_curtain.Length-i-1) + 0.5f);
			tl.push (tw);
			TweenTask tq = new TweenTask (delegate() {
				Tween tween =  TweenLocalPosition.Begin(_curtain[i].gameObject, 1.0f, new Vector3(_curtain[i].gameObject.transform.localPosition.x, 0, _curtain[i].gameObject.transform.localPosition.z));
				tween.method = _curtainMethod;
				return tween;
			});
			tl.push (tq);
			return tl;
		}
		public Task closeTask ()
		{
			TaskSet ts = new TaskSet ();
			
			for (int i =0; i<_curtain.Length; ++i) {
				ts.push (closeCurtainTask(i));
			}
			
			return ts;
		}
		public Task openTask ()
		{

			TaskSet ts = new TaskSet ();
		
			for (int i =0; i<_curtain.Length; ++i) {
				ts.push (openCurtainTask(i));
			}
			
			return ts;
		}

		public Task nextTask ()
		{
			TaskSet ts = new TaskSet ();
			TweenTask tt = new TweenTask (delegate() {
				Tween tween =  TweenLocalPosition.Begin(this.gameObject, 1.0f, new Vector3(0, -50, 30));
				tween.method = _curtainMethod;
				return tween;
			});
			ts.push (tt);
			/*for (int i =0; i<_curtain.Length; ++i) {
				ts.push (curtainTask(i));
			}*/

			return ts;
		}


	}
}