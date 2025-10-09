using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class IdleCombatState : IStateCombat
    {
        // public IdleCombatState(ComboCharacter comboCharacter):base(comboCharacter){}

        public override void OnEnter(ComboCharacter ComboCharacter)
        {
            base.OnEnter(ComboCharacter);
            ComboCharacter.IsAttacking = false;
        }
        public  void OnExit()
        {
        }
    }

}