using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.L_Punch.ToString(), false);
        animator.SetBool(TransitionParameter.L_Kick.ToString(), false);
        animator.SetBool(TransitionParameter.H_Punch.ToString(), false);
        animator.SetBool(TransitionParameter.H_Kick.ToString(), false);
        animator.SetBool(TransitionParameter.Grab.ToString(), false);
        animator.SetBool(TransitionParameter.Hump.ToString(), false);
        animator.SetBool(TransitionParameter.Move.ToString(), false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (control.animationProgress.punchAttackTriggered)
        {
            animator.SetBool(TransitionParameter.L_Punch.ToString(), true);
            characterState.characterControl.animationProgress.punchAttackTriggered = false;
        }

        if (control.animationProgress.kickAttackTriggered)
        {
            animator.SetBool(TransitionParameter.L_Kick.ToString(), true);
            characterState.characterControl.animationProgress.kickAttackTriggered = false;
        }

        if (control.h_punch)
        {
            animator.SetBool(TransitionParameter.H_Punch.ToString(), true);
        }

        if (control.h_kick)
        {
            animator.SetBool(TransitionParameter.H_Kick.ToString(), true);
        }

        if (control.grab)
        {
            animator.SetBool(TransitionParameter.Grab.ToString(), true);
        }

        if(control.moveRight && control.moveLeft)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }
        else if (control.moveRight)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }
        else if (control.moveLeft)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }

        if (control.moveUp && control.moveDown)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }

        else if (control.moveUp)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }
        else if (control.moveDown)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //animator.SetBool(TransitionParameter.L_Punch.ToString(), false);
        //animator.SetBool(TransitionParameter.L_Kick.ToString(), false);
        //animator.SetBool(TransitionParameter.H_Punch.ToString(), false);
        //animator.SetBool(TransitionParameter.H_Kick.ToString(), false);
        //animator.SetBool(TransitionParameter.Grab.ToString(), false);
        //animator.SetBool(TransitionParameter.Hump.ToString(), false);
        //animator.SetBool(TransitionParameter.Move.ToString(), false);
    }
}
