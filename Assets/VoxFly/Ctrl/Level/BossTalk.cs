using UnityEngine;
using System.Collections;
using GDGeek;
using System;

namespace VoxelTrek
{

	namespace LevelMode{


		public class BossTalk : Abstract {

			private float time_ = 0.0f;
			private bool over_ = false;
			public VoxelTalkManager[] _talk = null;
			
			public bool[] _close = null;
			[Serializable]
			public struct Sentence{
				public int index;
				public VoxelTalkManager.Sentence talk;
			};


			public Sentence[] _sentence;

			public override bool isOver(){
				return over_;
			}
			
/*			public override void clear(){
				
			}*/
			public override void end (){
				
			}
			public override void post(){
				//_talk.goNext ();
			}

			TaskPack.CreateTask talkTask (Sentence sentence)
			{
				return delegate {
					VoxelTalkManager talk = this._talk[sentence.index];
					if(!talk.isOpen()){
						TaskList tl = new TaskList();
						tl.push (talk.comeInTask());
						tl.push (talk.popTask(sentence.talk));

						return tl;
					}else{
						return talk.popTask(sentence.talk);
					}		
						


				};
			}		
			
			public override void begin (float length, int chapters, int paragraph)
			{
				time_ = 0.0f;
				this.over_ = false;
				GameManager.GetInstance ().road._speed = 0.001f;
				TaskList tl = new TaskList ();

				for (int i = 0; i < _sentence.Length; ++i) {
					tl.push (new TaskPack(talkTask(_sentence[i])));			
				}
				for (int i = 0; i < _talk.Length; ++i) {

					if(_close[i]){
						tl.push (_talk[i].goOutTask ());
					}else{
						tl.push (_talk[i].closePopTask ());

					}
				}
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
