using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "ScriptableObjects/Spawner")]
    public class SpawnerScriptable : ScriptableObject
    {
        public int vidaMinima, vidaMaxima;
        public int da�oMinimo, da�oMaximo;
        public int velocitatMinima, velocitatMaxima;
        public int rangeMinim, rangeMaxim;
    }
}