using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace OuttaSpace.AI {
    [RequireComponent(typeof (PlatformerCharacter2D))]
	public class Platformer2DSimpleAIControl : MonoBehaviour {
		public PlatformerCharacter2D target;
		[SerializeField] private float hGravity = 3.0f;
		[SerializeField] private float hDeadZone = 0.1f;
		[SerializeField] private float targetRange = 1.0f;

		[SerializeField] private float targetYRange = 1.5f;

		[SerializeField] private Vector2 minAttackDist = new Vector2(1.5f, 0.5f);

		private float hInput;
		
		private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake() {
            m_Character = GetComponent<PlatformerCharacter2D>();
		}

		private void FixedUpdate() {
			if(target == null) {
				return;
			}

			var tPos = target.transform.position;
			var xDiff = tPos.x - transform.position.x;
			var yDiff = tPos.y -transform.position.y;
			var hStep = hGravity * Time.fixedDeltaTime;

			if(xDiff > targetRange) {
				hInput += hStep;
				m_Jump = yDiff >= targetYRange;
			}
			else if(xDiff < -targetRange) {
				hInput -= hStep;
				m_Jump = yDiff >= targetYRange;
			}
			else {
				if(hInput > hDeadZone){
					hInput -= hStep;
				}
				else if(hInput < -hDeadZone) {
					hInput += hStep;
				}
				else {
					hInput = 0.0f;
				}
			}

			if(minAttackDist.x >= Mathf.Abs(xDiff) && minAttackDist.y >= Mathf.Abs(yDiff) ) {
				m_Character.Attack();
			}

			hInput = Mathf.Clamp(hInput, -1.0f, 1.0f);

			m_Character.Move(hInput, false, m_Jump);
            m_Jump = false;
		}
	}
}