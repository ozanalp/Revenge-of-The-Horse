using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/CheckRunningTurn")]
public class CheckRunningTurn : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (control.IsFacingForward())
        {
            if (control.moveLeft)
            {
                animator.SetBool(TransitionParameter.Turn.ToString(), true);
            }
        }
        //if (!control.IsFacingForward())
        else
        {
            if (control.moveRight)
            {
                animator.SetBool(TransitionParameter.Turn.ToString(), true);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.Turn.ToString(), false);
    }
}