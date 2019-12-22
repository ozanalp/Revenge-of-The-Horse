using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/CheckAttack")]
public class CheckAttack : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (control.animationProgress.punchAttackTriggered)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], true);
            characterState.characterControl.animationProgress.punchAttackTriggered = false;
        }

        if (control.animationProgress.kickAttackTriggered)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Kick], true);
            characterState.characterControl.animationProgress.kickAttackTriggered = false;
        }

        if (control.h_punch)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Punch], true);
        }

        if (control.h_kick)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Kick], true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
