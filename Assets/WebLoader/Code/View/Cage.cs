using UnityEngine;
using System.Collections;
namespace GDGeek.WebVox{
	public class Cage : MonoBehaviour {
		public AutoRound _round;
		private VoxelMesh mesh_ = null;
		//public Transform _offset = null;
		public void push(VoxelMesh mesh){
			if (mesh_ != null) {
				GameObject.DestroyImmediate(mesh_.gameObject);			
			}
			Debug.Log (mesh.max);
			Debug.Log (mesh.min);
			mesh_ = mesh;
			mesh_.gameObject.transform.parent = this.transform;
			float max = Mathf.Max (mesh.max.x, Mathf.Max (mesh.max.y, mesh.max.z));
			mesh_.transform.localScale = Vector3.one / (max+1);
			mesh_.transform.localPosition = Vector3.zero;
			mesh_.transform.localRotation = new Quaternion ();
			BoxCollider box = mesh.gameObject.GetComponent<BoxCollider> ();
			if (box != null) {
				box.enabled = false;			
			}
		}
	
		private TextMesh textMesh;
		private Vector3 deltaPosition;
		private Vector3 oldPosition;
		
		// Subscribe to events
		void OnEnable(){
			EasyTouch.On_Drag += On_Drag;
			EasyTouch.On_DragStart += On_DragStart;
			EasyTouch.On_DragEnd += On_DragEnd;
		}

		// At the drag beginning 
		void On_DragStart( Gesture gesture){
			_round.isEnabled = true;
			Debug.Log (gesture.pickObject);
			if (gesture.pickObject == gameObject){
				gameObject.GetComponent<Renderer>().material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));

				Vector3 position = gesture.GetTouchToWordlPoint(5);
				deltaPosition = position - transform.position;
			}	
		}

		void On_Drag(Gesture gesture){

			if (gesture.pickObject == gameObject){

				Vector3 position = gesture.GetTouchToWordlPoint(5);


//				float angle = gesture.GetSwipeOrDragAngle();
				Vector3 f = Vector3.Cross (deltaPosition.normalized, position.normalized);
				//f.x = -f.x;
				transform.localRotation = Quaternion.AngleAxis((deltaPosition - position).magnitude *30,  f);
			}
		}
		
		// At the drag end
		void On_DragEnd(Gesture gesture){
			if (gesture.pickObject == gameObject){
				this.transform.localRotation = this.transform.localRotation;
				gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
		}


	}

}