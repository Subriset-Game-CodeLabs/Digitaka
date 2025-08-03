using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundEntryState : MeleeBaseState
    {
        public GroundEntryState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter()
        {
            base.OnEnter();
            AttackIndex = 1;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            AudioManager.Instance.PlaySound(SoundType.SFX_Attack1);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (fixedtime > Duration)
            {
                Debug.Log(ShouldCombo);
                if (ShouldCombo)
                {
                    ComboCharacter.FiniteStateMachine.ChangeState(ComboCharacter.GroundComboState);
                }
                else
                {
                    ComboCharacter.FiniteStateMachine.ChangeState(ComboCharacter.IdleCombatState);
                }
            }
        }
    }
}