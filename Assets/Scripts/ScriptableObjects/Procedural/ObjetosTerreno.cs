using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ObjetosTerreno", menuName = "ScriptableObjects/ObjetosTerreno")]
public class ObjetosTerreno : ScriptableObject
{
    [Header("Variables de los objetos")]
    public List<Tile[]> lista;
    public Tile[] bosque;
    public Tile[] desierto;
    public Tile[] infierno;
    [Range(0f, 1f)]
    public float m_ObjecteThreshold = 0.9f;
    [Header("Biomes")]
    //Scale
    public float m_BiomesScale = 1000f;
    //Scale
    public float m_BiomesFrequency = 10f;
    [Range(0f, 1f)]
    public float m_BiomaThreshold = 0.5f;
    public Tile[] m_Objectes;
}
