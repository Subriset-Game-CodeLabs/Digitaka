using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundComboState : MeleeBaseState
    {
        public GroundComboState(ComboCharacter comboCharacter) : base(comboCharacter){}

        public override void OnEnter()
        {
            base.OnEnter();
            AttackIndex = 2;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (fixedtime >= Duration)
            {
                if (ShouldCombo)
                {
                    ComboCharacter.FiniteStateMachine.ChangeState(ComboCharacter.GroundFinisher);
                }
                else
                {
                    ComboCharacter.FiniteStateMachine.ChangeState(ComboCharacter.IdleCombatState);
                }
            }
        }
    }
}