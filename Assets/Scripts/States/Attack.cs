using System.Collections.Generic;
using UnityEngine;

public enum AttackBox
{
    PUNCH_BOX,
    KICK_BOX,

    MELEE_WEAPON,
}

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Attack")]
public class Attack : StateData
{
    public bool isPunch, isKick;
    public bool debug;
    public float startAttackTime;
    public float endAttackTime;
    public List<AttackBox> attackBoxes = new List<AttackBox>();
    public bool mustCollide;
    public bool mustFaceAttacker;
    public float lethalRange;
    public int maxHits;
    public List<RuntimeAnimatorController> deathAnimators = new List<RuntimeAnimatorController>();

    [Header("Combo Timers")]
    public float comboStartTime;
    public float comboEndTime;

    [Header("Death Particles")]
    public bool useDeathParticles;
    public PoolObjectType particleType;

    private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //characterState.GetCharacterControl(animator).l_punch = false;
        //characterState.GetCharacterControl(animator).h_punch = false;
        //characterState.GetCharacterControl(animator).l_kick = false;
        //characterState.GetCharacterControl(animator).h_kick = false;

        //characterState.characterControl.l_punch = false;
        //characterState.characterControl.h_punch = false;
        //characterState.characterControl.l_kick = false;
        //characterState.characterControl.h_kick = false;

        characterState.characterControl.animationProgress.kickAttackTriggered = false;
        characterState.characterControl.animationProgress.punchAttackTriggered = false;

        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Kick], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Kick], false);

        GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO);
        AttackInfo info = obj.GetComponent<AttackInfo>();

        obj.SetActive(true);
        info.ResetAttackInfo(this, characterState.GetCharacterControl(animator));

        if (!AttackManager.Instance.currentAttacks.Contains(info))
        {
            AttackManager.Instance.currentAttacks.Add(info);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        RegisterAttack(characterState, animator, stateInfo);
        DeregisterAttack(characterState, animator, stateInfo);
        CheckCombo(characterState, animator, stateInfo);
    }


    public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (isPunch)
        {
            if (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null)
                    {
                        Debug.Log(info + " is null");
                        continue;
                    }

                    if (!info.isRegistered && info.attackAbility == this)
                    {
                        info.RegisterAttack(this);

                        CharacterControl control = characterState.GetCharacterControl(animator);
                        while (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
                        {
                            for (float t = 0; t <= startAttackTime; t += Time.deltaTime)
                            {
                                control.l_punchBox.SetActive(true);
                            }
                            break;
                        }

                        if (debug)
                        {
                            Debug.Log(name + " registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        if (isKick)
        {
            if (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null)
                    {
                        Debug.Log(info + " is null");
                        continue;
                    }

                    if (!info.isRegistered && info.attackAbility == this)
                    {
                        info.RegisterAttack(this);

                        CharacterControl control = characterState.GetCharacterControl(animator);
                        while (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
                        {
                            for (float t = 0; t <= startAttackTime; t += Time.deltaTime)
                            {
                                control.l_kickBox.SetActive(true);
                            }
                            break;
                        }

                        if (debug)
                        {
                            Debug.Log(name + " registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        //if (characterState.characterControl.l_punch && characterState.characterControl.l_kick)
        //{
        //    characterState.characterControl.l_kick = false;
        //    characterState.characterControl.l_punch = false;
        //    characterState.GetCharacterControl(animator).l_punch = false;
        //    characterState.GetCharacterControl(animator).l_kick = false;
        //}
    }

    public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (isPunch)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        CharacterControl control = characterState.GetCharacterControl(animator);
                        if (info.isFinished)
                        {
                            control.l_punchBox.SetActive(false);
                        }

                        if (debug)
                        {
                            Debug.Log(name + " de-registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        if (isKick)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        CharacterControl control = characterState.GetCharacterControl(animator);
                        if (info.isFinished)
                        {
                            control.l_kickBox.SetActive(false);
                        }

                        if (debug)
                        {
                            Debug.Log(name + " de-registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        characterState.characterControl.l_kick = false;
        characterState.characterControl.l_punch = false;
    }

    public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (isPunch)
        {
            if (stateInfo.normalizedTime >= comboStartTime)
            {
                if (stateInfo.normalizedTime <= comboEndTime)
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);

                    if (control.animationProgress.punchAttackTriggered)
                    {
                        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], true);
                        characterState.characterControl.animationProgress.punchAttackTriggered = false;
                    }
                }
            }
        }

        if (isKick)
        {
            if (stateInfo.normalizedTime >= comboStartTime)
            {
                if (stateInfo.normalizedTime <= comboEndTime)
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);

                    if (control.animationProgress.kickAttackTriggered/*control.l_kick*/)
                    {
                        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Kick], true);
                        characterState.characterControl.animationProgress.kickAttackTriggered = false;
                    }
                }
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Kick], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Punch], false);
        animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Kick], false);

        ClearAttack();
    }

    public void ClearAttack()
    {
        finishedAttacks.Clear();

        foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
        {
            if (info == null || info.isFinished)
            {
                finishedAttacks.Add(info);
            }
        }

        foreach (AttackInfo info in finishedAttacks)
        {
            if (AttackManager.Instance.currentAttacks.Contains(info))
            {
                AttackManager.Instance.currentAttacks.Remove(info);
            }
        }
    }

    public RuntimeAnimatorController GetDeathAnimator()
    {
        int index = Random.Range(0, deathAnimators.Count);
        return deathAnimators[index];
    }
}