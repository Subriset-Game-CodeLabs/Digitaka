using System.Collections.Generic;
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
            // reset semua
            time = fixedtime = latetime = 0f;
            ShouldCombo = false;
            Animator = ComboCharacter.Animator;
            _collidersDamaged = new List<Collider>();
            _cameraShake = CameraShake.Instance;
            HitCollider = ComboCharacter.Hitbox;
            // _hitEffectPrefab = ComboCharacter.Hiteffect;
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

            // Collider[] collidersToDamage = new Collider[10];
            // ContactFilter2D filter = new ContactFilter2D();
            // filter.useTriggers = true;
            // int colliderCount = Physics.OverlapBox(HitCollider, filter, collidersToDamage);
            foreach  (Collider collider in collidersToDamage)
            {

                if (!_collidersDamaged.Contains(collider))
                {
                    TeamComponent hitTeamComponent = collider.GetComponentInChildren<TeamComponent>();

                    // Only check colliders with a valid Team Componnent attached
                    if (hitTeamComponent && hitTeamComponent.TeamIndex == TeamIndex.Enemy)
                    {
                        _cameraShake.Shake(0.2f, 0.05f);
                        int damageAmount = Random.Range(1, 50);
                        Vector3 attackDir = (hitTeamComponent.transform.position - ComboCharacter.transform.position);
                        DamagePopup.Create(
                            ComboCharacter.PrefabDamagePopup.transform,
                            hitTeamComponent.gameObject.transform.position, 
                            attackDir,
                            damageAmount, 
                            damageAmount>30);
                        // Object.Instantiate(_hitEffectPrefab, collidersToDamage[i].transform);
                        Debug.Log("Enemy Has Taken:" + AttackIndex + "Damage");
                        _collidersDamaged.Add(collider);
                    }
                }
            }
        }
     
        public override void OnExit()
        {
            InputManager.Instance.PlayerInput.Attack.OnDown -= OnShouldCombo;
        }

    }
}
