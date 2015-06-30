using UnityEngine;
using System.Collections;
using System;
using GDGeek;
using System.Collections.Generic;


namespace VoxelTrek{
	
	namespace LevelMode{
		public class LevelManager : MonoBehaviour {
			//fsm ???
			public List<Abstract> _list = new List<Abstract>();
			
			public List<Abstract> _random = new List<Abstract>();
			private Abstract mode_;
			private int chapters_ = 0;
			private int paragraph_ = 0;

			public float _step = 200.0f;
			private float length_ = 0.0f;
			public VoxelText _vText = null;
			public RockManager _rockManager = null;

			private int index_ = -1;
			private Abstract next(){

				++index_;	
				if (index_ >= this._list.Count) {
					TempBGM.GetInstance().fuck ();
					return 	_random[UnityEngine.Random.Range(0, _random.Count)];		
				}
				return _list[index_];		
			}
			private void doMove (float length)
			{
				
				if (mode_ == null || mode_.isOver ()) {
					if(length - length_  >= _step){
						length_ = length;
						++paragraph_;
						if(paragraph_ >= 5){
							paragraph_ = 0;
							++chapters_;
						}

					}
					if(mode_ != null){
						mode_.end ();
					}
					mode_ = next();
					mode_.begin(length, chapters_, paragraph_);
				} 
				
				mode_.doMove (length);
				_vText.text = Mathf.FloorToInt(length).ToString()+ "M";
				
			}
			public void clickDown (){
				if (mode_ != null) {
					mode_.post();
				}
			}
			
		
			public void building(){
				Debug.Log (index_);
				GameManager.GetInstance ().road.onMove += this.doMove;
				_vText.text = "0M";
				this.mode_ = next ();
				index_ = 0;
				chapters_ = 0;

				paragraph_ = 0;
				this.mode_.begin (0.0f, chapters_, paragraph_);
				
			}
			public void unbuild(){

				if (this.mode_ != null) {
					this.mode_.end();				
				}
				GameManager.GetInstance ().road.onMove -= this.doMove;
				_rockManager.clear ();
				length_ = -1;

			}
			
			
		}
	}
}