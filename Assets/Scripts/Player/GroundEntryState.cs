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
            Debug.Log("Player Attack " + AttackIndex+" Fired");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (fixedtime > Duration)
            {
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