using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Kick], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Kick], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], false);

        characterState.characterControl.animationProgress.blockingObjs.Clear();
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

        if (control.grab)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], true);
        }

        if(control.moveRight && control.moveLeft)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], false);
        }
        else if (control.moveRight)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], true);
        }
        else if (control.moveLeft)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], true);
        }

        if (control.moveUp && control.moveDown)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], false);
        }
        else if (control.moveUp)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], true);
        }
        else if (control.moveDown)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
