using BaseEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class ComboHandler : MonoBehaviour
    {
        private bool m_ComboAvailable = false;
        public bool ComboAvailable => m_ComboAvailable;

        public Action OnEndAction;

        private void OnEnable()
        {
            m_ComboAvailable = false;
        }

        private void OnDisable()
        {
            m_ComboAvailable = false;
        }

        public void InitComboWindow()
        {
            m_ComboAvailable = true;
        }

        public void EndComboWindow()
        {
            m_ComboAvailable = false;
        }

        public void EndAction()
        {
            OnEndAction?.Invoke();
        }
    }
}