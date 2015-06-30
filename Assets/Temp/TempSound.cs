using UnityEngine;
using System.Collections;

public class TempSound : MonoBehaviour {
	
	public AudioSource _start;
	public AudioSource _fire;
	public AudioSource _boom;
	public AudioSource _hurt;
	public AudioSource _gameClose;
	public AudioSource _gameOver;
	public AudioSource _flyClose;
	public AudioSource _cameOut;


	private AudioSource curr_ = null;
	static private TempSound instance_ = null;
	
	public static TempSound GetInstance(){
		return TempSound.instance_;
	}
	
	void Awake () {
		TempSound.instance_ = this;
		
		
	}
	private void playIt(AudioSource source){

		//curr_ = source;
		source.Play();
	}
	
	public void hurt(){
		playIt (this._hurt);
	}

	public void start(){
		playIt (this._start);
	}
	
	
	public void fire(){
		playIt (this._fire);
	}
	
	public void comeIn(){
		playIt (this._cameOut);
	}

	public void boom(){
		playIt (this._boom);
	}
	

	public void cameOut(){
		playIt (this._cameOut);
	}/**/
}
