using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BaseEmUp
{
    public class SMBIdleState : MBState
    {
        private PlayerScript m_PJ;
        private Rigidbody2D m_Rigidbody;
        private Animator m_Animator;
        private MBStateMachine m_StateMachine;
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
            m_Rigidbody.velocity = Vector2.zero;
            m_Animator.Play("Idle");
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
            if (m_PJ.MovementAction.ReadValue<Vector2>() != Vector2.zero)
                m_StateMachine.ChangeState<SMBWalkState>();

            RaycastHit2D HitSuelo = Physics2D.Raycast(transform.position, -transform.up, m_RCDetection, layerRayCastSalto);
            if (HitSuelo.collider == null)
                m_StateMachine.ChangeState<SMBJumpState>();
        }
    }
}
