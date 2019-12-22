using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Humping")]
public class Humping : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], true);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], true);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], false);
    }
}
