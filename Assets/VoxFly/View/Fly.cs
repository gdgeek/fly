using UnityEngine;
using System.Collections;
using GDGeek;
namespace VoxelTrek
{
	public class Fly : MonoBehaviour
	{
		public delegate void DoHurt(Damage damage);
		public VoxelEmitter[] _emitters = null;
		public VoxelEmitter _boom = null;
		public event DoHurt doHurt;
		public VoxelMesh _mesh;
		public void toBeHurtCB(Damage damage){
//			Debug.Log ("!!!!");
			if (doHurt != null) {
				doHurt(damage);			
			}
		}

		public void _shifting (float l)
		{
//			Debug.Log (l + "!!!!!");
			if (this != null) {
//				VoxelMesh vm = this.gameObject.GetComponent<VoxelMesh> ();
				this.transform.eulerAngles = new Vector3 (0, Mathf.Max (-30, Mathf.Min (30, l)), 0);
			}
		}



//		private Dirver.Group group_ = Dirver.Group.Friend;
		public VoxelEmitter.Parameter _nomal;
		public VoxelEmitter.Parameter _fire;
		public void Awake(){
			jetPower = false;
			refreshJet ();
			this.gameObject.transform.eulerAngles =  new Vector3(0, 0, 0);
			this.gameObject.transform.localPosition = new Vector3(0, -60, 0);
		}
		public Task downTask(){
			TaskList tl = new TaskList ();
			VoxelMesh vm = this.gameObject.GetComponent<VoxelMesh>();
			TweenTask tt = new TweenTask (delegate() {
				if(vm._offset.gameObject != null){
					Tween tween = TweenLocalPosition.Begin(vm._offset.gameObject, 0.05f, new Vector3(0, 0, 3));
					tween.method = Tween.Method.easeOutCubic;
					return tween;
				}else{
					return null;
				}
			});
			tl.push (tt);
			TweenTask t2 = new TweenTask (delegate() {
				if(vm._offset.gameObject != null){
					
					Tween tween = TweenLocalPosition.Begin(vm._offset.gameObject, 0.05f, new Vector3(0, 0, 0));
					tween.method = Tween.Method.easeInOutBounce;
					return tween;
				}else{
					return null;
				}
			});
			tl.push (t2);
			TaskManager.PushFront (tl, delegate {
				jetPower = true;
						});
			TaskManager.PushBack (tl, delegate {
				//jetPower = _fire;
				refreshJet();
			});
			return tl;		
		}
		private bool jetPower_ = false;
		private void refreshJet ()
		{

			if(this!=null){
			//	VoxelEmitter[] emitters = this.GetComponentsInChildren<VoxelEmitter>();
				for(int i=0; i<_emitters.Length; ++i){
					if(jetPower_){
						_emitters[i]._parameter = _fire;
					}else{
						_emitters[i]._parameter = _nomal;
					}
				}
			}
		}

		public Task upTask(){
			Task task = new Task ();
			TaskManager.PushBack (task, delegate {
				jetPower = false;

			});
			TaskManager.PushBack (task, delegate {
				//jetPower = _nomal;
				refreshJet();
			});
			return task;		
		}
	

		public bool jetPower{
			set{
				jetPower_ = value;
			}
			
		}
		public bool jet{
			set{
				if(this!=null){
					for(int i=0; i<_emitters.Length; ++i){
						_emitters[i].gameObject.SetActive(value);

					}
				}
			}

		}

	}

}
