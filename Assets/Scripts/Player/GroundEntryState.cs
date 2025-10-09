using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundEntryState : MeleeBaseState
    {
        // public GroundEntryState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter(ComboCharacter comboCharacter)
        {
            base.OnEnter(comboCharacter);
            AttackIndex = 1;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            AudioManager.Instance.PlaySound(SoundType.SFX_Attack1);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
        }

        public override void OnUpdate(ComboCharacter comboCharacter)
        {
            base.OnUpdate(comboCharacter);
            if (fixedtime > Duration)
            {
                Debug.Log(ShouldCombo);
                if (ShouldCombo)
                {
                    comboCharacter.FiniteStateMachine.ChangeState(comboCharacter.GroundComboState);
                }
                else
                {
                    comboCharacter.FiniteStateMachine.ChangeState(comboCharacter.IdleCombatState);
                }
            }
        }
    }
}