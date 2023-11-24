using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class generacionSalas : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Sala;
    [SerializeField]
    private GameObject m_Puerta;
    List<Vector3> posicionesSalas = new List<Vector3>();
    private float m_NumeroSalas;

    [SerializeField]
    private GameObject m_PuertaHorizontal;
    [SerializeField]
    private GameObject m_PuertaVertical;
    [SerializeField]
    private GameObject m_ParedHorizontal;
    [SerializeField]
    private GameObject m_ParedVertical;

    bool seAcabo;

    List<Vector3IntPair> ultimasSalas = new List<Vector3IntPair>();

    public struct Vector3IntPair
    {
        public Vector3 vector3Value;
        public int intValue;

        public Vector3IntPair(Vector3 vector3, int value)
        {
            vector3Value = vector3;
            intValue = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(comprobarSiSeHaAcabado());
        seAcabo = false;
        m_NumeroSalas = 24;
        InstanciarSala(Vector3.zero, HacerListaDePuertas());
    }
    /*
     * Añade posicion de la sala lista de salas, instancia la nueva sala y llama a generar sala con la posicion en la que has generado la nueva sala
     */
    private void InstanciarSala(Vector3 posicion, List<int> numerosDePuertas)
    {
        posicionesSalas.Add(posicion);

        GameObject salaInstanciada = Instantiate(m_Sala, posicion, Quaternion.identity);
        ColocarPuertas(numerosDePuertas, salaInstanciada);
    }

    private void ColocarPuertas(List<int> numerosDePuertas, GameObject salaInstanciada)
    {
   
        List<int> habitaciones = new List<int>();
        bool encontrao;

        ColocarPuerta(numerosDePuertas[0], salaInstanciada);
        
        
        for (int i = 1; i < 5; i++)
        {
            if (i == numerosDePuertas[0])
                continue;
            encontrao = EstaEnLaLista(numerosDePuertas, i);
            if (encontrao && m_NumeroSalas > 0)
            {
                m_NumeroSalas--;
                print(m_NumeroSalas);
                ColocarPuerta(i, salaInstanciada);
                habitaciones.Add(i);
            }
            else
            {
                ColocarPared(i, salaInstanciada);
            }
        }
        habitaciones.Shuffle();
        foreach (int habitacion in habitaciones)
        {
           
                ultimasSalas.Add(new Vector3IntPair( salaInstanciada.transform.position, habitacion));
        }
        foreach (int habitacion in habitaciones)
        {
            if (m_NumeroSalas > 0)
                GenerarSalas(habitacion, salaInstanciada.transform.position);
            else
                seAcabo = true;
        }
    }



    private void ColocarPuerta(int num, GameObject salaInstanciada)
    {
        GameObject puertaClone;
        Vector3 posicionPuerta;

        switch (num)
        {
           
            case 1:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x, salaInstanciada.transform.position.y + 4.5f, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_PuertaHorizontal, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 2:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x + 10, salaInstanciada.transform.position.y, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_PuertaVertical, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 3:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x, salaInstanciada.transform.position.y - 4.5f, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_PuertaHorizontal, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 4:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x - 10, salaInstanciada.transform.position.y, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_PuertaVertical, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            default:
                break;
        }
    }
    private void ColocarPared(int num, GameObject salaInstanciada)
    {
        GameObject puertaClone;
        Vector3 posicionPuerta;

        switch (num)
        {
            case 1:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x, salaInstanciada.transform.position.y + 4.5f, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_ParedHorizontal, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 2:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x + 10, salaInstanciada.transform.position.y, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_ParedVertical, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 3:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x, salaInstanciada.transform.position.y - 4.5f, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_ParedHorizontal, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            case 4:
                posicionPuerta = new Vector3(salaInstanciada.transform.position.x - 10, salaInstanciada.transform.position.y, salaInstanciada.transform.position.z);
                puertaClone = Instantiate(m_ParedVertical, posicionPuerta, Quaternion.identity);
                puertaClone.transform.parent = salaInstanciada.transform;
                break;
            default:
                break;
        }
    }
    private List<int> HacerListaDePuertas()
    {
        List<int> puertas = new List<int>();
        int numeroPuertas = Random.Range(1, 5);
        print("Numero de puertas: " + numeroPuertas);
        bool encontrado;

        do
        {
            int puerta = Random.Range(1, 5);
            encontrado = EstaEnLaLista(puertas, puerta);

            if (!encontrado)
            {
                puertas.Add(puerta);
                numeroPuertas--;
            }
        } while (numeroPuertas > 0);

        puertas.Sort();
        return puertas;
    }
    private List<int> HacerListaDePuertas(List<int> puertas)
    {
        int numeroPuertas = Random.Range(1, 4);
        print("Numero de puertas: " + numeroPuertas);
        bool encontrado;

        do
        {
            int puerta = Random.Range(1, 5);
            encontrado = EstaEnLaLista(puertas, puerta);

            if (!encontrado)
            {
                puertas.Add(puerta);
                numeroPuertas--;
            }
        } while (numeroPuertas > 0);

        puertas.Sort();
        return puertas;
    }

    private bool EstaEnLaLista(List<int> lista, int numeroEncontrar)
    {
        bool encontrado = false;

        foreach (int num in lista)
        {
            if (num == numeroEncontrar)
            {
                encontrado = true;
                break;
            }
        }

        return encontrado;
    }

    /*
     * 
     */
    private void GenerarSalas(int numeroPuertas, Vector3 posicionAnterior)
    {
        var numerosDePuerta = new List<int>();
        numerosDePuerta.Add(ConversionPuertas(numeroPuertas));
        Vector3 nuevaPosicionn = Vector3.zero;
        switch (numeroPuertas)
        {
            case 1:
                nuevaPosicionn = new Vector3(posicionAnterior.x, posicionAnterior.y + 9, posicionAnterior.z);
                break;
            case 2:
                nuevaPosicionn = new Vector3(posicionAnterior.x + 21, posicionAnterior.y, posicionAnterior.z);
                break;
            case 3:
                nuevaPosicionn = new Vector3(posicionAnterior.x, posicionAnterior.y - 9, posicionAnterior.z);
                break;
            case 4:
                nuevaPosicionn = new Vector3(posicionAnterior.x - 21, posicionAnterior.y, posicionAnterior.z);
                break;
        }
        if (!SalaOcupada(nuevaPosicionn))
        {
            InstanciarSala(nuevaPosicionn, HacerListaDePuertas(numerosDePuerta));
        }
    }
    /*
     * 
     */
    private bool SalaOcupada(Vector3 nuevaPosicionn)
    {
        bool haySala = false;
        for (int i = 0; i < posicionesSalas.Count; i++)
        {
            if (posicionesSalas[i] == nuevaPosicionn)
            {
                haySala = true;
                break;
            }
        }
        return haySala;
    }
    private int ConversionPuertas(int i)
    {
        switch (i)
        {
            case 1:
                return 3;
            case 2:
                return 4;
            case 3:
                return 1;
            case 4:
                return 2;
            default:
                return 0;
        }
    }
     private IEnumerator comprobarSiSeHaAcabado()
    {
        yield return new WaitForSeconds(1);

        if (seAcabo)
        {
            print("oquei");
            foreach(Vector3IntPair sala in ultimasSalas)
            {
                print(sala.vector3Value + " " + sala.intValue);
                //instanciaSalaFinal(sala.vector3Value, sala.intValue);
            }
            
        }else
        {
            seAcabo = false;
            m_NumeroSalas = 24;
            InstanciarSala(Vector3.zero, HacerListaDePuertas());
        }
    }

    private void instanciaSalaFinal(Vector3 vector3Value, int intValue)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
