using UnityEngine;
using System.Collections;
using GDGeek;


class VoxelBlow
{
	

	private VoxelMesh mesh_ = null;
	public VoxelMesh mesh {
		get{
			return mesh_;
		}
		set{
			mesh_ = value;
		}
	}



	private Task oneTask(Voxel vox){
		TaskSet task = new TaskSet ();
		TweenTask tt1 = new TweenTask(delegate {
			return TweenLocalPosition.Begin(vox.gameObject, Random.Range(0.3f, 0.5f), new Vector3(Random.Range(-100f, 100f),Random.Range(-100f, 100f),Random.Range(-100f, 100f)));
	
		});

		TaskManager.PushBack (tt1, delegate {
			vox.gameObject.SetActive(false);
		});
		task.push (tt1);
		return task;
	}
	public Task blow (){
		TaskSet ts = new TaskSet ();

		
		TaskManager.PushFront(ts, delegate{
			mesh_.showVox();
		});
		for (int i = 0; i< mesh_._datas.Length; ++i) {
			VoxelHandler handler = mesh_._datas[i];
			ts.push (oneTask (handler._vox));
		}


		return ts;
	}
}

