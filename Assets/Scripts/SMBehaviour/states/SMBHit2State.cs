using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace BaseEmUp
{
    public class SMBHit2State : SMBComboState
    {
        public override void Init()
        {
            base.Init();
            m_Animator.Play("Spin");
        }

        //Podriem fer alguna cosa, però a aquest exemple només fem un combo
        protected override void OnComboFailedAction()
        {
            
        }

        protected override void OnComboSuccessAction()
        {
            m_StateMachine.ChangeState<SMBHit1State>();
        }

        protected override void OnEndAction()
        {
            m_StateMachine.ChangeState<SMBIdleState>();
        }
    }
}
