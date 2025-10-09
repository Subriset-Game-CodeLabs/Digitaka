using Audio;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class GroundFinisherState:MeleeBaseState
    {
        // public GroundFinisherState(ComboCharacter comboCharacter):base(comboCharacter){}
        public override void OnEnter(ComboCharacter comboCharacter)
        {
            base.OnEnter(comboCharacter);
            AttackIndex = 3;
            Duration = 0.5f;
            Animator.SetTrigger("Attack" + AttackIndex);
            AudioManager.Instance.PlaySound(SoundType.SFX_Attack3);
            Debug.Log("Player Attack " + AttackIndex+" Fired");
            CinemachineShake.Instance.ShakeCamera(1,0.3f);
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