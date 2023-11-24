using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Player")]
    public class PlayerStats : ScriptableObject
    {
        public float vida;
        public float damage;
        public float velocity;
        public float jumpAcceleration;
    }
}