using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	namespace LevelMode{
		public class Speeddup : Abstract {


			public UISpeedup _ui;
			private bool over_ = false;
			public override bool isOver(){
				return over_;
			}
			
/*			public override void clear(){
				
			}*/
			public override void end(){
				
			}
			public override void post(){
				
			}
			public override void begin (float length, int chapters, int paragraph)
			{
				over_ = false;
				Task task = _ui.show ();
				TaskManager.PushBack (task, delegate {
					over_ = true;
					GameManager.GetInstance().road.degree = 1.0f + (float)(chapters) * 0.5f;
							});
				TaskManager.Run (task);
			}
			
			
			
			public override void doMove (float length)
			{
				
			}
			
		

		}
	}
}
