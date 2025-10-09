using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundUltimateState:MeleeBaseState
    {
        // public GroundUltimateState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter(ComboCharacter comboCharacter)
        {
            base.OnEnter(comboCharacter);
            AttackIndex = 3;
            Duration = 0.5f;
            _attackDamage = 2;
            Animator.SetTrigger("Ultimate");
            AudioManager.Instance.PlaySound(SoundType.SFX_Ultimate);
            Debug.Log("Ultimate Fired");
        }
        public override void OnUpdate(ComboCharacter comboCharacter)
        {
            base.OnUpdate(comboCharacter);

            if (fixedtime >= Duration)
            {
                comboCharacter.FiniteStateMachine.ChangeState(comboCharacter.IdleCombatState);
            }
        }
    }
}