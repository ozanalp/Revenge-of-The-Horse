using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_Walk_Transition
{
    start_walking, jump_up, fall_down,
}

[CreateAssetMenu(fileName = "New State", menuName = "AI/SendPathfindingAgent")]
public class SendPathfindingAgent : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.aiProgress.pathfindingAgent == null)
        {
            GameObject p = Instantiate(Resources.Load("PathfindingAgent", typeof(GameObject)) as GameObject);
            characterState.characterControl.aiProgress.pathfindingAgent = p.GetComponent<PathfindingAgent>();
        }

        characterState.characterControl.aiProgress.pathfindingAgent.owner = characterState.characterControl;
        characterState.characterControl.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;

        characterState.characterControl.aiProgress.pathfindingAgent.transform.position = 
            characterState.characterControl.transform.position + (Vector3.up * 0.5f);

        characterState.characterControl.obstacle.carving = false;
        characterState.characterControl.aiProgress.pathfindingAgent.GoToTarget();
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.aiProgress.pathfindingAgent.startWalk)
        {
            animator.SetBool(AI_Walk_Transition.start_walking.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(AI_Walk_Transition.start_walking.ToString(), false);
    }
}
