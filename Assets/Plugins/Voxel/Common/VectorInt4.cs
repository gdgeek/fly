using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	[Serializable]
	public struct VectorInt4 {
		
		public VectorInt4(int x, int y, int z, int w)  
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		
		public VectorInt4(VectorInt4 v4)  
		{
			this.x = v4.x;
			this.y = v4.y;
			this.z = v4.z;
			this.w = v4.w;
		}
		
		public static VectorInt4 operator +(VectorInt4 lhs, VectorInt4 rhs)
		{
			VectorInt4 result = new VectorInt4(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;
			result.w += rhs.w;
			return result;
		}
		
		public static bool operator ==(VectorInt4 lhs, VectorInt4 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
		}
		
		
		public static bool operator !=(VectorInt4 lhs, VectorInt4 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
		}
		//	public VectorInt3()
		public int x;
		public int y;
		public int z;
		public int w;
	}
}