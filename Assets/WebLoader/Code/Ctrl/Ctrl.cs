using UnityEngine;
using System.Collections;
using System.IO;
using GDGeek;
using UnityEngine.UI;

namespace GDGeek.WebVox{
	public class Ctrl : MonoBehaviour {
		public void pushJson (string json)
		{
			json_ = json;
			this.fsmPost("load");
		}

		public void selectMesh (string mesh)
		{
			
			mesh_ = mesh;
			this.fsmPost("load");
		}

		public bool _isWebPlayer = false;
		public Text _text;
		public View _view;
		public Camera _camera;
		public Model _model = null;
		private FSM fsm_ = new FSM();
		private string mesh_ = null;
		private string json_ = null;
		public void fsmPost(string msg){
			this.fsm_.post (msg);		
		}
		public void loadMeshFromUrl(Item item){
			Debug.Log (item.mesh);
			mesh_ = item.mesh;
			this.fsmPost("load");
		}
		private Task logoTask ()
		{
			WebLoaderTask<WebMenuInfo> web = new WebLoaderTask<WebMenuInfo> ("http://gdgeek.com/www/index.php/voxel/info");
			web.onSucceed += delegate(WebMenuInfo info) {
				_model.web.menu = info.menu;
			};
			web.onError += delegate(string msg) {
				Debug.Log(msg);
			};
			//tl.push (web);
			TaskManager.PushBack (web, delegate {
				foreach(var item in _model.web.menu.list){

					_view.itemList.addItem (item.title, item.iconUrl, item.mesh, item.message, item.like, item.postman);
				}
			});
			return web;
		}
		
		private State logoState ()
		{
			StateWithEventMap state = TaskState.Create (delegate {
				return logoTask();
			}, fsm_, delegate {
				if(WebPlayer._parames.ContainsKey("mesh")){
					this.mesh_ = WebPlayer._parames["mesh"];
					return "load";
				}else{
					_view.createMesh (_model.def);
					this.mesh_ = null;
				}
				return "main";

		});
			state.onStart += delegate {
				_view.logo.gameObject.SetActive(true);
			};
			state.onOver += delegate {
				_view.logo.gameObject.SetActive(false);
			};
			return state;
		}
		
		private State mainState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			
			return state;
		}
		public Task loadTask(){
			TaskList tl = new TaskList ();
			if (!_isWebPlayer) {
				tl.push (_view._main.hideTask ());
			}
			/*
			
			WebLoaderTask<VoxelJsonInfo> web = new WebLoaderTask<VoxelJsonInfo> ("http://localhost:8888/gdgeek/1/www/index.php/voxel/read");

			web.onSucceed += delegate(VoxelJsonInfo info) {
				_model.voxel._data = VoxelReader.FromJsonData(info.data);
				_text.text = _model.voxel._data.ToString();
			};
			web.onError += delegate(string msg) {
				Debug.Log(msg);
			};

			
			
			tl.push (web);*/
			TaskManager.PushFront (tl, delegate {
				_text.text = "?????";
			});
			TaskManager.PushBack(tl, delegate{

				VoxelJsonInfo info = VoxelJsonInfo.Load(json_);
				_model.voxel._data = VoxelReader.FromJsonData(info.data);
				_view.createMesh (_model.voxel);
				this.mesh_ = null;
			});
			
			return tl;
		}
		State loadState ()
		{
			StateWithEventMap state = TaskState.Create (delegate {
				return loadTask();
			}, fsm_, "idle");
			return state;
		}
		
		State idleState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			state.addAction("load", "load");
			state.addAction("share", "share");

			return state;
		}

		State showState ()
		{
			StateWithEventMap state = new StateWithEventMap ();
			state.addAction("back", "unload");
			return state;
		}

		private Task unloadTask ()
		{

			return _view._main.showTask ();
		}

		private State unloadState ()
		{
			StateWithEventMap state = TaskState.Create (delegate {
				return unloadTask();
			}, fsm_, "idle");
			return state;
		}	
		private State  shareState ()
		{
			StateWithEventMap state = TaskState.Create (delegate {
				Task tsk = new Task();
				TaskManager.PushFront(tsk, delegate {
					string okCb = "GDGeekSendMessage(\"GDGeek\", \"message\", \"web_ok\");";
					string cancelCb = "GDGeekSendMessage(\"GDGeek\", \"message\", \"web_cancel\");";
					WebPlayer.Share("haha", "http://baidu.com", okCb, cancelCb);
				});
				return tsk;
			}, fsm_, "close_share");
			return state;
		}

		private State closeShare ()
		{
			StateWithEventMap state = TaskState.Create (delegate {
				TaskWait tsk = new TaskWait();
				tsk.setAllTime(3.0f);
				TaskManager.PushBack(tsk, delegate {
					WebPlayer.Close();
				});
				return tsk;
			}, fsm_, "idle");
			return state;
		}

				
		
		// Use this for initialization
		void Start () {
			
			fsm_.addState ("logo", logoState ()); 

			fsm_.addState ("main", mainState ());
			fsm_.addState ("idle", idleState (), "main");
			fsm_.addState ("load", loadState (), "main");
			fsm_.addState ("show", showState (), "main");
			fsm_.addState ("share", shareState(), "main");
			fsm_.addState ("close_share", closeShare(), "main");
			fsm_.addState ("unload", unloadState (), "main");
			fsm_.init ("logo");
		}
		

	}
}
