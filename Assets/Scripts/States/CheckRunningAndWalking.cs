using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/CheckRunningAndWalking")]
public class CheckRunningAndWalking : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if ((control.moveLeft || control.moveRight || control.moveUp || control.moveDown) && control.doubleSpeed)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), true);
            animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), true);
        }
        else if (!control.doubleSpeed)
        {
            animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), false);
        }
        else
        {
            animator.SetBool(TransitionParameter.Move.ToString(), false);
            animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
