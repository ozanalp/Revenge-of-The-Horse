using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Humping")]
public class Humping : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.Hump.ToString(), false);
        animator.SetBool(TransitionParameter.Grab.ToString(), true);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool(TransitionParameter.Hump.ToString(), true);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            animator.SetBool(TransitionParameter.Grab.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.Hump.ToString(), false);
    }
}
