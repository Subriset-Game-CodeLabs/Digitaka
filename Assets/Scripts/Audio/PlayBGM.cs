using System;
using UnityEngine;

namespace Audio
{
    // [RequireComponent(typeof(AudioSource))]
    public class PlayBGM:MonoBehaviour
    {
        private AudioManager _audioManager;
        private AudioSource _audioSource;
        [SerializeField] private SoundType _soundType;
        private void Start()
        {
            BGMManager.Instance.PlayBGM(_soundType);
        }
    }
}