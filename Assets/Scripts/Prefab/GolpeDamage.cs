using BaseEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class GolpeDamage : MonoBehaviour
    {
        [SerializeField]
        private float m_Damage;
        private PlayerScript.Golpe m_Golpe;
        public PlayerScript.Golpe Golpe => m_Golpe;
        public float Damage => m_Damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                m_Golpe = GetComponentInParent<PlayerScript>().getTipoGolpe();
            }
        }
    }
}