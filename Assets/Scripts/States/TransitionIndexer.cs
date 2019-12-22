using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionConditionType
{
    UP, DOWN, LEFT, RIGHT, L_PUNCH, L_KICK, H_PUNCH, H_KICK, GRAB, HUMP, MOVING_TO_BLOCKING_OBJ,
}

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/TransitionIndexer")]
public class TransitionIndexer : StateData
{
    public int index;
    public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (MakeTransition(control))
        {
            animator.SetInteger(HashManager.Instance.dicMainParams[TransitionParameter.TransitionIndex], index);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (MakeTransition(control))
        {
            animator.SetInteger(HashManager.Instance.dicMainParams[TransitionParameter.TransitionIndex], index);
        }
        else
        {
            animator.SetInteger(HashManager.Instance.dicMainParams[TransitionParameter.TransitionIndex], 0);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetInteger(HashManager.Instance.dicMainParams[TransitionParameter.TransitionIndex], 0);
    }

    bool MakeTransition(CharacterControl control)
    {
        foreach(TransitionConditionType c in transitionConditions)
        {
            switch (c)
            {
                case TransitionConditionType.UP:
                {
                    if (!control.moveUp)
                        return false;
                }
                break;

                case TransitionConditionType.DOWN:
                {
                    if (!control.moveDown)
                        return false;
                }
                break;

                case TransitionConditionType.LEFT:
                {
                    if (!control.moveLeft)
                        return false;
                }
                break;
                case TransitionConditionType.RIGHT:
                {
                    if (!control.moveRight)
                        return false;
                }
                break;

                case TransitionConditionType.L_PUNCH:
                {
                    if (!control.animationProgress.punchAttackTriggered)
                        return false;
                }
                break;

                case TransitionConditionType.L_KICK:
                {
                    if (!control.animationProgress.kickAttackTriggered)
                        return false;
                }
                break;

                case TransitionConditionType.H_PUNCH:
                {
                    if (!control.h_punch)
                        return false;
                }
                break;

                case TransitionConditionType.H_KICK:
                {
                    if (!control.h_kick)
                        return false;
                }
                break;

                case TransitionConditionType.GRAB:
                {
                    if (!control.grab)
                        return false;
                }
                break;

                case TransitionConditionType.HUMP:
                {
                    if (!control.hump)
                        return false;
                }
                break;

                case TransitionConditionType.MOVING_TO_BLOCKING_OBJ:
                {
                    foreach (KeyValuePair<GameObject, GameObject> data in
                        control.animationProgress.blockingObjs)
                    {
                        Vector3 dir = data.Value.transform.position -
                        control.transform.position;

                        if (dir.x > 0f && !control.moveRight)
                        {
                            return false;
                        }

                        if (dir.x < 0f && !control.moveLeft)
                        {
                            return false;
                        }
                    }
                }
                break;
            }
        }
        return true;
    }
}