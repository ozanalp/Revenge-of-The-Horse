using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionConditionType
{
    UP, DOWN, LEFT, RIGHT, L_PUNCH, L_KICK, H_PUNCH, H_KICK, GRAB, HUMP,
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
            animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), index);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (MakeTransition(control))
        {
            animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), index);
        }
        else
        {
            animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), 0);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), 0);
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
                    if (!control.l_punch)
                        return false;
                }
                break;

                case TransitionConditionType.L_KICK:
                {
                    if (!control.l_kick)
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
            }
        }
        return true;
    }
}