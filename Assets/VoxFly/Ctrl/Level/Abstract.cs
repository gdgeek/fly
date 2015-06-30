using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GDGeek;

namespace VoxelTrek
{
	namespace LevelMode{
		public abstract class Abstract : MonoBehaviour {

			public abstract void begin (float length, int chapters, int paragraph);
			public abstract void doMove (float length);
			public abstract bool isOver ();
			public abstract void end();
			public abstract void post ();
			//public abstract void clear();
		}
	}
}
