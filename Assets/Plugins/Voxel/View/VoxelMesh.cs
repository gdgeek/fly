using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class VoxelMesh : MonoBehaviour {

		public bool _offsetEnable = true;
		public void setLayer (int layer)
		{
			this.gameObject.layer = layer;
			_offset.gameObject.layer = layer;
			this._mesh.gameObject.layer = layer;
			if (_root != null) {
				_root.gameObject.layer = layer;
				for(int i = 0; i<_datas.Length; ++i){
					if(_datas[i]._vox != null){
						_datas[i]._vox.gameObject.layer = layer;
					}
				}
			}

		}
	//	public Vector3 _fixSize = Vector3.zero;
		
		public Material _material = null;
		//public Texture _texture = null; 


		public VoxelHandler[] _datas = null;
		public GDGeek.Voxel _prototype = null;
		
		
		public bool _XReverse = false;
		public bool _YReverse = false;
		public bool _ZReverse = false;


		
		//public VoxelDrawMesh _drawMesh = null;
		public GameObject _root = null;
		
		public GameObject _offset = null;
		
		private Vector3 min_ = new Vector3(999, 999, 999);
		private Vector3 max_ = new Vector3(-999, -999, -999);



		private Dictionary<VectorInt3, VoxelHandler> voxels_ = new Dictionary<VectorInt3, VoxelHandler>();



		public Dictionary<VectorInt3, VoxelHandler> voxels{
			get{
				if(voxels_.Count == 0 && _datas !=null){
					foreach (VoxelHandler handler in _datas) {
						voxels_.Add (handler.position, handler);		
					}
				}
				return voxels_;
			}
		}

		private VoxelHandler data2Handler (VoxelData data)
		{

			VoxelHandler handler = new VoxelHandler();
			VectorInt3 position;
			if (_XReverse) {
				position.x = (int)this.max_.x - data.x + (int)this.min_.x; 
			} else {
				position.x = data.x;
			}

			if (_YReverse) {
				position.y = (int)this.max_.y - data.y + (int)this.min_.y; 
			} else {
				position.y = data.y;
			}

			if (_ZReverse) {
				position.z = (int)this.max_.z - data.z + (int)this.min_.z; 
			} else {
				position.z = data.z;
			}
			handler.position = position;
			handler.color = data.color;
			handler.id = data.id;

			return handler;

		}

		private void createHandler(VoxelData[] datas) {
		

			
			min_ = new Vector3(999, 999, 999);
			max_ = new Vector3(-999, -999, -999);

			_datas = new VoxelHandler[datas.Length];
			for (int i=0; i<datas.Length; ++i) {
				VoxelData d = datas [i];
				this.getMaxMin (d.x, d.y, d.z);
			}
			for (int i=0; i<datas.Length; ++i) {
				_datas[i] = data2Handler(datas [i]);
			}


		} 
		
	
		public bool empty{
			get{
				return _mesh == null;
			}
		}
		public void build (VoxelData[] datas)
		{

			if (empty) {

				createHandler(datas);

				if(_root != null){
					for (int i=0; i<_datas.Length; ++i) {
						VoxelHandler d = _datas[i];	
						this.createVoxel (_datas[i]);

					}
				}
				this.shadowBuild ();
				this.buildMesh ();
			}
		
		}



		public void load (VoxelData[] to)
		{
			throw new System.NotImplementedException ();
		}

		public void clear ()
		{
			
			if (!empty) {

				if(_datas != null){
					foreach (VoxelHandler handler in _datas){
						if(handler._vox && handler._vox.gameObject){
							GameObject.DestroyImmediate(handler._vox.gameObject);
						}
					}

					voxels_.Clear();
					_datas = null;
				}
				this.clearMesh ();
			}
		}

	

		public VoxelHandler getVoxel (VectorInt3 position)
		{
			
			var voxs = this.voxels;
			if (voxs.ContainsKey (position)) {
				return voxs [position];
			}
			return null;
		}

		public bool collider (GDGeek.VoxelMesh voxelMesh){
			return true;
		}

		public BoxCollider _collider = null;

		public Vector3 min{
			get{
				return min_;
			}
		}
		public Vector3 max{
			get{
				return max_;
			}
		}

		public void showMesh ()
		{
			_mesh.gameObject.SetActive (true);
			if (_root != null) {
				_root.SetActive (false);
			}
			refersh ();

		}
		private void refersh(){
			Vector3 offset = Vector3.zero;
			if (_offsetEnable) {
				offset = (min_ + max_) / -2.0f;
				offset.z = -offset.z;
			}

			this._mesh.transform.localPosition = offset;
			if (_root != null) {
				_root.transform.localPosition = offset;
			}


			if (_collider != null) {
				_collider.size = new Vector3 (max_.x - min_.x + 1, max_.z - min_.z + 1, max_.y - min_.y + 1);
			}
		}
		public void showVox ()
		{
			if (_root != null) {
				_root.gameObject.SetActive (true);
			}
			_mesh.gameObject.SetActive (false);
			refersh ();
		}
		public MeshFilter _mesh;

			
		public void setLightColor(Color color){
			Renderer renderer = _mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_LightColor", color);
		}
		public void setMainColor(Color color){
			Renderer renderer = _mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_MainColor", color);
		}
		public void clearMesh(){
			
			//_mesh = this.GetComponentInChildren<MeshFilter>();
			if (_mesh) {
				GameObject.DestroyImmediate (_mesh.gameObject);
			}
			_mesh = null;

		}
		public void buildMesh ()
		{	
			VoxelDrawMesh draw = new VoxelDrawMesh ();
		//	_drawMesh._clear ();
			Dictionary<VectorInt3, VoxelHandler> voxs = this.voxels; 
//			Debug.Log (voxs.Count + "!!!!");
			foreach (KeyValuePair<VectorInt3, VoxelHandler> kv in voxs) {
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0,  -1, 0))){
					VectorInt4 v = draw.addRect (Vector3.back, kv.Key,  VoxelHandler.GetUV(kv.Value.back), VoxelHandler.GetUV(kv.Value.lback), kv.Value.color);
					kv.Value.vertices.Add(v);
				}
				
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 1, 0))){
					VectorInt4 v = draw.addRect (Vector3.forward, kv.Key,  VoxelHandler.GetUV(kv.Value.front), VoxelHandler.GetUV(kv.Value.lfront), kv.Value.color);
					kv.Value.vertices.Add(v);
				}
				
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 0, 1))){
					VectorInt4 v = draw.addRect (Vector3.up, kv.Key, VoxelHandler.GetUV(kv.Value.up), VoxelHandler.GetUV(kv.Value.lup), kv.Value.color);
					kv.Value.vertices.Add(v);
				}

				
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 0, -1))){
					VectorInt4 v = draw.addRect (Vector3.down, kv.Key, VoxelHandler.GetUV(kv.Value.down), VoxelHandler.GetUV(kv.Value.ldown), kv.Value.color);
					kv.Value.vertices.Add(v);
				}

				
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(1, 0, 0))){
					VectorInt4 v = draw.addRect (Vector3.left, kv.Key, VoxelHandler.GetUV(kv.Value.left), VoxelHandler.GetUV(kv.Value.lleft), kv.Value.color);
					kv.Value.vertices.Add(v);
				}

				
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(-1, 0, 0))){
					VectorInt4 v = draw.addRect (Vector3.right, kv.Key, VoxelHandler.GetUV(kv.Value.right), VoxelHandler.GetUV(kv.Value.lright), kv.Value.color);
					kv.Value.vertices.Add(v);
				}
			}
			//Resources.
			_mesh = draw.crateMeshFilter ("Static", _material);
			_mesh.gameObject.transform.SetParent (this._offset.transform);
			_mesh.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
			
			_mesh.transform.localRotation = Quaternion.LookRotation(Vector3.up);
			_mesh.gameObject.SetActive (true);
			showMesh ();
		}

		private void getMaxMin(int x, int y, int z){
			min_.x = Mathf.Min (min_.x, x);
			min_.y = Mathf.Min (min_.y, y);
			min_.z = Mathf.Min (min_.z, z);
			max_.x = Mathf.Max (max_.x, x);
			max_.y = Mathf.Max (max_.y, y);
			max_.z = Mathf.Max (max_.z, z);
		}

		private void createVoxel (VoxelHandler handler)
		{

			//VoxelHandler handler = new VoxelHandler ();

			Voxel vox = (Voxel)GameObject.Instantiate (_prototype);
			vox.transform.SetParent (_root.transform);
			vox.transform.localRotation = Quaternion.LookRotation(Vector3.up);


			handler.setup (vox);
			/*
			var voxs = this.voxels;
			if (voxs.ContainsKey(handler.position)) {
				voxs.Remove(handler.position);	
			}
			voxs.Add (handler.position, handler);
			*/


		}


		private void shadowTest(VoxelHandler handler, VectorInt3 shadow, VectorInt3 light, byte index, Vector3 face){
			
			var voxs = this.voxels;
			if (voxs.ContainsKey (handler.position + shadow)) {
				handler.shadowAdd (index, face);			
			} else 	if (!voxs.ContainsKey (handler.position + light)) {
				handler.lightAdd (index, face);			
			}
		}
		public void shadowBuild(){
			
			var voxs = this.voxels;
			foreach (KeyValuePair<VectorInt3, VoxelHandler> kv in voxs) {
				kv.Value.lightShadowBegin();

				/*	*/	/**/

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, 1), new VectorInt3(-1, 0, 1), 0, Vector3.back);
				shadowTest(kv.Value, new VectorInt3(0, -1, 1), new VectorInt3(0, 0, 1), 1, Vector3.back);
				shadowTest(kv.Value, new VectorInt3(1, -1, 1), new VectorInt3(1, 0, 1), 2, Vector3.back);

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, 0), new VectorInt3(-1, 0, 0), 3, Vector3.back);
				shadowTest(kv.Value, new VectorInt3(1, -1, 0), new VectorInt3(1, 0, 0), 4, Vector3.back);

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, -1), new VectorInt3(-1, 0, -1), 5, Vector3.back);
				shadowTest(kv.Value, new VectorInt3(0, -1, -1), new VectorInt3(0, 0, -1), 6, Vector3.back);
				shadowTest(kv.Value, new VectorInt3(1, -1, -1), new VectorInt3(1, 0, -1), 7, Vector3.back);
				
				//=========================


				
				shadowTest(kv.Value, new VectorInt3(-1, 1, -1), new VectorInt3(-1, 0, -1), 0, Vector3.forward);
				shadowTest(kv.Value, new VectorInt3(0, 1, -1), new VectorInt3(0, 0, -1), 1, Vector3.forward);
				shadowTest(kv.Value, new VectorInt3(1, 1, -1), new VectorInt3(1, 0, -1), 2, Vector3.forward);

				
				shadowTest(kv.Value, new VectorInt3(-1, 1, 0), new VectorInt3(-1, 0, 0), 3, Vector3.forward);
				shadowTest(kv.Value, new VectorInt3(1, 1, 0), new VectorInt3(1, 0, 0), 4, Vector3.forward);

				
				shadowTest(kv.Value, new VectorInt3(-1, 1, 1), new VectorInt3(-1, 0, 1), 5, Vector3.forward);
				shadowTest(kv.Value, new VectorInt3(0, 1, 1), new VectorInt3(0, 0, 1), 6, Vector3.forward);
				shadowTest(kv.Value, new VectorInt3(1, 1, 1), new VectorInt3(1, 0, 1), 7, Vector3.forward);
				//=========================

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, -1), new VectorInt3(-1, -1, 0), 0, Vector3.down);
				shadowTest(kv.Value, new VectorInt3(0, -1, -1), new VectorInt3(0, -1, 0), 1, Vector3.down);
				shadowTest(kv.Value, new VectorInt3(1, -1, -1), new VectorInt3(1, -1, 0), 2, Vector3.down);
				
				shadowTest(kv.Value, new VectorInt3(-1, 0, -1), new VectorInt3(-1, 0, 0), 3, Vector3.down);
				shadowTest(kv.Value, new VectorInt3(1, 0, -1), new VectorInt3(1, 0, 0), 4, Vector3.down);

				shadowTest(kv.Value, new VectorInt3(-1, 1, -1), new VectorInt3(-1, 1,0), 5, Vector3.down);
				shadowTest(kv.Value, new VectorInt3(0, 1, -1), new VectorInt3(0, 1, 0), 6, Vector3.down);
				shadowTest(kv.Value, new VectorInt3(1, 1,-1), new VectorInt3(1, 1, 0), 7, Vector3.down);


				
				shadowTest(kv.Value, new VectorInt3(-1, 1, 1), new VectorInt3(-1, 1, 0), 0, Vector3.up);
				shadowTest(kv.Value, new VectorInt3(0, 1, 1), new VectorInt3(0, 1, 0), 1, Vector3.up);
				shadowTest(kv.Value, new VectorInt3(1, 1, 1), new VectorInt3(1, 1, 0), 2, Vector3.up);
				
				shadowTest(kv.Value, new VectorInt3(-1, 0, 1), new VectorInt3(-1, 0, 0), 3, Vector3.up);
				shadowTest(kv.Value, new VectorInt3(1, 0, 1), new VectorInt3(1, 0, 0), 4, Vector3.up);

				shadowTest(kv.Value, new VectorInt3(-1, -1, 1), new VectorInt3(-1, -1, 0), 5, Vector3.up);
				shadowTest(kv.Value, new VectorInt3(0, -1, 1), new VectorInt3(0, -1, 0), 6, Vector3.up);
				shadowTest(kv.Value, new VectorInt3(1, -1, 1), new VectorInt3(1, -1, 0), 7, Vector3.up);

				
				
				//=========================


				
				shadowTest(kv.Value, new VectorInt3(1, 1, -1), new VectorInt3(0, 1, -1), 0, Vector3.left);
				shadowTest(kv.Value, new VectorInt3(1, 0, -1), new VectorInt3(0, 0, -1), 1, Vector3.left);
				shadowTest(kv.Value, new VectorInt3(1, -1, -1), new VectorInt3(0, -1, -1), 2, Vector3.left);

				
				shadowTest(kv.Value, new VectorInt3(1, 1, 0), new VectorInt3(0, 1, 0), 3, Vector3.left);
				shadowTest(kv.Value, new VectorInt3(1, -1, 0), new VectorInt3(0, -1, 0), 4, Vector3.left);

				
				shadowTest(kv.Value, new VectorInt3(1, 1, 1), new VectorInt3(0, 1, 1), 5, Vector3.left);
				shadowTest(kv.Value, new VectorInt3(1, 0, 1), new VectorInt3(0, 0, 1), 6, Vector3.left);
				shadowTest(kv.Value, new VectorInt3(1, -1, 1), new VectorInt3(0, -1, 1), 7, Vector3.left);

				
				
				//=========================

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, -1), new VectorInt3(0, -1, -1), 0, Vector3.right);
				shadowTest(kv.Value, new VectorInt3(-1, 0, -1), new VectorInt3(0, 0, -1), 1, Vector3.right);
				shadowTest(kv.Value, new VectorInt3(-1, 1, -1), new VectorInt3(0, 1, -1), 2, Vector3.right);

				
				shadowTest(kv.Value, new VectorInt3(-1, -1, 0), new VectorInt3(0, -1, 0), 3, Vector3.right);
				shadowTest(kv.Value, new VectorInt3(-1, 1, 0), new VectorInt3(0, 1, 0), 4, Vector3.right);
				
				shadowTest(kv.Value, new VectorInt3(-1, -1, 1), new VectorInt3(0, -1, 1), 5, Vector3.right);
				shadowTest(kv.Value, new VectorInt3(-1, 0, 1), new VectorInt3(0, 0, 1), 6, Vector3.right);
				shadowTest(kv.Value, new VectorInt3(-1, 1, 1), new VectorInt3(0, 1, 1), 7, Vector3.right);
				
				/**/
				
				kv.Value.lightShadowEnd();
			}
			
		}


	}
}
