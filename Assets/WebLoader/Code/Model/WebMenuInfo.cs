using UnityEngine;
using System.Collections;
using System;
using Pathfinding.Serialization.JsonFx;

namespace GDGeek.WebVox{

	[Serializable]
	public class WebMenuInfo : DataInfo {
		[JsonMember]
		public WebMenu menu = null; 
	
		public static string Save(WebMenuInfo info){
			string json = JsonDataHandler.write<WebMenuInfo>(info);
			return json;
			
		}
		public static WebMenuInfo Load(string json){
			WebMenuInfo info = JsonDataHandler.reader<WebMenuInfo>(json);
			return info;
		}


	}

}