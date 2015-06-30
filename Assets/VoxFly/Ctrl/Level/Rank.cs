using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	namespace LevelMode{
		public class Rank : Abstract {

			public RockManager _rockManager = null;
			private float length_ = 0.0f;
			private float begin_ = 0.0f;
			private int chapters_ = 0;
			private int paragraph_ = 0;
		/*		
			public override void clear(){
				
			}*/
			public override void end (){
				
			}
			private void doAction(){
				int r = Random.Range (0, 4);
				for (int i =0; i<4; ++i) {
					if(r == i){
						if(paragraph_ != 0){
							_rockManager._doAction (0.33f * (float)(i), paragraph_);
						}
					}else{
						_rockManager._doAction (0.33f * (float)(i), 9);
					}
				}


			
			}
			private bool over_ = false;
			public override bool isOver(){
				return over_;
			}

			public override void post(){
				
			}
			public override void doMove (float length)
			{
				if (length - begin_ > 30.0f) {
					over_ = true;
				}else if (length - length_ > 10.0f) {
					length_ = length;
					doAction();
				}
			}
			public override void begin(float length, int chapters, int paragraph){

				chapters_ = chapters;
				paragraph_ = paragraph;
				over_ = false;
				length_ = length;
				begin_ = length;
				post ();
			}
		
		}
	}

}