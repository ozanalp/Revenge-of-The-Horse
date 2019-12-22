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
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], true);
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], true);
        }
        else if (!control.doubleSpeed)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], false);
        }
        else
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Move], false);
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
