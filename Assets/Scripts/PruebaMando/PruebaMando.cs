using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PruebaMando : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    public InputActionAsset Input => m_Input;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(m_InputAsset);
        m_Input = Instantiate(m_InputAsset);
        m_Input.FindActionMap("NotDefault").Enable();

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 apuntandoElMando = m_Input.FindActionMap("NotDefault").FindAction("ApuntaMando").ReadValue<UnityEngine.Vector2>();
        float angulo = Mathf.Atan2(apuntandoElMando.y, apuntandoElMando.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new UnityEngine.Vector3(0,0, angulo);
    }
}
