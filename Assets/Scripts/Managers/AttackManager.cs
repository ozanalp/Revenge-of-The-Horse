using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    public List<AttackInfo> currentAttacks = new List<AttackInfo>();

    public void ForceDeregister(CharacterControl control)
    {
        foreach (AttackInfo info in currentAttacks)
        {
            if (info._attacker == control)
            {
                info.isFinished = true;
                info.GetComponent<PoolObject>().TurnOff();
            }
        }
    }
}
