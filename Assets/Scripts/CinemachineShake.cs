using System;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake: MonoBehaviour
{
    public static CinemachineShake Instance{ get; private set; }
    private CinemachineCamera _cinemachineCamera;
    private float _shakeTimer;
    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void ShakeCamera(float intensity, float duration)
    {
        if (_cinemachineCamera != null)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
            _shakeTimer = duration;
        }
        else
        {
            Debug.LogWarning("CinemachineCamera component not found on this GameObject.");
        }
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
            {
                if (_cinemachineCamera != null)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                        _cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0f; // Reset shake intensity
                }
            }
        }
    }
}
