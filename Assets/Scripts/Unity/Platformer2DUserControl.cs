using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField] private string playerId = "P1";
        private string jumpInput;
        private string horizontalInput;
        private string fireInput;

        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();

            jumpInput = string.Format("{0}Jump", playerId);
            horizontalInput = string.Format("{0}Horizontal", playerId);
            fireInput = string.Format("{0}Fire", playerId);
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = Input.GetButtonDown(jumpInput);
            }

            if(Input.GetAxis(fireInput) > 0.1f) {
                m_Character.Attack();
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis(horizontalInput);
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
