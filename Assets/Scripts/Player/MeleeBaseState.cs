using System.Collections.Generic;
using Audio;
using Enemy;
using Input;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public abstract class MeleeBaseState: IStateCombat
    {
        public float Duration;
        protected Animator Animator;
        protected bool ShouldCombo;
        protected int AttackIndex;
        
        protected Collider HitCollider;
        private List<Collider> _collidersDamaged;
        private GameObject _hitEffectPrefab;
        private float _attackPressedTimer;
        private CameraShake _cameraShake;
        protected float _attackDamage;

        public override void OnEnter(ComboCharacter comboCharacter)
        {
            base.OnEnter(comboCharacter);
            time = fixedtime = latetime = 0f;
            ShouldCombo = false;
            comboCharacter.IsAttacking = true;
            Animator = comboCharacter.Animator;
            _collidersDamaged = new List<Collider>();
            _cameraShake = CameraShake.Instance;
            HitCollider = comboCharacter.Hitbox;
            _hitEffectPrefab = comboCharacter.Hiteffect;
            _attackDamage = PlayerStats.Instance.damage;
            InputManager.Instance.PlayerInput.Attack.OnDown += OnShouldCombo;
        }
        private void OnShouldCombo()
        {
            _attackPressedTimer = 2;
            if (Animator.GetFloat("AttackWindow.Open") > 0f && _attackPressedTimer > 0)
            {
                ShouldCombo = true;
            }
        }

        public override void OnUpdate(ComboCharacter comboCharacter)
        {
            base.OnUpdate(comboCharacter);
            _attackPressedTimer -= Time.deltaTime;
            if (Animator.GetFloat("Weapon.Active") > 0f)
            {
                Attack(comboCharacter);
            }
        }

        private void Attack(ComboCharacter comboCharacter)
        {
            // Create Overlap Box in 3D
            Collider[] collidersToDamage = Physics.OverlapBox(
                HitCollider.bounds.center,
                HitCollider.bounds.extents,
                HitCollider.transform.rotation,
                ~0, // All layers
                QueryTriggerInteraction.Collide
            );
            foreach  (Collider collider in collidersToDamage)
            {
                if (!_collidersDamaged.Contains(collider))
                {
                    EnemyStats hitEnemy = collider.GetComponentInChildren<EnemyStats>();

                    // Only check colliders with a valid Team Componnent attached
                    if (hitEnemy && hitEnemy.IsAlive)
                    {
                        _cameraShake.Shake(0.2f, 0.05f);
                        Vector3  attackDir = (hitEnemy.transform.position - comboCharacter.transform.position);
                        DamagePopup.Create(
                            comboCharacter.PrefabDamagePopup.transform,
                            hitEnemy.gameObject.transform.position, 
                            attackDir,
                            _attackDamage, 
                            PlayerStats.Instance.damage>1);
                        Object.Instantiate(_hitEffectPrefab, collider.transform);
                        AudioManager.Instance.PlaySound(SoundType.SFX_PlayerGetHit);
                        hitEnemy.TakeDamage(_attackDamage);
                        Debug.Log("Enemy Has Taken:" + AttackIndex + "Damage");
                        _collidersDamaged.Add(collider);
                    }
                }
            }
        }
     
        public override void OnExit(ComboCharacter comboCharacter)
        {
            base.OnExit(comboCharacter);
            InputManager.Instance.PlayerInput.Attack.OnDown -= OnShouldCombo;
            comboCharacter.IsAttacking = false;
        }

    }
}
