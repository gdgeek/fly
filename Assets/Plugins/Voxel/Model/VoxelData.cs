using UnityEngine;
using System.Collections;
using System;

using Pathfinding.Serialization.JsonFx;
namespace GDGeek{
	[Serializable]
	[JsonOptIn]
	public class VoxelData{
		[JsonMember]
		public int x = 0;
		
		[JsonMember]
		public int y = 0;
		
		[JsonMember]
		public int z = 0;


		[JsonMember]
		public Color color = Color.red;
		
		[JsonMember]
		public int id = 0;



		

	}
}