using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	
	namespace LevelMode{
		public class Converse : Abstract {


			private float time_ = 0.0f;
			private bool over_ = false;
			public VoxelTalkManager _talk = null;
			public VoxelTalkManager.Sentence[] _sentence;
			public override bool isOver(){
				return over_;
			}
			
/*			public override void clear(){
				
			}*/
			public override void end (){
				
			}
			public override void post(){
				_talk.goNext ();
			}
			
			
			public override void begin (float length, int chapters, int paragraph)
			{
				time_ = 0.0f;
				this.over_ = false;
				GameManager.GetInstance ().road._speed = 0.001f;
				TaskList tl = new TaskList ();
				tl.push (_talk.comeInTask ());
				for (int i = 0; i<_sentence.Length; ++i) {
					tl.push (_talk.popTask (_sentence[i]));	
				}
				
				//tl.push (_talk.popTask ("迎a欢"));
				//tl.push (_talk.popTask ("欢迎迎"));
				//			tl.push (_talk.popTalk (_sentence));
				tl.push (_talk.goOutTask ());
				TaskManager.PushBack (tl, delegate {
					this.over_ = true;
					GameManager.GetInstance ().road._speed = 1.0f;
				});
				TaskManager.Run (tl);
			}
			
			public void Update(){
				time_ += Time.deltaTime;
				
			}
			
			
			public override void doMove (float length)
			{
				
			}
		}
	}
}