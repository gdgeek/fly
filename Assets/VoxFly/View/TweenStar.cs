using UnityEngine;
using System.Collections;
using GDGeek;
namespace VoxelTrek{
	public class TweenStar : Tween
	{
		
		private Color from_;
		private Color to_;
		private MeshRenderer render_;
		override protected void OnUpdate (float factor, bool isFinished)
		{   
			float f = Mathf.PingPong (factor * 2, 1.0f);
			render_.material.color = from_ * (1f - f) + to_ * f;
		}
		
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		//13801889517 zhou ping
		static public TweenStar Begin (MeshRenderer render, float duration, Color to)
		{
			TweenStar comp = Tween.Begin<TweenStar>(render.gameObject, duration);
			comp.render_ = render;
			comp.from_ = render.material.color;
			comp.to_ = to;
		
			return comp;
		}
	}
	
	
}