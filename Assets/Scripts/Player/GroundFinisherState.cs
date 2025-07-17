using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundFinisherState:MeleeBaseState
    {
        public GroundFinisherState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter()
        {
            base.OnEnter();
            AttackIndex = 3;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
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