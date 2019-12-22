using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Turn180")]
public class Turn180 : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (!control.IsFacingForward())
        {
            control.FaceForward(false);
        }
        else
        {
            control.FaceForward(true);
        }

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Turn], false);
    }
}