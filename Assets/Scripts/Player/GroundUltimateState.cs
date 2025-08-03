using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundUltimateState:MeleeBaseState
    {
        public GroundUltimateState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter()
        {
            base.OnEnter();
            AttackIndex = 3;
            Duration = 0.5f;
            _attackDamage = 2;
            Animator.SetTrigger("Ultimate");
            AudioManager.Instance.PlaySound(SoundType.SFX_Ultimate);
            Debug.Log("Ultimate Fired");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (fixedtime >= Duration)
            {
                ComboCharacter.FiniteStateMachine.ChangeState(ComboCharacter.IdleCombatState);
            }
        }
    }
}