using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using TwoDotFiveDimension;

namespace Enemy
{
    public class EnemyAttack:MonoBehaviour
    {
        [field: SerializeField] public Collider hitBox { get; private set; }
        [field: SerializeField] public GameObject hitEffect { get; private set; }
        [field:SerializeField] public GameObject prefabDamagePopup { get; private set; }
        private EnemyStats _enemyStats;
        private List<Collider> _collidersDamaged;
        private Animator _animator;
        private void Start()
        {
            _collidersDamaged = new List<Collider>();
            _enemyStats = GetComponent<EnemyStats>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_animator.GetFloat("Weapon.Active") > 0f)
            {
                Attack();
            }
            else
            {
                _collidersDamaged.Clear();
            }
        }

        private void Attack()
        {
            Collider[] collidersToDamage = Physics.OverlapBox(
                hitBox.bounds.center,
                hitBox.bounds.extents,
                hitBox.transform.rotation,
                LayerMask.GetMask("Player"), 
                QueryTriggerInteraction.Collide
            );
            foreach (Collider collider in collidersToDamage)
            {
                if (!_collidersDamaged.Contains(collider)){
                    PlayerStats hitPlayer = collider.GetComponentInChildren<PlayerStats>();
                    if (hitPlayer && hitPlayer.IsAlive)
                    {
                        Vector3 attackDir = (hitPlayer.transform.position - gameObject.transform.position);
                        DamagePopup.Create(
                            prefabDamagePopup.transform,
                            hitPlayer.gameObject.transform.position, 
                            attackDir,
                            _enemyStats.damage, 
                            _enemyStats.damage>5);
                        Instantiate(hitEffect, collider.transform);
                        AudioManager.Instance.PlaySound(SoundType.SFX_PlayerGetHit);
                        hitPlayer.TakeDamage(_enemyStats.damage);
                        _collidersDamaged.Add(collider);
                    }
                  
                }
            }
        }
    }
}