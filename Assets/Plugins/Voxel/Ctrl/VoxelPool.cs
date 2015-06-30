﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class VoxelPool : MonoBehaviour {
		public delegate void DoAction(VoxelPoolObject obj);
		public int _reserve = 0;
		public event DoAction doEnable;
		public event DoAction doDisable;

		public VoxelPoolObject _prototype = null;
		private Stack<VoxelPoolObject> _pool = new Stack<VoxelPoolObject>();
		public void Awake(){
			_prototype.gameObject.SetActive (false);
			VoxelPoolObject[] list = new VoxelPoolObject[_reserve];
			for (int i =0; i <_reserve; ++i) {
				list[i] = this.create();	
				list[i].gameObject.SetActive(true);	
			}
			for (int i =0; i <_reserve; ++i) {
				list[i].gameObject.SetActive(false);		
			}

		}
		public VoxelPoolObject create(){
			VoxelPoolObject obj = null;
			if (_pool.Count == 0) {
				obj = GameObject.Instantiate (_prototype);
				obj.transform.parent = this.transform;
				obj.doDisable += delegate(VoxelPoolObject po){
					if(doDisable != null){
						doDisable(po);
					}
					this.destory(po);
				};


				obj.doEnable += delegate(VoxelPoolObject po){
					if(doEnable != null){
						doEnable(po);
					}
				};


			} else {
				obj = _pool.Pop ();
			}
			return obj;
		}  
		public void destory(VoxelPoolObject obj){
			
			//obj.transform.SetParent(this.transform);
			_pool.Push (obj);

		}

	}
}