using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek{
	public class Rock : MonoBehaviour {
		//public 
		public int _blood = 8;//372
		public void setup (Vector3 begin, Vector3 end, int blood)
		{

			_blood = blood;
			begin_ = begin;
			end_ = end;
			this.gameObject.transform.position = begin_;
			this.gameObject.SetActive (true);
		}
		private float beginLength_ = 0.0f;
		private Vector3 begin_;
		private Vector3 end_;
		public Vector3 _rotate;
		public VoxelEmitter _emitter;
		public GameObject _offset;
		
		public VoxelMesh[] _meshs = null;
		void Start () {
			_rotate = new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), Random.Range (-100, 100));
		}
		public void toBeHurtCB(Damage damage){
			_rotate = new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), Random.Range (-100, 100));
			_meshs[_blood].gameObject.SetActive(false);	
			--_blood;
			if (_blood <0) {
				TempSound.GetInstance().boom();
//				AkSoundEngine.PostEvent ("Boon", this.gameObject);
				_emitter.emission ();
				this.gameObject.SetActive (false);
			} else {
				
				TempSound.GetInstance().hurt();
//				AkSoundEngine.PostEvent ("Hurt", this.gameObject);
				_meshs[_blood].gameObject.SetActive(true);	
			}
		}
		void doMove (float length){
			float l = length/10.0f - beginLength_/10.0f;
			
			this.gameObject.transform.position = begin_ + new Vector3 (0, -l * 100, 0);// +  new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), Random.Range (-100, 100));;
		}

		void OnEnable(){
			if (GameManager.GetInstance ()) {
				GameManager.GetInstance ().road.onMove += doMove;
				beginLength_ = GameManager.GetInstance ().road.length;
			}
			//_blood = Random.Range(0,8);
			//_blood = 8;
//			Debug.Log ("!" + _blood);
			for (int i =0; i<_meshs.Length; ++i) {
				_meshs[i].gameObject.SetActive(_blood == i);
			}
		}
		void OnDisable(){
			if (GameManager.GetInstance ()) {
				GameManager.GetInstance ().road.onMove -= doMove;
			}
		}
		// Update is called once per frame
		void Update () {
			_offset.gameObject.transform.Rotate (_rotate * Time.deltaTime);
			if (this.gameObject.transform.position.y < end_.y) {
				this.gameObject.SetActive(false);			
			}
		}

		
		public void OnTriggerEnter( Collider other ){
			if (other.gameObject.layer == 10) {
				Debug.Log (other.name + other.gameObject.layer);
				_emitter.emission ();
				this.gameObject.SetActive (false);
				other.gameObject.SendMessage("toBeHurtCB", new Damage(), SendMessageOptions.DontRequireReceiver);
			}

		}
	}
}