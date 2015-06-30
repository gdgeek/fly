using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelEmitterFunc : VoxelFunction {
		public VoxelEmitter _emitter;
		public override void build (VoxelMesh mesh, Voxel c)
		{
			VoxelEmitter emitter = (VoxelEmitter)GameObject.Instantiate (_emitter);
			emitter.transform.parent = mesh.transform;
			emitter.transform.position = c.transform.position;
			emitter.transform.eulerAngles =c.transform.eulerAngles;
			emitter.gameObject.SetActive (true);
			//emitter.transform = c.transform.rotation;
		}
	}
}