using BaseEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

namespace BaseEmUp
{
    [RequireComponent(typeof(MBStateMachine))]
    [RequireComponent(typeof(SMBIdleState))]
    [RequireComponent(typeof(SMBWalkState))]
    [RequireComponent(typeof(SMBHit1State))]
    [RequireComponent(typeof(SMBHit2State))]
    [RequireComponent(typeof(SMBJumpState))]


    public class PlayerScript : MonoBehaviour
    {

        public enum Golpe
        {
            NO_GOLPE, GOLPE_1, GOLPE_2
        }

        private MBStateMachine m_StateMachine;
        [SerializeField]
        private PlayerStats stats;

        [SerializeField]
        private InputActionAsset m_InputAsset;
        private InputActionAsset m_Input;
        public InputActionAsset Input => m_Input;
        private InputAction m_MovementAction;
        public InputAction MovementAction => m_MovementAction;
        [SerializeField]
        private float m_RCDetection = 1.1f;
        public float FloorDetection => m_RCDetection;
        [SerializeField]
        private float m_JumpForce = 5;
        public float JumpForce => m_JumpForce;
        [SerializeField]
        private LayerMask m_LayerRayCastSalto;
        public LayerMask LayerRayCastSalto => m_LayerRayCastSalto;

        public static Action<int> onDamage;
        public static Action<int> TotalVida;

        private float vida;

        private GolpeDamage m_GolpeDamage;

        private void Awake()
        {

            Assert.IsNotNull(m_InputAsset);
            m_StateMachine = GetComponent<MBStateMachine>();

            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("Default").FindAction("Movement");
            m_Input.FindActionMap("Default").Enable();
            m_GolpeDamage = GetComponentInChildren<GolpeDamage>();
        }

        private void Start()
        {
            m_StateMachine.ChangeState<SMBIdleState>();
            vida = stats.vida;
            TotalVida?.Invoke((int)vida);
            
        }
        public Golpe getTipoGolpe()
        {
            if (typeof(SMBHit1State).IsInstanceOfType(m_StateMachine.CurrentState))
                return Golpe.GOLPE_1;
            else if (typeof(SMBHit2State).IsInstanceOfType(m_StateMachine.CurrentState))
                return Golpe.GOLPE_2;
            else
                return Golpe.NO_GOLPE;
        }
        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyGolpe"))
            {
                EnemigoGolpe enemigo = collision.gameObject.GetComponent<EnemigoGolpe>();
                SetVida(enemigo.Damage);
            }
        }

        private void SetVida(float damage)
        {
            vida -= damage;
            onDamage?.Invoke((int)vida);
            
            if (vida <= 0)
                SceneManager.LoadScene("muerto");
        }
        private void OnDestroy()
        {
            m_Input.FindActionMap("Default").Disable();
        }
    }
}