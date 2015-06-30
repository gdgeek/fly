using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	public class Test : MonoBehaviour {
		
		public VoxelHead _me = null;
		public VoxelFontManager _mePop = null;

		public VoxelHead _head = null;
		public VoxelFontManager _pop = null;  
		// Use this for initialization

		private Task talk2(Task task){
			TaskManager.PushFront (task, delegate {
				_head.talk = true;
			});
			
			TaskManager.PushBack (task, delegate {
				_head.talk = false;
			});
			return task;
		}
		private Task talk(Task task){
			TaskManager.PushFront (task, delegate {
				_me.talk = true;
			});
			
			TaskManager.PushBack (task, delegate {
				_me.talk = false;
			});
			return task;
		}
		void Start () {
			_head.close ();
			_me.close ();
			_mePop.close ();
			_pop.close ();
			TaskList tl = new TaskList ();
			tl.push (new TaskWait (1.0f));
			TaskSet ts = new TaskSet ();
			
			tl.push (_me.openTask());
			tl.push (_head.openTask());
			Task pop = _mePop.openTask ("那边的是楚云帆老师吗？");
			
			tl.push (talk(pop));
			tl.push (new TaskWait(2.0f));

			tl.push (talk(_mePop.next("这次比赛能给高分么？")));
			tl.push (_mePop.closeTask());

			tl.push (talk2(_pop.openTask ("我有看过你的作品么？")));
			TaskWait tw = new TaskWait (0.5f);


			TaskManager.PushBack (tw, delegate {
				_me.emotion = VoxelHead.Emotions.Surprise;
						});
			tl.push (tw);
			TaskManager.Run (tl);
			//TaskManag
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}