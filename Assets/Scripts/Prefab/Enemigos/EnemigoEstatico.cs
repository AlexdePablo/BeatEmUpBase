using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace BaseEmUp
{
    public class EnemigoEstatico : EnemyBasicScript
    {
        [SerializeField]
        LayerMask m_Layer;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            GetComponentInParent<EnemyBasicScript>().setStats += setDamageChildren;
        }

        private void setDamageChildren()
        {
            GetComponentInChildren<EnemigoGolpe>().SetDamage(damage);
        }

        // Start is called before the first frame update
        void Start()
        {
            m_EstadoActual = Estado.PATROLING;
            canHitPlayerByTime = true;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateState(m_EstadoActual);
        }
        protected override void Patroling()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, range, m_Layer);
            if (hit.collider != null)
            {
                player = hit.transform;
                ChangeState(Estado.PERSEGUIR);
            }
        }

        protected override void PrePatrolling()
        {
           ChangeState(Estado.PATROLING);
        }
    }
}