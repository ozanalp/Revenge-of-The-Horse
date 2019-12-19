using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_TYPE
{
    NONE, WALK_AND_JUMP,
}
public class AIController : MonoBehaviour
{
    private List<AISubset> AIList = new List<AISubset>();
    public AI_TYPE initialAI;
    private Coroutine AIRoutine;
    private Vector3 targetDir = new Vector3();
    private CharacterControl control;

    /* WHEN THE GAME STARTS WE ARE LOOKING FOR THE EVERY AISUBSET
     * AND ON THE NEXT FRAME
     * WE TRIGGER ONE OF THEM WHICH IS THE INITIAL AI
     */

    private void Awake()
    {
        control = gameObject.GetComponentInParent<CharacterControl>();
    }

    private void Start()
    {
        InitializeAI();
    }

    public void InitializeAI()
    {
        if (AIList.Count == 0)
        {
            AISubset[] arr = gameObject.GetComponentsInChildren<AISubset>();

            foreach (AISubset s in arr)
            {
                if (!AIList.Contains(s))
                {
                    AIList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        //control.aiProgress.pathIsBlocked = false;
        AIRoutine = StartCoroutine(_InitAI());
    }

    private void OnEnable()
    {
        if (AIRoutine != null)
        {
            StopCoroutine(AIRoutine);
        }
    }

    private IEnumerator _InitAI()
    {
        yield return new WaitForEndOfFrame();

        TriggerAI(initialAI);
    }

    public void TriggerAI(AI_TYPE aiType)
    {
        AISubset next = null;

        foreach (AISubset s in AIList)
        {
            s.gameObject.SetActive(false);

            if (s.aiType == aiType)
            {
                next = s;
            }
        }

        if (next != null)
        {
            next.gameObject.SetActive(true);
        }
    }

    public void WalkStraightToStartSpehere()
    {
        control.animator.SetBool("L_Kick", false);
        control.animator.SetBool("L_Punch", false);

        targetDir = control.aiProgress.pathfindingAgent.startSphere.transform.position - control.transform.position;

        if (control.aiProgress.blockingCharacter == null)
        {
            if (targetDir.x > .65f)
            {
                control.moveRight = true;
                control.moveLeft = false;
            }
            else if (targetDir.x < -.65f)
            {
                control.moveRight = false;
                control.moveLeft = true;
            }
            else if (targetDir.x >= -0.64f && targetDir.x <= 0.64f)
            {
                control.moveRight = false;
                control.moveLeft = false;
            }

            if (targetDir.z > 0)
            {
                control.moveUp = true;
                control.moveDown = false;
            }
            else if (targetDir.z < 0)
            {
                control.moveUp = false;
                control.moveDown = true;
            }
            else if (targetDir.z == 0)
            {
                control.moveUp = false;
                control.moveDown = false;
            }
        }
    }

    public void WalkToTheAttackingPosition()
    {
        //targetDir = control.aiProgress.pathfindingAgent.target.transform.position - control.transform.position;
        targetDir = control.aiProgress.pathfindingAgent.endSphere.transform.position - control.transform.position;

        if (targetDir.z > .1f)
        {
            control.moveUp = true;
            control.moveDown = false;
        }
        else if (targetDir.z < -.1f)
        {
            control.moveUp = false;
            control.moveDown = true;
        }
        else if (targetDir.z >= -.09f && targetDir.z <= .09f)
        {
            control.moveUp = false;
            control.moveDown = false;
        }

        if (control.aiProgress.blockingCharacter == null)
        {
            return;
        }
        else if (control.aiProgress.blockingCharacter.isEnemy)
        {
            Vector3 targetOfTargetDir = control.aiProgress.pathfindingAgent.target.transform.position -
            control.aiProgress.blockingCharacter.transform.position;
            if (targetOfTargetDir.x < 0)
            {
                control.moveRight = true;
                control.moveLeft = false;
                control.RIGID_BODY.AddForce(control.transform.right * Vector3.Magnitude(targetOfTargetDir) * 300 * Time.deltaTime);
            }
            else if (targetOfTargetDir.x == 0)
            {
                if (!control.IsFacingForward())
                {
                    control.RIGID_BODY.AddForce(control.transform.right * 300 * Time.deltaTime);
                }

                if (control.IsFacingForward())
                {
                    control.RIGID_BODY.AddForce(control.transform.right * -300 * Time.deltaTime);
                }
            }
            else
            {
                control.moveLeft = true;
                control.moveRight = false;
                control.RIGID_BODY.AddForce(control.transform.right * Vector3.Magnitude(targetOfTargetDir) * -300 * Time.deltaTime);
            }
        }
    }
}