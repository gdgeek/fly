using UnityEngine;
using System.Collections;
using GDGeek;

public class TalkTest : MonoBehaviour {
	public VoxelTalkManager _talk = null;
	public VoxelTalkManager.Sentence[] _sentence;
	// Use this for initialization
	void Start () {
		TaskList tl = new TaskList ();
		for(int i = 0;i<_sentence.Length; ++i){
			tl.push (_talk.popTask (_sentence[i]));
		}
		TaskManager.Run (tl);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
