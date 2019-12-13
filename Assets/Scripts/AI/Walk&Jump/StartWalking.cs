using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AI/StartWalking")]
public class StartWalking : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.aiProgress.SetRandomAttack();

        characterState.characterControl.aiController.WalkStraightToStartSpehere();
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.l_punch)
        {
            return;
        }

        if (characterState.characterControl.l_kick)
        {
            return;
        }

        float dist = characterState.characterControl.aiProgress.AIDistanceToStartSphere();

        // JUMP FOR FUTURE PROGRESS
        // if the y value of the start sphere is lower than the value of the end sphere
        if (characterState.characterControl.aiProgress.EndSphereIsHigher())
        {
            if (dist < 4f)
            {
                characterState.characterControl.moveRight = false;
                characterState.characterControl.moveLeft = false;
                characterState.characterControl.moveUp = false;
                characterState.characterControl.moveDown = false;

                animator.SetBool(AI_Walk_Transition.jump_up.ToString(), true);
                return; // IN BOTH CASES WE STOP UPDATING AND CARRY ON THE NEXT STATE
            }
        }

        // FALL FOR FUTURE PROGRESS
        if (characterState.characterControl.aiProgress.EndSphereIsLower())
        {
            animator.SetBool(AI_Walk_Transition.fall_down.ToString(), true);
            return;
        }

        // STRAIGHT
        if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 7f) //5
        {
            characterState.characterControl.doubleSpeed = true;
        }
        else
        {
            characterState.characterControl.doubleSpeed = false;
        }

        characterState.characterControl.aiController.WalkStraightToStartSpehere();

        if (characterState.characterControl.aiProgress.TargetIsOnTheSameZAxis() == false)
        {
            //Debug.Log(characterState.characterControl.name + " " + characterState.characterControl.aiProgress.TargetIsOnTheSameZAxis());
            characterState.characterControl.aiController.WalkToTheAttackingPosition();
        }

        if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < .7f)  //7
        {
            characterState.characterControl.doubleSpeed = false;
            characterState.characterControl.moveRight = false;
            characterState.characterControl.moveLeft = false;
            characterState.characterControl.moveUp = false;
            characterState.characterControl.moveDown = false;
        }

        characterState.characterControl.aiProgress.RepositionDestination();
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(AI_Walk_Transition.jump_up.ToString(), false);
        animator.SetBool(AI_Walk_Transition.fall_down.ToString(), false);
    }
}