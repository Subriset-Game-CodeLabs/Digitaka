using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundComboState : MeleeBaseState
    {
        // public GroundComboState(ComboCharacter comboCharacter) : base(comboCharacter){}

        public override void OnEnter(ComboCharacter comboCharacter)
        {
            base.OnEnter(comboCharacter);
            AttackIndex = 2;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            AudioManager.Instance.PlaySound(SoundType.SFX_Attack2);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
        }
        public override void OnUpdate(ComboCharacter comboCharacter)
        {
            base.OnUpdate(comboCharacter);

            if (fixedtime >= Duration)
            {
                if (ShouldCombo)
                {
                    comboCharacter.FiniteStateMachine.ChangeState(comboCharacter.GroundFinisher);
                }
                else
                {
                    comboCharacter.FiniteStateMachine.ChangeState(comboCharacter.IdleCombatState);
                }
            }
        }
    }
}