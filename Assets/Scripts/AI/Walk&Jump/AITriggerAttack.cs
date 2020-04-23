using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AI/AITriggerAttack")]
public class AITriggerAttack : StateData
{
    private delegate void GroundAttack(CharacterControl control);

    private List<GroundAttack> ListGroundAttacks = new List<GroundAttack>();

    [SerializeField] float attackCounter;
    [SerializeField] float minTimeBetweenAttacks = .9f;
    [SerializeField] float maxTimeBetweenAttacks = 3f;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (ListGroundAttacks.Count == 0)
        {
            ListGroundAttacks.Add(PunchAttack);
            ListGroundAttacks.Add(KickAttack);
        }

        attackCounter = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (characterState.characterControl.aiProgress.TargetIsDead())
        {
            characterState.characterControl.l_punch = false;
            characterState.characterControl.l_kick = false;
            return;
        }

        if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
        {
            ListGroundAttacks[Random.Range(0, ListGroundAttacks.Count)](characterState.characterControl);
        }
        else
        {
            characterState.characterControl.l_punch = false;
            characterState.characterControl.l_kick = false;
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

        if (control.aiProgress.IsFacingTarget() && !control.animationProgress.IsRunning(typeof(MoveForward)))
        {
            //control.l_punch = true;
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0f)
            {
                control.l_punch = true;
                attackCounter = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
            }
        }

        if (control.l_kick && control.l_punch)
        {
            control.l_kick = false;
            control.l_punch = false;
        }
    }

    public void KickAttack(CharacterControl control)
    {
        control.moveUp = false;
        control.moveDown = false;
        control.moveRight = false;
        control.moveLeft = false;

        if (control.aiProgress.IsFacingTarget() && !control.animationProgress.IsRunning(typeof(MoveForward)))
        {
            //control.l_kick = true;
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0f)
            {
                control.l_kick = true;
                attackCounter = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
            }
        }

        if (control.l_kick && control.l_punch)
        {
            control.l_kick = false;
            control.l_punch = false;
        }
    }
}