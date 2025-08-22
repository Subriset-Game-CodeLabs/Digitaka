using Audio;
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
            AudioManager.Instance.PlaySound(SoundType.SFX_Attack3);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
            CinemachineShake.Instance.ShakeCamera(1,0.3f);
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