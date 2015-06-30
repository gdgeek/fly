using UnityEngine;
using System.Collections;
using GDGeek;
using System.IO;


namespace GDGeek{
	
	[ExecuteInEditMode]
	public class VoxelFilePreLoading : MonoBehaviour {
		public VoxelModel _model = null;

		public string _vodFile = "fly.vox";
		public bool _refresh = false;
		#if UNITY_EDITOR
		void Update () {
			if (_refresh && !string.IsNullOrEmpty(_vodFile)) {
				
				FileStream sw = new FileStream (_vodFile, FileMode.Open, FileAccess.Read);
				System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 
				if(_model != null){
					_model.data = VoxelReader._FromMagica (br);
				}
				_refresh = false;
			}
			
		}
		
		#endif

	}
}