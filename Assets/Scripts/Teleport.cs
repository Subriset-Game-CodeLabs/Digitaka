using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Teleport:MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string _targetScene = "MainMenu";
    private CapsuleCollider _capsuleCollider;
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            SceneManager.Instance.ChangeScene(_targetScene);
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
        }
    }
}
