using UnityEngine;
using System.Collections;
using System;
using Pathfinding.Serialization.JsonFx;


namespace GDGeek.WebVox
{
	[Serializable]
	[JsonOptIn]
	public class WebItem
	{
		[JsonMember]
		public string mesh;
		[JsonMember]
		public string title;
		[JsonMember]
		public string message;
		[JsonMember]
		public string iconUrl;
		[JsonMember]
		public int like;
		[JsonMember]
		public string postman;
	}

}