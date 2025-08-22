using System;
using UnityEngine;

namespace Enemy
{
    public class PatrolPoint:MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor = Color.green;
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}