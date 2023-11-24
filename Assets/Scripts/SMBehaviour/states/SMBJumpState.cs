using BaseEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class SMBJumpState : MBState
    {
        private PlayerScript m_PJ;
        private Rigidbody2D m_Rigidbody;
        private Animator m_Animator;
        private MBStateMachine m_StateMachine;

        private Vector2 m_Movement;

        [SerializeField]
        private float m_Speed = 3;
        private LayerMask layerRayCastSalto;
        private float m_RCDetection;
        private void Awake()
        {
            m_PJ = GetComponent<PlayerScript>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            m_StateMachine = GetComponent<MBStateMachine>();
            layerRayCastSalto = m_PJ.LayerRayCastSalto;
            m_RCDetection = m_PJ.FloorDetection;
        }

        public override void Exit()
        {

        }

        public override void Init()
        {
            m_Animator.Play("Jump");
        }

        void Update()
        {
            m_Movement = m_PJ.MovementAction.ReadValue<Vector2>();

            if (m_Movement.x < 0)
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            else if (m_Movement.x > 0)
                transform.rotation = Quaternion.identity;

            RaycastHit2D HitSuelo = Physics2D.Raycast(transform.position, -transform.up, m_RCDetection, layerRayCastSalto);
            if (HitSuelo.collider != null)
                m_StateMachine.ChangeState<SMBIdleState>();
        }
        private void FixedUpdate()
        {
            m_Rigidbody.velocity = new Vector2(m_Movement.x * m_Speed, m_Rigidbody.velocity.y);
        }
    }
}