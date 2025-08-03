using System.Collections.Generic;
using Audio;
using Enemy;
using Input;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public abstract class MeleeBaseState: IStateCombat
    {
        protected ComboCharacter ComboCharacter;
        public float Duration;
        protected Animator Animator;
        protected bool ShouldCombo;
        protected int AttackIndex;
        
        protected Collider HitCollider;
        private List<Collider> _collidersDamaged;
        private GameObject _hitEffectPrefab;
        private float _attackPressedTimer;
        private CameraShake _cameraShake;

        public MeleeBaseState(ComboCharacter comboCharacter)
        {
            ComboCharacter = comboCharacter;
        }
        public override void OnEnter()
        {
            time = fixedtime = latetime = 0f;
            ShouldCombo = false;
            ComboCharacter.IsAttacking = true;
            Animator = ComboCharacter.Animator;
            _collidersDamaged = new List<Collider>();
            _cameraShake = CameraShake.Instance;
            HitCollider = ComboCharacter.Hitbox;
            _hitEffectPrefab = ComboCharacter.Hiteffect;
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

        public override void OnUpdate()
        {
            base.OnUpdate();
            _attackPressedTimer -= Time.deltaTime;
            if (Animator.GetFloat("Weapon.Active") > 0f)
            {
                Attack();
            }
        }

        private void Attack()
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
                        Vector3  attackDir = (hitEnemy.transform.position - ComboCharacter.transform.position);
                        DamagePopup.Create(
                            ComboCharacter.PrefabDamagePopup.transform,
                            hitEnemy.gameObject.transform.position, 
                            attackDir,
                            PlayerStats.Instance.damage, 
                            PlayerStats.Instance.damage>5);
                        Object.Instantiate(_hitEffectPrefab, collider.transform);
                        AudioManager.Instance.PlaySound(SoundType.SFX_PlayerGetHit);
                        hitEnemy.TakeDamage(PlayerStats.Instance.damage);
                        Debug.Log("Enemy Has Taken:" + AttackIndex + "Damage");
                        _collidersDamaged.Add(collider);
                    }
                }
            }
        }
     
        public override void OnExit()
        {
            InputManager.Instance.PlayerInput.Attack.OnDown -= OnShouldCombo;
            ComboCharacter.IsAttacking = false;
        }

    }
}
