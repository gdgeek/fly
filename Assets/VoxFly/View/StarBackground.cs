using UnityEngine;
using System.Collections;
using GDGeek;
namespace VoxelTrek{
	public class StarBackground : MonoBehaviour {
	
		public BGLayer[] _layers = null;
	
		private void doMove(float length){
			for (int i  =0; i <_layers.Length; ++i) {
				_layers[i].roll(length);
			}
		}
		void Start () {
			GameManager.GetInstance ().road.onMove += doMove;

		}

		void OnDisable(){
			GameManager.GetInstance ().road.onMove -= doMove;
		}
		
		void OnDestroy(){
			GameManager.GetInstance ().road.onMove -= doMove;
		}


		// Update is called once per frame
		void Update () {

		}
	}
}
