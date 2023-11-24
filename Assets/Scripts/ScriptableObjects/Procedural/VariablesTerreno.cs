using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "VariablesTerreno", menuName = "ScriptableObjects/VariablesTerreno")]
public class VariablesTerreno : ScriptableObject
{
    [Header("Variables del terreno")]
    public Tile[] tiles;
    public float[] levels;
    public Vector2 sizeMap;
    public float scale;
    //Scale
    public float m_Frequency = 10f;
    //octaves
    public const int MAX_OCTAVES = 8;
    [Range(0, MAX_OCTAVES)]
    public int m_Octaves = 0;
    [Range(2, 3)]
    public int m_Lacunarity = 2;
    [Range(0.1f, 0.9f)]
    public float m_Persistence = 0.5f;
    public Vector2 Seed;
}
