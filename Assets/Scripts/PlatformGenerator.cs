using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class PlatformGenerator : MonoBehaviour
    {
        public GameObject platformPrefab; // Prefab de plataforma
        public int numberOfPlatforms = 2; // Número de plataformas iniciales
        public float levelWidth = 10f; // Ancho del área de generación
        public Transform player; // Referencia al jugador para determinar el rango de visión
        public float platformRemovalDistance = 10f; // Distancia a la que las plataformas se destruyen
        private Dictionary<int, GameObject> plataformas = new Dictionary<int, GameObject>();
        private int veces;
        private void Start()
        {
            StartCoroutine(AddPlataforma());
        }
        private void Update()
        {
            // Eliminar plataformas fuera del rango de visión del jugador

            List<int> plataformasParaEliminar = new List<int>();

            // Marcar las plataformas para eliminar
            foreach (KeyValuePair<int, GameObject> plataforma in plataformas)
            {
                if (platformRemovalDistance < Mathf.Abs(player.position.x - plataforma.Value.transform.position.x))
                {
                    plataformasParaEliminar.Add(plataforma.Key);
                }
            }

            // Eliminar las plataformas marcadas
            foreach (int key in plataformasParaEliminar)
            {
                GameObject plataforma = plataformas[key];
                plataformas.Remove(key);
                Destroy(plataforma);
            }
        }
        private IEnumerator AddPlataforma()
        {
            while (true)
            {
                Vector3 spawnPosition = new Vector3();
                veces = ((int)player.position.x / 30);

                if ((player.position.x % 15) > 7 || (player.position.x % 15) > -7)
                {
                    veces += 1;
                    if (comprobacionPlataforma(veces))
                    {
                        spawnPosition.x = 30 * veces;
                        spawnPosition.y = 1.3f;
                        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                        plataformas.Add(veces, platform);
                    }
                }
                if ((player.position.x % 15) < 7 || (player.position.x % 15) < -7)
                {
                    veces -= 1;
                    if (comprobacionPlataforma(veces))
                    {
                        spawnPosition.x = 30 * veces;
                        spawnPosition.y = 1.3f;
                        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                        plataformas.Add(veces, platform);
                    }
                }
                yield return new WaitForSeconds(1);
            }
        }
        private bool comprobacionPlataforma(int num)
        {
            foreach (int i in plataformas.Keys)
            {
                if (num == i)
                    return false;
            }
            return true;
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}