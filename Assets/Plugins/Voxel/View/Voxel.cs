using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class Voxel : MonoBehaviour {

		public VoxelProperty property {
			get{
				VoxelProperty prop = new VoxelProperty();
				prop.color = this.color;
				prop.scale = this.transform.localScale;
				prop.position = this.transform.position;
				return prop;
			}
			set{
				VoxelProperty prop = value;
				this.color = prop.color;
				this.transform.position = prop.position;
				this.transform.localScale = prop.scale;
			}
		}

		public int _id = 0;
		public Color _color;
		public Color color {
			get{
				return _color;
			}
			set{
				
				this._color = value;
				if(Application.isPlaying)
				{
					this.GetComponent<Renderer>().material.color = this._color;
				}
			}
		}
		public void Awake(){
			this.GetComponent<Renderer>().material.color = this._color;
		}
		/*
		public int back_ = 0;
		public int front_ = 0;
		public int down_ = 0;
		public int up_ = 0;
		public int left_ = 0;
		public int right_ = 0;


		private int lback_ = 0;
		private int lfront_ = 0;
		private int ldown_ = 0;
		private int lup_ = 0;
		private int lleft_ = 0;
		private int lright_ = 0;
		public int right{
			get{
				return right_;
			}
		}
		public int left{
			get{
				return left_;
			}
		}
		public int up{
			get{
				return up_;
			}
		}
		public int down{
			get{
				return down_;
			}
		}
		public int back{
			get{
				return back_;
			}
		}
		public int front{
			get{
				return front_;
			}
		}




		public int lright{
			get{
				return lright_;
			}
		}
		public int lleft{
			get{
				return lleft_;
			}
		}
		public int lup{
			get{
				return lup_;
			}
		}
		public int ldown{
			get{
				return ldown_;
			}
		}
		public int lback{
			get{
				return lback_;
			}
		}
		public int lfront{
			get{
				return lfront_;
			}
		}
		*/
		/*private Vector3 vPosition_ = Vector3.zero;
		public Vector3 vPosition{
			get{
				return vPosition_;
			}
		}
*/

		public void setup (VectorInt3 position, Color color, int id)
		{
			this.gameObject.SetActive (true);
			//Vector3 pos = new Vector3(position.x, position.y, -position.z);
			//pos.z = -pos.z;
			this.transform.localPosition = new Vector3(position.x, position.y, -position.z);
			this.color = color;
			this._id = id;
		}

		/*
		public void lightShadowBegin(){
			back_ = 0;
			up_ = 0;
			front_ = 0;
			down_ = 0;
			left_ = 0;
			right_ = 0;

			lback_ = 0;
			lup_ = 0;
			lfront_ = 0;
			ldown_ = 0;
			lleft_ = 0;
			lright_ = 0;
		}

		public void lightAdd (byte i, Vector3 face)
		{
			if( face == Vector3.back){
				lback_ |= 0x1 << i;
			}if( face == Vector3.forward){
				lfront_ |= 0x1 << i;
			}else if(face == Vector3.up){
				lup_ |= 0x1 <<i;
				
			}else if(face == Vector3.down){
				ldown_ |= 0x1 <<i;
				
			}else if(face == Vector3.left){
				lleft_ |= 0x1 <<i;
				
			}else if(face == Vector3.right){
				lright_ |= 0x1 <<i;
				
			}

		}
		public void shadowAdd(byte i, Vector3 face){
			if( face == Vector3.back){
				back_ |= 0x1 << i;
			}if( face == Vector3.forward){
				front_ |= 0x1 << i;
			}else if(face == Vector3.up){
				up_ |= 0x1 <<i;
				
			}else if(face == Vector3.down){
				down_ |= 0x1 <<i;
				
			}else if(face == Vector3.left){
				left_ |= 0x1 <<i;
				
			}else if(face == Vector3.right){
				right_ |= 0x1 <<i;
				
			}


			
		
		}
		public static Vector2 GetUV(int i){
			
			float unit = 1.0f / 16f;
			Vector2 uv = new Vector2 ();
			uv.x = (i&0xf) * unit;
			uv.y = 1.0f - unit *(((i >> 0x4)&0xf) +1);
			return uv;
				
		}
		public void lightShadowEnd(){


		}

		*/

	}
}