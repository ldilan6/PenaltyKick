using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace PenaltyKick
{
    [RequireComponent(typeof(SoccerPlayerCharacter))]
    public class SoccerPlayerUserControl : MonoBehaviour
    {
        SoccerPlayerCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        Transform m_Cam;                  // A reference to the main camera in the scenes transform
        Vector3 m_CamForward;             // The current forward direction of the camera
        Vector3 m_Move;
        bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        bool m_Crouch;
        bool m_Kick;
        float kickPower;

        // Start is called before the first frame update
        void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<SoccerPlayerCharacter>();

        }

        // Update is called once per frame
        void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_Kick)
            {
                m_Kick = CrossPlatformInputManager.GetButtonDown("Fire1");
            }

        }

        // Fixed update is called in sync with physics
        void FixedUpdate()
        {
            float h, v;

            if (GameManager.Instance.UseJoystick)
            {
                h = GameManager.Instance.Joystick.Horizontal;
                v = GameManager.Instance.Joystick.Vertical;
            }
            else
            {
                // read inputs
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");
            }

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            if (!GameManager.Instance.UseJoystick)
            {
                // walk speed multiplier
                if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
            }

            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_Crouch, m_Jump, m_Kick, false, false);
            m_Jump = false;
            m_Kick = false;
            m_Crouch = false;
        }
    }
}
