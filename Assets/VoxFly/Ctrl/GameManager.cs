using UnityEngine;
using System.Collections;

namespace VoxelTrek
{
	public class GameManager : MonoBehaviour {


		private static GameManager instance_ = null; 
		public RoadManager _road = null;


		public RoadManager road{
			get{
				return _road;
			}
		}
		
		void Awake(){
			GameManager.instance_ = this;
		}

		
		public static GameManager GetInstance(){
			return GameManager.instance_;
		}

	}
}