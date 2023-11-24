using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matrices : MonoBehaviour
{
    [SerializeField]
    private GameObject salaPocha;
    [SerializeField]
    private GameObject m_ParedHorizontal;
    [SerializeField]
    private GameObject m_ParedVertical;
    [SerializeField]
    private GameObject m_PuertaHorizontal;
    [SerializeField]
    private GameObject m_PuertaVertical;
    [SerializeField]
    private GameObject m_Sala;

    List<GameObject> listaDeSalas = new List<GameObject>();
    List<Vector3> listaDeSalasBuena = new List<Vector3>();

    private int[,] matrix = new int[41, 41];

    private int maximoSalas = 20;

    private int m_PosicionOriginal = 20;

    public struct Vector3IntInt
    {
        public Vector3 vector3Value;
        public int intX;
        public int intY;

        public Vector3IntInt(Vector3 vector3, int X, int Y)
        {
            vector3Value = vector3;
            intX = X;
            intY = Y;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RellenarMatriz();
        GeneracionMapa(20, 20);
        GenerarSalas();
        elegirTipoDeSala();
    }

    private void elegirTipoDeSala()
    {

        GameObject tipoSala = Instantiate(salaPocha);
        tipoSala.transform.position = listaDeSalasBuena[listaDeSalasBuena.Count - 1];
        tipoSala.GetComponent<SpriteRenderer>().color = Color.red;

        GameObject tipoSalaPrincipal = Instantiate(salaPocha);
        tipoSalaPrincipal.transform.position = listaDeSalasBuena[0];
        tipoSalaPrincipal.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void GenerarSalas()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row,col] == 1)
                    instanciarSala(row,col);
            }
        }
    }

    private void instanciarSala(int row, int col)
    {
        int posicionX = ((m_PosicionOriginal - col) * 21);
        int posicionY = ((m_PosicionOriginal - row) * 9);
        GameObject sala = Instantiate(m_Sala);
        sala.transform.position = new Vector3(posicionX, posicionY, 0);
        ColocarPuertas(sala, row, col);
    }

    private void ColocarPuertas(GameObject sala, int row, int col)
    {
        //ARRIBA
        if (matrix[row - 1, col] == 1)
        {
            GameObject puerta = Instantiate(m_PuertaHorizontal);
            puerta.transform.position = new Vector3(sala.transform.position.x, sala.transform.position.y + 4.5f, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        else
        {
            GameObject puerta = Instantiate(m_ParedHorizontal);
            puerta.transform.position = new Vector3(sala.transform.position.x, sala.transform.position.y + 4.5f, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        //DERECHA
        if (matrix[row, col - 1] == 1)
        {
            GameObject puerta = Instantiate(m_PuertaVertical);
            puerta.transform.position = new Vector3(sala.transform.position.x + 10, sala.transform.position.y, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        else
        {
            GameObject puerta = Instantiate(m_ParedVertical);
            puerta.transform.position = new Vector3(sala.transform.position.x + 10, sala.transform.position.y, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        //ABAJO
        if (matrix[row + 1,col] == 1)
        {
            GameObject puerta = Instantiate(m_PuertaHorizontal);
            puerta.transform.position = new Vector3(sala.transform.position.x, sala.transform.position.y - 4.5f, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        else
        {
            GameObject puerta = Instantiate(m_ParedHorizontal);
            puerta.transform.position = new Vector3(sala.transform.position.x, sala.transform.position.y - 4.5f, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        //IZQUIERDA
        if (matrix[row , col + 1] == 1)
        {
            GameObject puerta = Instantiate(m_PuertaVertical);
            puerta.transform.position = new Vector3(sala.transform.position.x - 10, sala.transform.position.y, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
        else
        {
            GameObject puerta = Instantiate(m_ParedVertical);
            puerta.transform.position = new Vector3(sala.transform.position.x - 10, sala.transform.position.y, sala.transform.position.z);
            puerta.transform.parent = sala.transform;
        }
    }

    private void GeneracionMapa(int row, int col)
    {
        matrix[row, col] = 1;
        maximoSalas--;
        listaDeSalasBuena.Add(new Vector3(((m_PosicionOriginal - col) * 21), ((m_PosicionOriginal - row) * 9), 0));
        List<int> SalasAlrededor;
        //Seleccionar las posibles salas
        GetSalasAlrededor(out SalasAlrededor);
        CambiarMatriz(SalasAlrededor, row, col);
    }

    private void CambiarMatriz(List<int> salasAlrededor, int row, int col)
    {
        foreach (int sala in salasAlrededor)
        {
            if (maximoSalas > 0)
            {
                switch (sala)
                {
                    case 1:
                        ponerSalaEnUno(row,col-1);
                        break;
                    case 2:
                        ponerSalaEnUno(row + 1, col);
                        break;
                    case 3:
                        ponerSalaEnUno(row, col + 1);
                        break;
                    case 4:
                        ponerSalaEnUno(row -1, col);
                        break;
                }
            }
        }
    }

    private void ponerSalaEnUno(int row, int col)
    {
        if (matrix[row, col] == 1)
            return;

        else
        {
            GeneracionMapa(row, col);
        }
    }

    private void GetSalasAlrededor(out List<int> puertas)
    {
        puertas = new List<int>();
        int numeroPuertas = Random.Range(1, 5);
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

        puertas.Shuffle();
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

    //Rellenar la matriz de ceros
    private void RellenarMatriz()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = 0;
            }
        }
    }
}
