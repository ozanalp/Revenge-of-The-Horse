using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Release")]
public class Release : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        var control = characterState.GetCharacterControl(animator);
        var caughtFur = control.caughtFur;
        
        control.CallAnimationEnding();

        caughtFur.GetComponentInChildren<SpriteRenderer>().enabled = true;
        caughtFur.SetActive(true);
        caughtFur.transform.parent = null;
    }
}