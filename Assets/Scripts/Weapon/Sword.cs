using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace OuttaSpace.Weapon {
	public class Sword : IWeapon {
		[SerializeField] HingeJoint2D joint;

		[SerializeField] float lowerAngle = 10.0f;
		[SerializeField] float upperAngle = 95.0f;

		[SerializeField] float returnForce = -100.0f;
		[SerializeField] float hitForce = 10000.0f;
		[SerializeField] float reloadTime = 0.1f;
		[SerializeField] float damage = 0.05f;
		[SerializeField] Transform swordTip;
		[SerializeField] Vector2 hitDirection = new Vector2(0.8f, 0.2f);

		bool facingRight = true;
		bool isFiring;

		public override bool IsReady {
			get { return !isFiring; }
		}

		public override void SetDirection(bool facingRight) {
			this.facingRight = facingRight;


			var motor = joint.motor;
			if(isFiring) {
				motor.motorSpeed = facingRight ? hitForce : -hitForce;
			}
			else {
				motor.motorSpeed = facingRight ? returnForce : -returnForce;
			}
			joint.motor = motor;
			
			var limits = joint.limits;
			limits.min = facingRight ? lowerAngle : -lowerAngle;
			limits.max = facingRight ? upperAngle : -upperAngle;
			joint.limits = limits;
		}

		// Use this for initialization
		public override void Fire () {
			isFiring = true;
			var motor = joint.motor;
			motor.motorSpeed = facingRight ? hitForce : -hitForce;
			joint.motor = motor;
			StartCoroutine(WaitForReload());
		}

		private IEnumerator WaitForReload() {
			yield return new WaitForSeconds(reloadTime);

			isFiring = false;
			var motor = joint.motor;
			motor.motorSpeed = facingRight ? returnForce : -returnForce;
			joint.motor = motor;
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if(!isFiring) {
				return;
			}

			var rigidbody = col.gameObject.GetComponent<PlatformerCharacter2D>();

			if(rigidbody != null) {
				// var direction = (col.transform.position - swordTip.position).normalized;
				var direction = facingRight ? hitDirection * hitForce: new Vector2(-hitDirection.x, hitDirection.y) * hitForce;
				// rigidbody.AddForce(direction * (hitForce * hitForceMultiplier));
				rigidbody.ReceiveDamage(damage, direction);
			}
		}

	}
}
