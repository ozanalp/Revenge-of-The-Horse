using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private CharacterControl control;
    private CollisionSpheres collisionSpheres;

    public int damageTaken;

    private void Awake()
    {
        damageTaken = 0;
        control = GetComponent<CharacterControl>();
        collisionSpheres = GetComponentInChildren<CollisionSpheres>();
    }

    private void Update()
    {
        if (AttackManager.Instance.currentAttacks.Count > 0)
        {
            CheckAttack();
        }
    }

    private void CheckAttack()
    {
        foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
        {
            if (info == null)
            {
                continue;
            }

            if (!info.isRegistered)
            {
                continue;
            }

            if (info.isFinished)
            {
                continue;
            }

            if (info.currentHits >= info.maxHits)
            {
                continue;
            }

            if (info._attacker == control)
            {
                continue;
            }

            if (info.mustFaceAttacker)
            {
                Vector3 vec = transform.position - info._attacker.transform.position;
                if (vec.z * info._attacker.transform.right.x < 0f)
                {
                    continue;
                }
            }

            if (info.mustCollide)
            {
                if (isCollided(info))
                {
                    TakeDamage(info);
                }
            }
            else
            {
                float dist = Vector3.SqrMagnitude(gameObject.transform.position - info._attacker.transform.position);
                if (dist <= info.lethalRange)
                {
                    TakeDamage(info);
                }
            }
        }
    }

    private bool isCollided(AttackInfo info)
    {
        foreach (TriggerDetector trigger in control.GetAllTriggers())
        {
            foreach (Collider collider in trigger.collidingBoxes)
            {
                foreach (AttackBox ab in info.attackBoxes)
                {
                    if (ab == AttackBox.PUNCH_BOX)
                    {
                        if (collider.gameObject == info._attacker.l_punchBox)
                        {
                            return true;
                        }
                    }
                    else if (ab == AttackBox.KICK_BOX)
                    {
                        if (collider.gameObject == info._attacker.l_kickBox)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void TakeDamage(AttackInfo info)
    {
        if (damageTaken > 0)
        {
            Debug.Log("this will be changed when there is dmage system implemented.");
            return;
        }

        if (info.mustCollide)
        {
            CameraManager.Instance.ShakeCamera(.2f);
        }

        Debug.Log(info._attacker.gameObject.name + " hits " + gameObject.name);

        if (info.attackAbility)
        {
            control.animator.runtimeAnimatorController = info.attackAbility.GetDeathAnimator();
        }

        info.currentHits++;
        damageTaken++;

        AttackManager.Instance.ForceDeregister(control);

        control.GetComponent<BoxCollider>().enabled = false;
        control.RIGID_BODY.useGravity = false;

        collisionSpheres.RemoveSpheres(collisionSpheres.bottomSpheres);
        collisionSpheres.RemoveSpheres(collisionSpheres.topSpheres);
        collisionSpheres.RemoveSpheres(collisionSpheres.rightSpheres);
        collisionSpheres.RemoveSpheres(collisionSpheres.leftSpheres);
        collisionSpheres.RemoveSpheres(collisionSpheres.frontSpheres);
        collisionSpheres.RemoveSpheres(collisionSpheres.backSpheres);

        if (control.aiController != null)
        {
            control.aiController.gameObject.SetActive(false);
            control.obstacle.enabled = false;
        }
    }
}