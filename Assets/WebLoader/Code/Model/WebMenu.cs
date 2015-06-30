using UnityEngine;
using System.Collections;
using System;
using Pathfinding.Serialization.JsonFx;


namespace GDGeek.WebVox
{
	[Serializable]
	[JsonOptIn]
	public class WebMenu
	{
		[JsonMember]
		public string loadUrl = "";
		[JsonMember]
		public WebItem[] list = null;

	}


}