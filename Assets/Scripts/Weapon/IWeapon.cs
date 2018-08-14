using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OuttaSpace.Weapon {
	public abstract class IWeapon : MonoBehaviour {
		public abstract bool IsReady { get; }
		public abstract void SetDirection(bool facingRight);
		public abstract void Fire();
	}
}
