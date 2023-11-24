using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaMovimientoProcedural : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;

    [SerializeField] 
    private int m_Velocity;

    Camera miCamara;

    private bool canUpdateCamera;

    private void Awake()
    {
        m_Rigidbody2D= GetComponent<Rigidbody2D>(); 
        miCamara = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        canUpdateCamera = true;
        StartCoroutine(ComprovarCamara());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocidad = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
            velocidad += Vector2.right * -1;

        if (Input.GetKey(KeyCode.D))
            velocidad += Vector2.right;

        if (Input.GetKey(KeyCode.W))
            velocidad += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            velocidad += Vector2.up * -1;

        m_Rigidbody2D.velocity = velocidad.normalized * m_Velocity;
    }

    private IEnumerator DejarDeUpdatearCamara()
    {
        canUpdateCamera = false;
        yield return new WaitForSeconds(2);
    }

    private IEnumerator ComprovarCamara()
    {
        while (true)
        {
            if (canUpdateCamera)
                UpdateaCamara();

            yield return new WaitForSeconds(1);
        }


    }

    private void UpdateaCamara()
    {
        if (miCamara.WorldToViewportPoint(transform.position).x < 0)
        {
            miCamara.transform.position = new Vector3(miCamara.transform.position.x - 21, miCamara.transform.position.y, miCamara.transform.position.z);
            DejarDeUpdatearCamara();
        }
        if (miCamara.WorldToViewportPoint(transform.position).x > 1)
        {
            miCamara.transform.position = new Vector3(miCamara.transform.position.x + 21, miCamara.transform.position.y, miCamara.transform.position.z);
            DejarDeUpdatearCamara();
        }
        if (miCamara.WorldToViewportPoint(transform.position).y < 0)
        {
            miCamara.transform.position = new Vector3(miCamara.transform.position.x, miCamara.transform.position.y - 9, miCamara.transform.position.z);
            DejarDeUpdatearCamara();
        }
        if (miCamara.WorldToViewportPoint(transform.position).y > 1)
        {
            miCamara.transform.position = new Vector3(miCamara.transform.position.x, miCamara.transform.position.y + 9, miCamara.transform.position.z);
            DejarDeUpdatearCamara();
        }
    }
}
