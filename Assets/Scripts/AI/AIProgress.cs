using UnityEngine;

public class AIProgress : MonoBehaviour
{
    public PathfindingAgent pathfindingAgent;
    private CharacterControl control;
    public CharacterControl blockingCharacter;
    //public bool pathIsBlocked;
    public bool doKickAttack;

    private void Awake()
    {
        control = gameObject.GetComponentInParent<CharacterControl>();
    }

    public float AIDistanceToStartSphere()
    {
        return Vector3.SqrMagnitude(
            control.aiProgress.pathfindingAgent.startSphere.transform.position -
            control.transform.position);
    }

    public float AIDistanceToEndSphere()
    {
        return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.endSphere.transform.position -
                control.transform.position);
    }

    public float AIDistanceToTarget()
    {
        return Vector3.SqrMagnitude(
            control.aiProgress.pathfindingAgent.target.transform.position -
            control.transform.position);
    }

    public float TargetDistanceToEndSphere()
    {
        return Vector3.SqrMagnitude(
            control.aiProgress.pathfindingAgent.endSphere.transform.position -
            control.aiProgress.pathfindingAgent.target.transform.position);
    }

    public bool EndSphereIsOnSameLevel()
    {
        if (Mathf.Abs(pathfindingAgent.endSphere.transform.position.y -
            pathfindingAgent.startSphere.transform.position.y) > 0.01f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool EndSphereIsHigher()
    {
        if (EndSphereIsOnSameLevel())
        {
            return false;
        }

        if (pathfindingAgent.endSphere.transform.position.y - pathfindingAgent.startSphere.transform.position.y > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EndSphereIsLower()
    {
        if (EndSphereIsOnSameLevel())
        {
            return false;
        }

        if (pathfindingAgent.endSphere.transform.position.y - pathfindingAgent.startSphere.transform.position.y > 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool TargetIsDead()
    {
        if (CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target).damageDetector.damageTaken > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TargetIsOnRightSide()
    {
        if ((control.aiProgress.pathfindingAgent.target.transform.position -
            control.transform.position).x > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TargetIsOnTheSameZAxis()
    {
        if (Mathf.Abs(control.aiProgress.pathfindingAgent.target.transform.position.z -
            control.transform.position.z) <= .5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetRandomAttack()
    {
        if (Random.Range(0f, 1f) < .3f)
        {
            doKickAttack = true;
        }
        else
        {
            doKickAttack = false;
        }
    }

    public bool IsFacingTarget()
    {
        if ((control.aiProgress.pathfindingAgent.target.transform.position - control.transform.position).x > 0f)
        {
            if (control.IsFacingForward())
            {
                return true;
            }
        }
        else
        {
            if (!control.IsFacingForward())
            {
                return true;
            }
        }
        
        if ((control.aiProgress.pathfindingAgent.target.transform.position - control.transform.position).z > 0f)
        {
            if (control.IsMovingUp())
            {
                return true;
            }
        }
        else
        {
            if (control.IsMovingDown())
            {
                return true;
            }
        }

        return false;
    }

    public void RepositionDestination()
    {
        pathfindingAgent.startSphere.transform.position = pathfindingAgent.target.transform.position;
        pathfindingAgent.endSphere.transform.position = pathfindingAgent.target.transform.position;
    }
}