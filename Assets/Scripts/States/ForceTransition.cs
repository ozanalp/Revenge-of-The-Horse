using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/ForceTransition")]
public class ForceTransition : StateData
{
    [Range(.01f, 1f)] public float transitionTiming;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if(stateInfo.normalizedTime >= transitionTiming)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.ForceTransition], true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.ForceTransition], false);
    }
}