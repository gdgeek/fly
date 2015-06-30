using UnityEngine;
using System.Collections;
namespace GDGeek.WebVox
{
	public class View : MonoBehaviour {


		public VoxelManager _manager;
		public Logo _logo = null;
		public Cage _cage = null;
		public Main _main = null;
		public ItemList _itemList = null;

		public ItemList itemList{
			get{
				return _itemList;
			}
		}
		public Logo logo {
			get{
				return _logo;
			}
		}

		public VoxelMesh createMesh(VoxelModel model){
			VoxelMesh mesh =  _manager.create (model);
			_cage.push (mesh);
			return mesh;

		}

		public Task menuHideTask ()
		{
			throw new System.NotImplementedException ();
		}
	}
}
