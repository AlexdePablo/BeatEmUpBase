using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseEmUp
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(m_Player.position.x, transform.position.y, -10);
            if (transform.position.x < -28)
                transform.position = new Vector3(-28, transform.position.y, -10);
            if (transform.position.x > 28)
                transform.position = new Vector3(28, transform.position.y, -10);
        }
    }
}