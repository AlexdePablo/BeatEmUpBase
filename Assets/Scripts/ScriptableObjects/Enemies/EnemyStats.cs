using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemy")]
    public class EnemyStats : ScriptableObject
    {
        public float vida;
        public float damage;
        public float velocity;
        public float tiempoMareado;
        public float tiempoInestable;
        public float AtckSpeed;
        public float tiempoStunned;
        public float plusDamage;
        public float range;
    }
}