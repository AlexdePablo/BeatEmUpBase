using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace BaseEmUp
{
    public class MBStateMachine : MonoBehaviour
    {
        MBState[] m_States;

        MBState m_CurrentState = null;

        public MBState CurrentState => m_CurrentState;

        private void Awake()
        {
            m_States = GetComponents<MBState>();

            foreach (MBState state in m_States)
                state.enabled = false;
        }

        public T GetState<T>() where T : MBState
        {
            return m_States.First(state => state.GetType() == typeof(T)) as T;
        }

        public void ChangeState<T>() where T : MBState
        {
            T state = GetState<T>();
            Assert.IsNotNull(state);

            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
                m_CurrentState.enabled = false;
            }

            m_CurrentState = state;
            m_CurrentState.Init();
            m_CurrentState.enabled = true;
        }
    }
}
