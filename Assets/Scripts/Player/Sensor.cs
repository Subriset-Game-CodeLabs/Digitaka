using System;
using UnityEngine;
using System.Collections;

namespace TwoDotFiveDimension
{
    public class Sensor : MonoBehaviour {

        private int m_ColCount = 0;
        [SerializeField] private string _gameObjectTag;
        private float m_DisableTimer;

        private void OnEnable()
        {
            m_ColCount = 0;
        }

        public bool State()
        {
            if (m_DisableTimer > 0)
                return false;
            return m_ColCount > 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!string.IsNullOrEmpty(_gameObjectTag))
            {
                if (other.CompareTag(_gameObjectTag))
                {
                    Debug.Log($"Sensor: {gameObject.name} - OnTriggerEnter: {other.tag}");
                    m_ColCount++;
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (!string.IsNullOrEmpty(_gameObjectTag))
            {
                if (other.CompareTag(_gameObjectTag))
                {
                    Debug.Log($"Sensor: {gameObject.name} - OnTriggerEnter: {other.tag}");
                    m_ColCount--;
                }
            }
        }


        void Update()
        {
            m_DisableTimer -= Time.deltaTime;
        }

        public void Disable(float duration)
        {
            m_DisableTimer = duration;
        }
    }

}
