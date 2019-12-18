using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AI/AITriggerAttack")]
public class AITriggerAttack : StateData
{
    private delegate void GroundAttack(CharacterControl control);

    private List<GroundAttack> ListGroundAttacks = new List<GroundAttack>();

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (ListGroundAttacks.Count == 0)
        {
            ListGroundAttacks.Add(PunchAttack);
            ListGroundAttacks.Add(KickAttack);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.aiProgress.TargetIsDead())
        {
            characterState.characterControl.l_punch = false;
            characterState.characterControl.l_kick = false;
            return;
        }

        Debug.Log(characterState.characterControl.name + characterState.characterControl.aiProgress.AIDistanceToTarget());
        if (characterState.characterControl.aiProgress.AIDistanceToTarget() < 3f)
        {
            ListGroundAttacks[Random.Range(0, ListGroundAttacks.Count)](characterState.characterControl);
        }
        else
        {
            characterState.characterControl.l_punch = false;
            characterState.characterControl.l_kick = false;
            //characterState.characterControl.aiController.InitializeAI();
        }

        characterState.characterControl.animationProgress.kickAttackTriggered = characterState.characterControl.l_kick;
        characterState.characterControl.animationProgress.punchAttackTriggered = characterState.characterControl.l_punch;
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public void PunchAttack(CharacterControl control)
    {
        control.moveUp = false;
        control.moveDown = false;
        control.moveRight = false;
        control.moveLeft = false;

        if (control.aiProgress.IsFacingTarget() && control.animationProgress.IsRunning(typeof(MoveForward)) ||
            control.aiProgress.IsFacingTarget() && !control.animationProgress.IsRunning(typeof(MoveForward)))
        {
            control.l_punch = true;
        }

        control.l_punch = false;
    }

    public void KickAttack(CharacterControl control)
    {
        control.moveUp = false;
        control.moveDown = false;
        control.moveRight = false;
        control.moveLeft = false;

        if (control.aiProgress.IsFacingTarget() && control.animationProgress.IsRunning(typeof(MoveForward)) ||
            control.aiProgress.IsFacingTarget() && !control.animationProgress.IsRunning(typeof(MoveForward)))
        {
            control.l_kick = true;
        }

        control.l_kick = false;
    }
}