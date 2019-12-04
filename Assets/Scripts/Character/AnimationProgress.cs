using System.Collections.Generic;
using UnityEngine;

public class AnimationProgress : MonoBehaviour
{
    public Dictionary<StateData, int> CurrentRunningAbilities = new Dictionary<StateData, int>();

    public bool cameraShaken;
    public List<PoolObjectType> poolObjectList = new List<PoolObjectType>();
    public MoveForward latestMoveForward;

    [Header("Attack Button")]
    public bool punchAttackTriggered;
    public bool kickAttackTriggered;
    public bool attackButtonIsReset;

    [Header("Movement")]
    private List<GameObject> spheresList;
    private float dirBlock;

    [Header("AirControl")]
    public float momentum;

    [Header("Colliding Objects")]
    public Dictionary<GameObject, GameObject> blockingObjs = new Dictionary<GameObject, GameObject>();

    [Header("UpdateBoxColldier")]
    public bool updatingBoxCollider;
    public bool updatingSpheres;
    public Vector3 targetSize;
    public float size_Speed;
    public Vector3 targetCenter;
    public float center_Speed;

    private CharacterControl control;
    private void Awake()
    {
        control = GetComponentInParent<CharacterControl>();
    }

    private void Update()
    {
        if (control.l_punch)
        {
            if (attackButtonIsReset)
            {
                punchAttackTriggered = true;
                attackButtonIsReset = false;
            }
        }
        else if (control.l_kick)
        {
            if (attackButtonIsReset)
            {
                kickAttackTriggered = true;
                attackButtonIsReset = false;
            }
        }
        else
        {
            attackButtonIsReset = true;
        }

        if (DoublePunchTap.Instance.doubleTap)
        {
            punchAttackTriggered = false;
            kickAttackTriggered = false;
        }
        else if (control.l_kick && control.l_punch)
        {
            punchAttackTriggered = false;
            kickAttackTriggered = false;
        }
        else if (punchAttackTriggered)
        {
            kickAttackTriggered = false;
        }
        else if (kickAttackTriggered)
        {
            punchAttackTriggered = false;
        }
        else if (control.l_kick)
        {
            kickAttackTriggered = true;
        }
        else if (control.l_punch)
        {
            punchAttackTriggered = true;
        }
    }

    private void FixedUpdate()
    {
        if (IsRunning(typeof(MoveForward)))
        {
            CheckBlockingObjs();
        }
        else
        {
            if (blockingObjs.Count != 0)
            {
                blockingObjs.Clear();
            }
        }
    }

    private void CheckBlockingObjs()
    {
        if (latestMoveForward.speed > 0)
        {
            spheresList = control.collisionSpheres.rightSpheres;
            dirBlock = 0.3f;

            foreach (GameObject s in control.collisionSpheres.leftSpheres)
            {
                if (blockingObjs.ContainsKey(s))
                {
                    blockingObjs.Remove(s);
                }
            }
        }
        else
        {
            spheresList = control.collisionSpheres.leftSpheres;
            dirBlock = -0.3f;

            foreach (GameObject s in control.collisionSpheres.rightSpheres)
            {
                if (blockingObjs.ContainsKey(s))
                {
                    blockingObjs.Remove(s);
                }
            }
        }

        if (control.IsMovingUpward())
        {
            spheresList = control.collisionSpheres.frontSpheres;
            dirBlock = 0.3f;

            foreach (GameObject s in control.collisionSpheres.backSpheres)
            {
                if (blockingObjs.ContainsKey(s))
                {
                    blockingObjs.Remove(s);
                }
            }
        }
        else
        {
            spheresList = control.collisionSpheres.backSpheres;
            dirBlock = -0.3f;

            foreach (GameObject s in control.collisionSpheres.frontSpheres)
            {
                if (blockingObjs.ContainsKey(s))
                {
                    blockingObjs.Remove(s);
                }
            }
        }

        foreach (GameObject o in spheresList)
        {
            RaycastHit hit;

            if (control.IsMovingUpward())
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * dirBlock, Color.yellow);

                if (Physics.Raycast(o.transform.position, control.transform.forward * dirBlock,
                    out hit, latestMoveForward.blockDistance))
                {
                    if (!IsPlayerAttackBox(hit.collider))
                    {
                        if (blockingObjs.ContainsKey(o))
                        {
                            blockingObjs[o] = hit.collider.transform.root.gameObject;
                        }
                        else
                        {
                            blockingObjs.Add(o, hit.collider.transform.root.gameObject);
                        }
                    }
                    else
                    {
                        if (blockingObjs.ContainsKey(o))
                        {
                            blockingObjs.Remove(o);
                        }
                    }
                    // THIS PART IS MOVEMENT RELATED, SHALL BE REMOVED REPEATED BELOW
                    if (hit.collider.gameObject.GetComponent<CharacterControl>().isEnemy)
                    {
                        float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                        if (currentDistance < 2f)
                        {
                            Vector3 dist = control.transform.position - hit.transform.position;
                            control.transform.position += Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.right * dirBlock * 3.33f;
                            break;
                        }
                    }
                }
                else
                {
                    if (blockingObjs.ContainsKey(o))
                    {
                        blockingObjs.Remove(o);
                    }
                }
            }

            Debug.DrawRay(o.transform.position, control.transform.right * dirBlock, Color.yellow);

            if (Physics.Raycast(o.transform.position, control.transform.right * dirBlock,
                out hit, latestMoveForward.blockDistance))
            {
                if (!IsPlayerAttackBox(hit.collider))
                {
                    if (blockingObjs.ContainsKey(o))
                    {
                        blockingObjs[o] = hit.collider.transform.root.gameObject;
                    }
                    else
                    {
                        blockingObjs.Add(o, hit.collider.transform.root.gameObject);
                    }
                }
                else
                {
                    if (blockingObjs.ContainsKey(o))
                    {
                        blockingObjs.Remove(o);
                    }
                }
                // THIS PART IS MOVEMENT RELATED, SHALL BE REMOVED, REPEATED ABOVE
                if (hit.collider.gameObject.GetComponent<CharacterControl>().isEnemy)
                {
                    float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                    if (currentDistance < 2f)
                    {
                        Vector3 dist = control.transform.position - hit.transform.position;
                        control.transform.position += Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.forward * dirBlock * 3.33f;
                        break;
                    }
                }
            }
            else
            {
                if (blockingObjs.ContainsKey(o))
                {
                    blockingObjs.Remove(o);
                }
            }


        }
    }

    // IGNORE SELF BOX 
    private bool IsPlayerAttackBox(Collider col)
    {
        //// not body part, it is the root, character control itself
        //if (control.gameObject == col.gameObject)
        //{
        //    return false;
        //}

        if (col.transform.root.gameObject == control.gameObject)
        {
            return true;
        }

        //CharacterControl target = col.transform.root.GetComponent<CharacterControl>();
        CharacterControl target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

        if (target == null)
        {
            return false;
        }

        if (control.collisionSpheres.childs.Contains(col.transform))
        {
            return true;
        }

        if (target.damageDetector.damageTaken > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRunning(System.Type type)
    {
        foreach (KeyValuePair<StateData, int> data in CurrentRunningAbilities)
        {
            if (data.Key.GetType() == type)
            {
                return true;
            }
        }

        return false;
    }

    public bool RightSideIsBlocked()
    {
        foreach (KeyValuePair<GameObject, GameObject> data in blockingObjs)
        {
            if ((data.Value.transform.position - control.transform.position).x > 0f)
            {
                return true;
            }
        }

        return false;
    }

    public bool LeftSideIsBlocked()
    {
        foreach (KeyValuePair<GameObject, GameObject> data in blockingObjs)
        {
            if ((data.Value.transform.position - control.transform.position).x < 0f)
            {
                return true;
            }
        }

        return false;
    }

    public bool UpSideIsBlocked()
    {
        foreach (KeyValuePair<GameObject, GameObject> data in blockingObjs)
        {
            if ((data.Value.transform.position - control.transform.position).z > 0f)
            {
                return true;
            }
        }

        return false;
    }

    public bool DownSideIsBlocked()
    {
        foreach (KeyValuePair<GameObject, GameObject> data in blockingObjs)
        {
            if ((data.Value.transform.position - control.transform.position).z < 0f)
            {
                return true;
            }
        }

        return false;
    }
}