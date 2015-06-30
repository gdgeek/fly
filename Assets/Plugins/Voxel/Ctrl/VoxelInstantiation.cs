using UnityEngine;
using System.Collections;
using GDGeek;


[ExecuteInEditMode]
public class VoxelInstantiation : MonoBehaviour {
	//public Vector3 _fixSize = Vector3.zero;
	public VoxelModel _model = null;
	public VoxelMesh _mesh = null;
	public bool _building = false;
	public bool _destroying = false;
	// Use this for initialization
	void Start () {
//		UnityEditor.DockArea.OnGUI ();
//		Debug.Log(_model._data.Length + "!!!!!!");;
	}
	
	// Update is called once per frame
	void Update () {
		if (_building) {


			if(_mesh.empty){
				//_mesh._fixSize = _fixSize;
				_mesh.build (_model.data);
				
				VoxelFunctionManager vf = _model.gameObject.GetComponent<VoxelFunctionManager>();
				if(vf != null){
					vf.build(_mesh);
				}
			}	
			_building = false;
		}
		
//		Debug.Log ("???"+_destroying);
		if (_destroying) {
//			Debug.Log ("???");
			if(!_mesh.empty){
				_mesh.clear ();
				
//				VoxelFunctionManager vf = _model.gameObject.GetComponent<VoxelFunctionManager>();
//				vf.clear();
			}	
			_destroying = false;
		}

	}
}
