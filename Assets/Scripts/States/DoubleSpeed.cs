using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/DoubleSpeed")]
public class DoubleSpeed : StateData
{
    public bool mustRequireMovement;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (control.doubleSpeed)
        {
            if (mustRequireMovement)
            {
                if (control.moveLeft || control.moveRight || control.moveUp || control.moveDown)
                {
                    animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), false);
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), true);
            }

            if(control.moveRight && control.moveLeft || control.moveUp && control.moveDown)
            {
                animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), false);
            }
        }
        else
        {
            animator.SetBool(TransitionParameter.DoubleSpeed.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}