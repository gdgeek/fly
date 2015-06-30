using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	namespace LevelMode{
		public class Talk : Abstract {
			private float time_ = 0.0f;
			private bool over_ = false;
			public VoxelTalkManager _talk = null;
			public VoxelTalkManager.Sentence[] _sentence;

			private TaskList tl_ = null; 
			public override bool isOver(){
				return over_;
			}
			/*
			public override void clear(){

			}*/
			public override void end(){
				Debug.Log ("ASDFASDF");
				tl_.forceQuit ();
			}
			

			public override void post(){
				_talk.goNext ();
			}


			public override void begin (float length, int chapters, int paragraph)
			{
				time_ = 0.0f;
				this.over_ = false;
				GameManager.GetInstance ().road._speed = 0.001f;
				tl_ = new TaskList ();
				tl_.push (_talk.comeInTask ());
				for (int i = 0; i<_sentence.Length; ++i) {
					tl_.push (_talk.popTask (_sentence[i]));	
				}
				tl_.push (_talk.goOutTask ());
				TaskManager.PushBack (tl_, delegate {
					this.over_ = true;
					_talk.close();
					GameManager.GetInstance ().road._speed = 1.0f;
				});
				TaskManager.Run (tl_);
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