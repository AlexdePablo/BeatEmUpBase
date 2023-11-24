using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class EnemigoPatrullador : EnemyBasicScript
    {
        [SerializeField]
        LayerMask m_Layer;
        [SerializeField]
        private GameObject m_Chaqueta;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            m_EstadoActual = Estado.PRE_PATROLLING;
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
            m_Rigidbody.velocity = transform.right * velocity;
            if (transform.position.x < posicionInicialPatrulla - 3)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (transform.position.x > posicionInicialPatrulla + 3)
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            }
               
        }

        protected override void PrePatrolling()
        {
           if(m_StateDeltaTime > 2)
            {
                m_Rigidbody.velocity = new Vector2(velocity, m_Rigidbody.velocity.y);
                ChangeState(Estado.PATROLING);
            }
        }
        public void Disparar()
        {
            GameObject chaquetilla = Instantiate(m_Chaqueta);
            chaquetilla.GetComponent<EnemigoGolpe>().SetDamage(damage);
            chaquetilla.transform.position = transform.position;
            chaquetilla.GetComponent<Rigidbody2D>().velocity = new Vector2(getVx(), getVy());
        }

        private float getVx()
        {
            return (player.position.x - transform.position.x) / Mathf.Cos(45 * 2 * Mathf.PI / 360);
        }

        private float getVy()
        {
            return (1 - transform.position.y - -9.81f/2) / Mathf.Sin(45 * 2 * Mathf.PI / 360);
        }

    }
}