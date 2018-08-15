using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace OuttaSpace.Weapon {
	public class Sword : IWeapon {
		
		[SerializeField] Animator anim;
		[SerializeField] float hitForce = 10000.0f;
		[SerializeField] float damage = 0.05f;
		[SerializeField] Transform swordTip;
		[SerializeField] Vector2 hitDirection = new Vector2(0.8f, 0.2f);

		private readonly int kFireTrigger = Animator.StringToHash("Fire");

		bool facingRight = true;
		bool isFiring;
		bool isReady = true;

		public override bool IsReady {
			get { return isReady; }
		}

		public override void SetDirection(bool facingRight) {
			this.facingRight = facingRight;
		}

		// Use this for initialization
		public override void Fire () {
			isFiring = true;
			isReady = false;

			anim.SetTrigger(kFireTrigger);
		}

		private void FinishedFiring() {
			isFiring = false;
		}

		private void FinishedReload() {
			isReady = true;
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
