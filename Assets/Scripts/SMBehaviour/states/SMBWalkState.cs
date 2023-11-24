using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BaseEmUp
{
    public class SMBWalkState : MBState
    {
        private PlayerScript m_PJ;
        private Rigidbody2D m_Rigidbody;
        private Animator m_Animator;
        private MBStateMachine m_StateMachine;

        private Vector2 m_Movement;

        [SerializeField]
        private float m_Speed = 3;
        private float m_JumpForce;
        private LayerMask layerRayCastSalto;
        private float m_RCDetection;
        private void Awake()
        {
            m_PJ = GetComponent<PlayerScript>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            m_StateMachine = GetComponent<MBStateMachine>();
            m_JumpForce = m_PJ.JumpForce;
            layerRayCastSalto = m_PJ.LayerRayCastSalto;
            m_RCDetection = m_PJ.FloorDetection;
        }

        public override void Init()
        {
            m_PJ.Input.FindActionMap("Default").FindAction("Atck1").started += OnAttack1;
            m_PJ.Input.FindActionMap("Default").FindAction("Atck2").started += OnAttack2;
            m_PJ.Input.FindActionMap("Default").FindAction("Jump").started += Jump;
            m_Animator.Play("Walk");
        }

        public override void Exit()
        {
            m_PJ.Input.FindActionMap("Default").FindAction("Atck1").started -= OnAttack1;
            m_PJ.Input.FindActionMap("Default").FindAction("Atck2").started -= OnAttack2;
            m_PJ.Input.FindActionMap("Default").FindAction("Jump").started -= Jump;
        }

        private void OnAttack1(InputAction.CallbackContext context)
        {
            m_StateMachine.ChangeState<SMBHit1State>();
        }
        private void OnAttack2(InputAction.CallbackContext context)
        {
            m_StateMachine.ChangeState<SMBHit2State>();
        }
        private void Jump(InputAction.CallbackContext context)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

        private void Update()
        {
            m_Movement = m_PJ.MovementAction.ReadValue<Vector2>();

            if (m_Movement.x < 0)
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            else if (m_Movement.x > 0)
                transform.rotation = Quaternion.identity;

            if (m_Movement ==  Vector2.zero)
                m_StateMachine.ChangeState<SMBIdleState>();

            RaycastHit2D HitSuelo = Physics2D.Raycast(transform.position, -transform.up, m_RCDetection, layerRayCastSalto);
            if (HitSuelo.collider == null)
                m_StateMachine.ChangeState<SMBJumpState>();
        }

        private void FixedUpdate()
        {
            m_Rigidbody.velocity = new Vector2(m_Movement.x * m_Speed, m_Rigidbody.velocity.y);
        }
    }
}
