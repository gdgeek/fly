using UnityEngine;
using System.Collections;
using GDGeek;
/*
public class BGM : MonoBehaviour {


	public AkEvent _play = null;
	public AkEvent _stop = null;
	static private BGM instance_ = null;

	public static BGM GetInstance(){
		return BGM.instance_;
	}

	private bool isPlay_ = false;



	// Update is called once per frame
	void Awake () {
		BGM.instance_ = this;
		_play.useOtherObject = true;
		_stop.useOtherObject = true;
		isPlay_ = false;

	}

	public void play(){
		_play.HandleEvent(this.gameObject);
		isPlay_ = true;
	}

	public void stop(){
		if (isPlay_) {
			isPlay_ = false;	
			
			_stop.HandleEvent(this.gameObject);
		}
	}
	public void endOfEvent(AkEventCallbackMsg in_info){

//		Debug.Log ("is Over!!");
		if (isPlay_) {
			_play.HandleEvent (this.gameObject);
		}
	}
}*/
