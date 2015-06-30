using UnityEngine;
using System.Collections;

namespace GDGeek.WebVox
{
	public class Main : MonoBehaviour
	{
		public Task hideTask(){
			TweenTask task = new TweenTask (delegate {
				return TweenLocalPosition.Begin(this.gameObject, 0.3f, new Vector3(-640, 0, 0));
			});
			return task;
		}

		public Task showTask(){
			TweenTask task = new TweenTask (delegate {
				return TweenLocalPosition.Begin(this.gameObject, 0.3f, new Vector3(0, 0, 0));
			});
			return task;
		}
	}

}
