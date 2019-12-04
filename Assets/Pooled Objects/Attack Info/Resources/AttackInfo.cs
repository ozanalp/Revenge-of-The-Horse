using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    //private bool isPunch, isKick;
    public CharacterControl _attacker = null;
    public Attack attackAbility;
    public List<AttackBox> attackBoxes = new List<AttackBox>();
    public bool mustCollide;
    public bool mustFaceAttacker;
    public float lethalRange;
    public int maxHits;
    public int currentHits;
    public bool isRegistered;
    public bool isFinished;

    //private void Start()
    //{
    //    isPunch = attackAbility.isPunch;
    //    isKick = attackAbility.isKick;
    //}

    public void ResetAttackInfo(Attack attack, CharacterControl attacker)
    {
        isRegistered = false;
        isFinished = false;
        attackAbility = attack;
        _attacker = attacker;
    }

    public void RegisterAttack(Attack attack)
    {
        isRegistered = true;

        attackAbility = attack;
        attackBoxes = attack.attackBoxes;
        mustCollide = attack.mustCollide;
        mustFaceAttacker = attack.mustFaceAttacker;
        lethalRange = attack.lethalRange;
        maxHits = attack.maxHits;
        currentHits = 0;
    }

    private void OnDisable()
    {
        isFinished = true;
    }
}
