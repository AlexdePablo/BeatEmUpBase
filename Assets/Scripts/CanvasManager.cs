using BaseEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BaseEmUp
{
    public class CanvasManager : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI textoRecord;
        [SerializeField]
        private TextMeshProUGUI textoWave;
        [SerializeField]
        private Slider sliderVida;
        private void Awake()
        {
            GameManager.Instance.onRecordWave += UpdateRecord;
            Spawner.onNextWave += UpdateWave;
            PlayerScript.onDamage += UpdateVida;
            PlayerScript.TotalVida += SetVida;
        }

        private void SetVida(int obj)
        {
            sliderVida.maxValue = obj;
        }

        private void UpdateVida(int obj)
        {
            sliderVida.value = obj;
        }

        private void UpdateRecord(int obj)
        {
            textoRecord.text = "Record: " + obj;
        }

        private void UpdateWave(int obj)
        {
            textoWave.text = "Wave actual: " + obj;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDestroy()
        {
            GameManager.Instance.onRecordWave -= UpdateRecord;
            Spawner.onNextWave -= UpdateWave;
            PlayerScript.onDamage -= UpdateVida;
            PlayerScript.TotalVida -= SetVida;
        }
    }
}