using UnityEngine;
using System.Collections;
using GDGeek;

namespace Transformers
{
	class Transformer
	{


		private VoxelData[] from_ = null;
		private VoxelData[] to_ = null;
		private VoxelMesh mesh_ = null;
		public VoxelMesh mesh {
			get{
				return mesh_;
			}
			set{
				mesh_ = value;
			}
		}

		public VoxelData[] to {
			get{
				return to_;
			}
			set{
				to_ = value;
			}

		}

		public VoxelData[] from {
			get{
				return from_;
			}
			set{
				from_ = value;
			}
		}
		private Task oneTask(Voxel vox, VoxelData to){
			TaskSet task = new TaskSet ();
			TweenTask tt1 = new TweenTask(delegate {
				return TweenVoxel.Begin(vox, 1.0f, to, new Vector3(-1, -4, -0.5f));
			});
			task.push (tt1);
			return task;
		}
		public Task transform (){
		//	return mesh_.transform (to_);
			int count = Mathf.Min (from_.Length, to_.Length);
			TaskSet tl = new TaskSet ();

			for (int i = 0; i< count; ++i) {
				VoxelHandler hander = mesh_.getVoxel(new VectorInt3(from_[i].x, from_[i].y, from_[i].z) );
				tl.push (oneTask (hander._vox, to_[i]));
			}
			TaskManager.PushFront(tl, delegate{
				mesh_.showVox();
			});
			if (to_.Length > count) {
				Debug.LogWarning(to_.Length);	
				for(int i = count; i<to_.Length; ++i){

					Debug.LogWarning(i);
				}
			}
			TaskManager.PushBack(tl, delegate{
				mesh_.clear();
			});
			return tl;
		}
	}

}