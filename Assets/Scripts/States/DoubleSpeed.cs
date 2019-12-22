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
                    animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], true);
                }
                else
                {
                    animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], false);
                }
            }
            else
            {
                animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], true);
            }

            if(control.moveRight && control.moveLeft || control.moveUp && control.moveDown)
            {
                animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], false);
            }
        }
        else
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.DoubleSpeed], false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}