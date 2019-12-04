using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New State", menuName = "AI/FallDown")]
public class FallDown : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.transform.position.x < characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.x)
        {
            characterState.characterControl.FaceForward(true);
        }
        else if(characterState.characterControl.transform.position.x > characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.x)
        {
            characterState.characterControl.FaceForward(false);
        }

        if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 7f)
        {
            characterState.characterControl.doubleSpeed = true;
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.IsFacingForward())
        {
            if(characterState.characterControl.transform.position.x < characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.x)
            {
                characterState.characterControl.moveRight = true;
                characterState.characterControl.moveLeft = false;
            }
            else
            {
                characterState.characterControl.moveRight = false;
                characterState.characterControl.moveLeft = false;

                characterState.characterControl.aiController.InitializeAI();
            }
        }
        else
        {
            if (characterState.characterControl.transform.position.x > characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.x)
            {
                characterState.characterControl.moveRight = false;
                characterState.characterControl.moveLeft = true;
            }
            else
            {
                characterState.characterControl.moveRight = false;
                characterState.characterControl.moveLeft = false;

                characterState.characterControl.aiController.InitializeAI();
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}