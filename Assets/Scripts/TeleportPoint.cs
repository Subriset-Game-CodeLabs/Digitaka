using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TeleportPoint:MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string _targetScene = "MainMenu";
    [SerializeField] private bool _starterPoint = false;
    [SerializeField] private bool _endScene = false;
    public bool StarterPoint
    {
        get => _starterPoint; 
        set => _starterPoint = value;
    }

    public string TargetScene
    {
        get => _targetScene;
        private set => _targetScene = value;
    }
    private CapsuleCollider _capsuleCollider;
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player") && !_starterPoint && !_endScene)
        {
            TeleportManager.Instance.TeleportToScene(_targetScene, _targetScene);
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            _starterPoint = false;
        }
    }
    
}
