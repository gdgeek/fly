using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class Trigger : MonoBehaviour {
		
		private Gun gun_;
		public VoxelPool _buttlePool;
		

		public float _interval = 0.3f;
		/*public int _clip = 3;
		public float _renew = 0.6f;
		private int left_ = 3;
		*/

		private float time_ = 0.0f;
		



		public void doFire(){
			VoxelPoolObject obj =  _buttlePool.create ();
			Buttle buttle = obj.GetComponent<Buttle>();
			buttle.gameObject.layer = gun_.gameObject.layer;
			buttle.setup (gun_._color, gun_._director);
			buttle.gameObject.transform.position = this.gun_.transform.position;
			buttle.gameObject.SetActive (true);
			
			TempSound.GetInstance().fire();
//			Debug.Log(buttle.name+"!!!");
//			AkSoundEngine.PostEvent("Shoot", buttle.gameObject);
		}
		
		
		public void close ()
		{
			gun_ = null;
		}
		
		public void shooting (Gun gun)
		{

			gun_ = gun;

		}

		public void Update(){
			
			if (gun_ != null) {

				time_ += Time.deltaTime;
				if(time_ >= _interval){
					time_ -= _interval;
					this.doFire();
				}
			}
			
			
		}
		
		
	}
}