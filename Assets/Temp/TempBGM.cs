using UnityEngine;
using System.Collections;

public class TempBGM : MonoBehaviour {

	public AudioSource _start;
	public AudioSource _play;
	public AudioSource _boss;
	public AudioSource _fuck;
	private AudioSource curr_ = null;
//	AudioSource _
	static private TempBGM instance_ = null;
	
	public static TempBGM GetInstance(){
		return TempBGM.instance_;
	}

	void Awake () {
		TempBGM.instance_ = this;

		
	}
	private void playIt(AudioSource source){
		if (curr_ != null && curr_.isPlaying) {
			curr_.Pause();
		}
		curr_ = source;
		curr_.Play();
	}
	public void start(){
		playIt (this._start);
	}


	public void fuck(){
		playIt (this._fuck);
	}


	public void boss(){
		playIt (this._boss);
	}


	public void play(){
		playIt (this._play);
	}



}
