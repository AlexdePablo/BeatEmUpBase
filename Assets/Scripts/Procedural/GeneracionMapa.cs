using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneracionMapa : MonoBehaviour
{
    [SerializeField]
    VariablesTerreno m_VariablesTerreno;
    [SerializeField]
    ObjetosTerreno m_ObjetosTerreno;
    [SerializeField]
    public Tilemap tilemap;
    private List<Tile> m_LlistaObjectes;
    private void Awake()
    {
        /*   m_ObjetosTerreno.lista.Add(m_ObjetosTerreno.bosque);
           m_ObjetosTerreno.lista.Add(m_ObjetosTerreno.desierto);
           m_ObjetosTerreno.lista.Add(m_ObjetosTerreno.infierno);    */
        m_ObjetosTerreno.m_Objectes = m_ObjetosTerreno.bosque;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            StartCoroutine("GenTerrain");
    }
    public IEnumerator GenTerrain()
    {
        for (int y = 0; y < m_VariablesTerreno.sizeMap.y; y++)
        {
            yield return null;
            for (int x = 0; x < m_VariablesTerreno.sizeMap.x; x++)
            {
                float sample = CalculatePerlinNoise(x, y, m_VariablesTerreno.scale, m_VariablesTerreno.m_Frequency);
                /* if (sample >= m_ObjetosTerreno.m_ObjecteThreshold)
                 {
                     int indexObjecte = 0;
                 if (CalculatePerlinNoise(x, y, m_ObjetosTerreno.m_BiomesScale, m_ObjetosTerreno.m_BiomesFrequency) >= m_ObjetosTerreno.m_BiomaThreshold)
                 {
                     indexObjecte = 1;
                 }^*/
                //arbre.transform.position = new Vector3(x, m_Terrain.terrainData.GetHeight(x, y), y);
                tilemap.SetTile(new Vector3Int(x, y, 0), SelectTile(sample));
                //}
            }
        }
    }
    Tile SelectTile(float p)
    {
        //return m_Objectes[indexObjecte];
        for (int i = 0; i < m_VariablesTerreno.levels.Length; i++)
        {
            if (p < m_VariablesTerreno.levels[i]) return m_ObjetosTerreno.m_Objectes[i];
        }
        return null;
    }
    private float CalculatePerlinNoise(int x, int y, float scale, float frequency)
    {
        float xCoord = m_VariablesTerreno.Seed.x + (x / scale) * frequency;
        float yCoord = m_VariablesTerreno.Seed.y + (y / scale) * frequency;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
