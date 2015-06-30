using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	namespace LevelMode{
		public class Boss : Abstract {

			public BossFace _boss = null;
			public override void begin (float length, int chapters, int paragraph){
				_boss.running ();
				GameManager.GetInstance ().road._speed = 0;
				TempBGM.GetInstance().boss();
			}
		/*	public override void clear(){
				_boss.clear ();
			}*/
			public override void doMove (float length){
			}
			public override bool isOver (){
				return !_boss.alive;
			}
			
			public override void end (){
				TempBGM.GetInstance().play();
				_boss.clear ();
			}
			public override void post (){

			}
		}
	}
}
