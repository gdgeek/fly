using UnityEngine;
using System.Collections;

public class AutoRound : MonoBehaviour {
	private bool _isEnabled = true;
	public bool isEnabled {

		set{

			_isEnabled = value;
			if(_isEnabled){
				a_ = 0.0f;
			}
		}
	}

	private float a_ = 0;

	// Update is called once per frame
	void Update () {
		if (_isEnabled) {
				a_ += Time.deltaTime * -10.0f;
				this.transform.localRotation = Quaternion.Euler (new Vector3 (0, a_, 0));
		}
	}
}
