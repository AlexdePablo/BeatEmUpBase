using BaseEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaseEmUp
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_enemyPatrol;
        [SerializeField]
        private GameObject m_enemyEstatic;
        [SerializeField]
        private SpawnerScriptable SOSpawner;

        public static Action<int> onNextWave;

        private int enemigosPorWave = 0;
        private int enemigosPorWaveCount;
        private int enemigosMoridos;
        private int numeroDeWave = 0;

        [SerializeField]
        private Transform[] m_SpawnPoints;


        private void Awake()
        {

        }
        // Start is called before the first frame update
        void Start()
        {
            SubirWave();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SubirWave()
        {
            numeroDeWave++;
            enemigosMoridos = 0;
            enemigosPorWave = numeroDeWave * 2;
            enemigosPorWaveCount = enemigosPorWave;
            onNextWave?.Invoke(numeroDeWave);
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (enemigosPorWave > 0)
            {
                GameObject enemigo;
                if (numeroDeWave == 1)
                    enemigo = Instantiate(m_enemyEstatic);
                else if (numeroDeWave == 2)
                    enemigo = Instantiate(m_enemyPatrol);
                else
                {
                    int num = Random.Range(0, 2);

                    if (num == 1)
                        enemigo = Instantiate(m_enemyEstatic);
                    else
                        enemigo = Instantiate(m_enemyPatrol);

                }
                enemigo.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
                SetStats(enemigo);
                enemigosPorWave--;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void SetStats(GameObject enemigo)
        {
            enemigo.GetComponent<EnemyBasicScript>().SetStats(Random.Range(SOSpawner.vidaMinima, SOSpawner.vidaMaxima + 1), Random.Range(SOSpawner.dañoMinimo, SOSpawner.dañoMaximo + 1), Random.Range(SOSpawner.velocitatMinima, SOSpawner.velocitatMaxima + 1), Random.Range(SOSpawner.rangeMinim, SOSpawner.rangeMaxim + 1));
        }

        public void CountEnemigosMoridos()
        {
            enemigosMoridos++;
            if (enemigosMoridos == enemigosPorWaveCount)
                SubirWave();
        }
    }
}