using UnityEngine;
using System.Collections;
using GDGeek;


namespace VoxelTrek{
	public class Buttle : MonoBehaviour {

		public MeshFilter _mesh = null;

		public delegate void Blow(Fly fly);
		private Vector3 direction_;

		public void setup (Color color, Vector3 direction)
		{
//			Debug.Log (direction);
			Mesh mesh = _mesh.mesh;
			//Vector3[] vertices = mesh.vertices;
			Color[] colors = mesh.colors;
			for (var i = 0; i < colors.Length; i++) {
				if(colors[i] != Color.white){
					colors [i] = color;
				}
			}
			mesh.colors = colors;

			direction_ = direction;
			if (direction.y > 0.0f) {
				this.gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
//				Debug.Log("A");
			} else {
//				Debug.Log("B" + direction.y);
				this.gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 180));
			}
		}


/*
		public void fire (Color color)
		{

			Mesh mesh = _mesh.mesh;
			Vector3[] vertices = mesh.vertices;
			Color[] colors = new Color[vertices.Length];
			for (var i = 0; i < vertices.Length; i++) {
				colors [i] = Color.Lerp (color, Color.white, vertices [i].y);
			}
		
			mesh.colors = colors;

			if (direction_.y > 0.0f) {
				this.gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				
			} else {
				this.gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			}
		
			this.gameObject.SetActive(true);

		}
		*/
		void Start () {
			Vector3 p = this.transform.position;
			p.y += 0.1f * Time.deltaTime * this.direction_.y;
			this.transform.position = p;
		}

		void Update () {
			Vector3 p = this.transform.position;
			p.y += 100 * Time.deltaTime * this.direction_.y;
			this.transform.position = p;
			if (direction_.y > 0.0f && p.y >200.0f) {
				this.gameObject.SetActive(false);		
			}else if(direction_.y < 0.0f && p.y < -200.0f){
				this.gameObject.SetActive(false);
			}


		}
		
		public void OnTriggerEnter( Collider other ){
//			Debug.Log (other.name + "!!!");
			other.gameObject.SendMessage ("toBeHurtCB", new Damage(), SendMessageOptions.DontRequireReceiver);
			this.gameObject.SetActive (false);
		}

	}
}