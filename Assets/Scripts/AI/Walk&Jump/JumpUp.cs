using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New State", menuName = "AI/JumpUp")]
public class JumpUp : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //CharacterControl control = characterState.GetCharacterControl(animator);
        characterState.characterControl.jump = true;

        // #62 
        if(characterState.characterControl.aiProgress.pathfindingAgent.startSphere.transform.position.x
           < characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.x)
        {
            characterState.characterControl.FaceForward(true);
        }
        else
        {
            characterState.characterControl.FaceForward(false);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        float topDist = characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.y
            - characterState.characterControl.collisionSpheres.topSpheres[0].transform.position.y;

        float bottomDist = characterState.characterControl.aiProgress.pathfindingAgent.endSphere.transform.position.y
            - characterState.characterControl.collisionSpheres.bottomSpheres[0].transform.position.y;

        // THESE LINES ARE ALSO TEMPORARY, BECAUSE EACH PLATFORM COULD HAVE DIFFERENT HEIGHT
        if (topDist < 3f && bottomDist > 0.5f)
        {
            if (characterState.characterControl.IsFacingForward())
            {
                characterState.characterControl.moveRight = true;
                characterState.characterControl.moveLeft = false;
            }
            else
            {
                characterState.characterControl.moveRight = false;
                characterState.characterControl.moveLeft = true;
            }
        }

        if (bottomDist < 0.5f)
        {
            characterState.characterControl.moveRight = false;
            characterState.characterControl.moveLeft = false;
            characterState.characterControl.jump = false;

            characterState.characterControl.aiController.InitializeAI();
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}