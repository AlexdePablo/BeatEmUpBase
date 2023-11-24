using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class EnemigoGolpe : MonoBehaviour
    {
        private float m_Damage;
        public float Damage => m_Damage;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetDamage(float damage)
        {
            m_Damage = damage;
        }
    }
}