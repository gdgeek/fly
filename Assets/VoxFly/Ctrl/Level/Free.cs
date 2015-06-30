using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	namespace LevelMode{
		public class Free : Abstract {
			
			public RockManager _rockManager = null;
			private float length_ = 0.0f;
			private float begin_ = 0.0f;
			private int chapters_ = 0;
			private int paragraph_ = 0;
			private float step_ = 0;

			/*
			public override void clear(){
				
			}*/
			public override void end ()
			{

			}
			private void doAction(){

				if (paragraph_ <= 5) {
					if (Random.Range (0, 2) == 1) {
						_rockManager._doAction (Random.Range (0.0f, 1.0f),Random.Range (paragraph_+1, 5));
					}
				} else {
					_rockManager._doAction (Random.Range (0.0f, 1.0f), paragraph_);
				}
				
				
				
			}
			
			public override void doMove (float length)
			{
				if (length - begin_ > 30.0f) {
					over_ = true;
				}else if (length - length_ > step_) {
					length_ = length;
					doAction();
				}
			}
			private bool over_ = false;
			public override bool isOver(){
				return over_;
			}
			public override void post(){
					
			}
			public override void begin(float length, int chapters, int paragraph){
				
				GameManager.GetInstance ().road._speed = 1.0f;
				chapters_ = chapters;
				paragraph_ = paragraph;
				step_ = 3.0f;
				over_ = false;
				length_ = length;
				begin_ = length;
				post ();
				_rockManager._doAction (Random.Range (0.0f, 1.0f), 2);
			}
			
		}
	}
	
}