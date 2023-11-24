using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace BaseEmUp
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_Instance;
        public static GameManager Instance
        {
            get { return m_Instance; }
        }


        private int m_RecordWave;

        public Action<int> onRecordWave;

        private void Awake()
        {
            if (m_Instance == null)
                m_Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(this);

            m_RecordWave = 0;

        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "InGame")
            {
                Spawner.onNextWave += subirWave;
                onRecordWave?.Invoke(m_RecordWave);
            }
            else
            {

            }
        }

        private void subirWave(int obj)
        {
            if (obj > m_RecordWave)
            {
                m_RecordWave = obj;
                onRecordWave?.Invoke(m_RecordWave);
            }
        }
    }
}