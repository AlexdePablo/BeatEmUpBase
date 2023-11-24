using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static BaseEmUp.EnemyBasicScript;

namespace BaseEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class EnemyBasicScript : MonoBehaviour
    {
        [Header("Estados")]
        protected Estado m_EstadoActual;
        public enum Estado
        {
            PATROLING, INESTABLE, MAREADO, ATACAR, STUNNED, PERSEGUIR, PRE_PATROLLING
        }
        public Estado EstadoActual => m_EstadoActual;
        protected float m_StateDeltaTime;
        protected bool canHitPlayerByTime;

        [Header("Stats Enemigo")]
        [SerializeField]
        protected EnemyStats m_Stats;
        protected float vida;
        protected float damage;
        protected float velocity;
        protected float range;
        protected float posicionInicialPatrulla;

        [Header("Componentes")]
        protected Animator m_Animator;
        protected Rigidbody2D m_Rigidbody;

        [Header("Player")]
        protected Transform player;

        [SerializeField]
        protected GameEvent eventoMorision;

        public Action setStats;
        private void Awake()
        {
            
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public void SetStats(float vida, float damage, float velocity, float range)
        {
            this.vida = vida;
            this.damage = damage;
            this.velocity = velocity;
            this.range = range;
            setStats?.Invoke();
        }
        protected void ChangeState(Estado newState)
        {
            if (newState == m_EstadoActual)
                return;

            ExitState(m_EstadoActual);
            InitState(newState);
        }
        protected void InitState(Estado initState)
        {
            m_EstadoActual = initState;
            m_StateDeltaTime = 0;
            //print(m_EstadoActual);
            switch (m_EstadoActual)
            {
                case Estado.PATROLING:
                    m_Animator.Play("Patroling");
                    posicionInicialPatrulla = transform.position.x;
                    break;
                case Estado.MAREADO:
                    m_Animator.Play("Mareado");
                    break;
                case Estado.INESTABLE:
                    m_Animator.Play("Inestable");
                    break;
                case Estado.ATACAR:
                    m_Animator.Play("Punyo");
                    break;
                case Estado.STUNNED:
                    m_Animator.Play("Stunned");
                    break;
                case Estado.PERSEGUIR:
                    m_Animator.Play("Walk");
                    break;
                case Estado.PRE_PATROLLING:
                    m_Animator.Play("PrePatroling");
                    break;
                default:
                    break;
            }
        }
        protected void UpdateState(Estado updateState)
        {
            m_StateDeltaTime += Time.deltaTime;

            switch (updateState)
            {
                case Estado.PATROLING:
                    Patroling();
                    break;
                case Estado.MAREADO:
                    break;
                case Estado.INESTABLE:
                    break;
                case Estado.ATACAR:
                    if (m_StateDeltaTime > 0.5)
                        ChangeState(Estado.PERSEGUIR);
                    break;
                case Estado.STUNNED:
                    break;
                case Estado.PERSEGUIR:
                    Perseguir();
                    break;
                case Estado.PRE_PATROLLING:
                    PrePatrolling();
                    break;
                default:
                    break;
            }
        }


        protected void ExitState(Estado exitState)
        {
            switch (exitState)
            {
                case Estado.PATROLING:
                    break;
                case Estado.MAREADO:
                    break;
                case Estado.INESTABLE:
                    break;
                case Estado.ATACAR:
                    StartCoroutine(CooldownHit());
                    break;
                case Estado.STUNNED:
                    break;
                case Estado.PERSEGUIR:
                    m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
                    break;
                case Estado.PRE_PATROLLING:
                    break;
                default:
                    break;
            }
        }
        protected abstract void PrePatrolling();
        protected abstract void Patroling();
        protected void Perseguir()
        {
            if(player.transform.position.x - transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
                m_Rigidbody.velocity = new Vector2(-velocity, m_Rigidbody.velocity.y);
            }
            else
            {
                transform.rotation = Quaternion.identity;
                m_Rigidbody.velocity = new Vector2(velocity, m_Rigidbody.velocity.y);
            }  
            if (Vector2.Distance(transform.position, player.transform.position) < m_Stats.range && canHitPlayerByTime)
                ChangeState(Estado.ATACAR);
            if (Vector2.Distance(transform.position, player.transform.position) < range && !canHitPlayerByTime)
                m_Rigidbody.velocity = Vector2.zero;
            if(Vector2.Distance(transform.position, player.transform.position) > range + 1)
                ChangeState(Estado.PRE_PATROLLING);
        }
        public void SetVida(float damage, PlayerScript.Golpe estadoGolpe)
        {
            switch (m_EstadoActual)
            {
                case Estado.PATROLING:
                case Estado.ATACAR:
                case Estado.PERSEGUIR:
                    PrimerGolpe(estadoGolpe, damage);
                    break;
                case Estado.INESTABLE:
                    SegundoGolpe(estadoGolpe, damage);
                    break;
                case Estado.MAREADO:
                    SegundoGolpe(estadoGolpe, damage);
                    break;
                case Estado.STUNNED:
                    vida -= damage * m_Stats.plusDamage;
                    break;
            }
            if (vida <= 0)
            {
                eventoMorision.Raise();
                Destroy(gameObject);
            }
        }
        // LE HA DE LLEGAR QUE GOLPE LE PEGA EL JUGADOR 1 O 2 NO EL ESTADO
        protected void PrimerGolpe(PlayerScript.Golpe nuevoEstado, float damage)
        {
            switch (nuevoEstado)
            {
                case PlayerScript.Golpe.GOLPE_1:
                    vida -= damage;
                    ChangeState(Estado.INESTABLE);
                    break;
                case PlayerScript.Golpe.GOLPE_2:
                    vida -= damage;
                    ChangeState(Estado.MAREADO);
                    break;
            }
        }
        protected void SegundoGolpe(PlayerScript.Golpe nuevoEstado, float damage)
        {
            switch (nuevoEstado)
            {
                case PlayerScript.Golpe.GOLPE_1:
                    if(EstadoActual == Estado.MAREADO)
                    {
                        vida -= damage * m_Stats.plusDamage;
                        ChangeState(Estado.PERSEGUIR);
                    }
                    else 
                        ChangeState(Estado.PERSEGUIR);
                    break;
                case PlayerScript.Golpe.GOLPE_2:
                    if (EstadoActual == Estado.INESTABLE)
                    {
                        vida -= damage;
                        ChangeState(Estado.STUNNED);
                    }
                    else
                        ChangeState(Estado.PERSEGUIR);
                    break;
            }
        }
        protected IEnumerator CooldownHit()
        {
            canHitPlayerByTime = !canHitPlayerByTime;
            yield return new WaitForSeconds(m_Stats.AtckSpeed);
            canHitPlayerByTime = !canHitPlayerByTime;
        }
        public void StopInestable()
        {
            ChangeState(Estado.PERSEGUIR);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerGolpe"))
            {
                GolpeDamage golpe = collision.gameObject.GetComponent<GolpeDamage>();

                if (player == null)
                    player = collision.gameObject.transform;

                SetVida(golpe.Damage, golpe.Golpe);
            }
        }
    }
}
