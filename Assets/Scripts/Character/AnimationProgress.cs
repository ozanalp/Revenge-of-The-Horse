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

    [Header("Damage Info")]
    public Attack attack;
    public CharacterControl attacker;
    public GameObject attackingBox;

    [Header("Movement")]
    [HideInInspector] public List<GameObject> spheresList;
    private float hrzBlock;
    private float vrtBlock;

    [Header("AirControl")]
    public float momentum;

    [Header("Colliding Objects")]
    public Dictionary<TriggerDetector, List<Collider>> collidingAttackBoxes =
            new Dictionary<TriggerDetector, List<Collider>>();
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
            kickAttackTriggered = false;
            punchAttackTriggered = false;
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
        //if (latestMoveForward.speed > 0)
        if (control.moveRight && !control.moveLeft || !control.moveRight && control.moveLeft)
        {
            spheresList = control.collisionSpheres.frontSpheres;
            hrzBlock = 0.3f;

            foreach (GameObject s in control.collisionSpheres.backSpheres)
            {
                if (blockingObjs.ContainsKey(s))
                {
                    blockingObjs.Remove(s);
                }
            }
        }

        if (control.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            if (control.IsMovingUp())
            {
                spheresList = control.collisionSpheres.forwardSpheres;
                vrtBlock = 0.3f;

                foreach (GameObject s in control.collisionSpheres.backwardSpheres)
                {
                    if (blockingObjs.ContainsKey(s))
                    {
                        blockingObjs.Remove(s);
                    }
                }
            }
            else if (control.IsMovingDown())
            {
                spheresList = control.collisionSpheres.backwardSpheres;
                vrtBlock = -0.3f;

                foreach (GameObject s in control.collisionSpheres.forwardSpheres)
                {
                    if (blockingObjs.ContainsKey(s))
                    {
                        blockingObjs.Remove(s);
                    }
                }
            }
        }
        else if (control.transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            if (control.IsMovingUp())
            {
                spheresList = control.collisionSpheres.backwardSpheres;
                vrtBlock = -0.3f;

                foreach (GameObject s in control.collisionSpheres.forwardSpheres)
                {
                    if (blockingObjs.ContainsKey(s))
                    {
                        blockingObjs.Remove(s);
                    }
                }
            }
            else if (control.IsMovingDown())
            {
                spheresList = control.collisionSpheres.forwardSpheres;
                vrtBlock = 0.3f;

                foreach (GameObject s in control.collisionSpheres.backwardSpheres)
                {
                    if (blockingObjs.ContainsKey(s))
                    {
                        blockingObjs.Remove(s);
                    }
                }
            }
        }

        foreach (GameObject o in spheresList)
        {
            if (control.moveRight && !control.moveLeft || !control.moveRight && control.moveLeft)
            {
                Debug.DrawRay(o.transform.position, control.transform.right * hrzBlock, Color.red);

                if (Physics.Raycast(o.transform.position, control.transform.right * hrzBlock,
                    out RaycastHit hit, latestMoveForward.blockDistance))
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
                }
                else
                {
                    if (blockingObjs.ContainsKey(o))
                    {
                        blockingObjs.Remove(o);
                    }
                }
            }

            if (control.IsMovingUp() || control.IsMovingDown())
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * vrtBlock, Color.yellow);

                if (Physics.Raycast(o.transform.position, control.transform.forward * vrtBlock,
                    out RaycastHit hit, latestMoveForward.blockDistance))
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