using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/UpdateBoxCollider")]
public class UpdateBoxCollider : StateData
{
    public Vector3 targetCenter;
    public float centerUpdateSpeed;
    [Space(10)]
    public Vector3 targetSize;
    public float sizeUpdateSpeed;
    [Space(10)]
    public bool keepUpdating;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);
        control.animationProgress.updatingBoxCollider = true;

        control.animationProgress.targetSize = targetSize;
        control.animationProgress.size_Speed = sizeUpdateSpeed;

        control.animationProgress.targetCenter = targetCenter;
        control.animationProgress.center_Speed = centerUpdateSpeed;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);        
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (!keepUpdating) control.animationProgress.updatingBoxCollider = false;
    }
}