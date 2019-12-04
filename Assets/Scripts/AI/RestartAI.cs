using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AI/RestartAI")]
public class RestartAI : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        // walking
        if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 4f) //6.5
        {
            if (characterState.characterControl.aiProgress.TargetDistanceToEndSphere() > 5f) //3.5
            {
                //if (characterState.characterControl.aiProgress.TargetIsGrounded())
                {
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }

        // path is blocked
        if (characterState.characterControl.animationProgress.blockingObjs.Count == 0)
        {
            characterState.characterControl.aiProgress.blockingCharacter = null;
        }
        else
        {
            foreach (KeyValuePair<GameObject, GameObject> data in
            characterState.characterControl.animationProgress.blockingObjs)
            {
                CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(data.Value);

                if (blockingChar != null)
                {
                    characterState.characterControl.aiProgress.blockingCharacter = blockingChar;
                    break;
                }
                //else
                //{
                //    characterState.characterControl.aiProgress.blockingCharacter = null;
                //}
            }
        }

        if (characterState.characterControl.aiProgress.blockingCharacter != null)
        {
            //if (characterState.characterControl.animationProgress.Ground != null)
            {
                //if (!characterState.characterControl.animationProgress.IsRunning(typeof(Jump)) &&
                //    !characterState.characterControl.animationProgress.IsRunning(typeof(JumpPrep)))
                {
                    characterState.characterControl.doubleSpeed = false;
                    characterState.characterControl.jump = false;
                    characterState.characterControl.moveRight = false;
                    characterState.characterControl.moveLeft = false;
                    characterState.characterControl.moveUp = false;
                    characterState.characterControl.moveDown = false;
                    //characterState.characterControl.aiProgress.pathIsBlocked = true;
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}