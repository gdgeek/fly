using UnityEngine;
using System.Collections;
using GDGeek;
namespace VoxelTrek{
	public class Heart : MonoBehaviour {
		public VoxelMesh[] _mesh;
		public VoxelMesh _empty;

		public void Awake(){
			value = _mesh.Length;
		}
		public int all {
			get{
				return _mesh.Length;
			}
		}
		public int value{

			set{
				foreach(VoxelMesh m in _mesh){
					m.gameObject.SetActive(false);
				}

				if(value > this.all){
					value = all;
				}else if(value < 0){
					value = 0;
				}
				_empty.gameObject.SetActive(false);
				if(value == 0){
					_empty.gameObject.SetActive(true);

				}else{

					_mesh[3-value].gameObject.SetActive(true);
				}
			}
		}

	}
}