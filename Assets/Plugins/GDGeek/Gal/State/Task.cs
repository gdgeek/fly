using UnityEngine;
using System.Collections;
using System;
namespace GDGeek.Gal{
	public abstract class TaskFactory : MonoBehaviour {

		abstract public Task create();
	}

}
