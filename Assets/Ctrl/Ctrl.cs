using UnityEngine;
using System.Collections;
using GDGeek;
namespace Transformers{
	public class Ctrl : MonoBehaviour {
		public VoxelModelEditor _fly;
		public VoxelModelEditor _fly3;
		public VoxelManager _manager;
		// Use this for initialization
		void Start () {

			var mesh = _manager.create (_fly3);
			mesh.gameObject.SetActive (false);
			Transformer a = new Transformer ();
			VoxelBlow b = new VoxelBlow ();
			a.from = _fly._data;
			a.to = _fly3._data;
//			b.data = _fly._data;
			b.mesh = _manager.create (_fly);

			TaskList tl = new TaskList ();
			TaskWait tw = new TaskWait ();
			tw.setAllTime (1.0f);
			tl.push (tw);
			tl.push (b.blow());
			TaskManager.PushBack (tl, delegate {
				Debug.Log ("!!!!!!!");
				b.mesh.gameObject.SetActive(false);
						});
			TaskManager.Run (tl);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}