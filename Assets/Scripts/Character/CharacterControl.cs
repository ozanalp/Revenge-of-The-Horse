using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TransitionParameter
{
    Move, Grab, Hump, TransitionIndex, ForceTransition, L_Punch, L_Kick, H_Punch, H_Kick, DoubleSpeed, Turn,
}

public class CharacterControl : MonoBehaviour
{
    public bool moveRight, moveLeft, moveUp, moveDown, attempt, grab, hump, l_punch, l_kick, h_punch, h_kick, doubleSpeed, jump;

    [Space(10)]
    [Header("Subcomponents")]
    public Animator animator;
    public AnimationProgress animationProgress;
    public AIProgress aiProgress;
    public AIController aiController;
    public DamageDetector damageDetector;
    public BoxCollider boxCollider;
    public NavMeshObstacle obstacle;
    public CollisionSpheres collisionSpheres;

    [Space(10)]
    [Header("Character")]
    public PlayableCharacterType playableCharacterType;
    public bool isEnemy;

    [Space(10)]
    [Header("Attack Boxes")]
    public GameObject l_punchBox;
    public GameObject l_kickBox;

    private List<TriggerDetector> triggerDetectors = new List<TriggerDetector>();

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }
            return rigid;
        }
    }

    private void Awake()
    {
        animationProgress = GetComponent<AnimationProgress>();
        aiProgress = GetComponentInChildren<AIProgress>();
        damageDetector = GetComponentInChildren<DamageDetector>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();

        collisionSpheres = GetComponentInChildren<CollisionSpheres>();
        collisionSpheres.owner = this;
        CollisionSpheres.SetupColliders(boxCollider, out float bottom, out float top, out float front, out float back, out float right, out float left);
        collisionSpheres.PlaceEdgeSphere(bottom, top, front, back, left, right);
        collisionSpheres.FindEveryChild(gameObject.transform);

        aiController = GetComponentInChildren<AIController>();
        if (aiController == null)
        {
            if (obstacle != null)
            {
                obstacle.carving = true;
            }
        }

        RegisterCharacter();
    }

    public List<TriggerDetector> GetAllTriggers()
    {
        if (triggerDetectors.Count == 0)
        {
            TriggerDetector[] arr = gameObject.GetComponentsInChildren<TriggerDetector>();

            foreach (TriggerDetector d in arr)
            {
                triggerDetectors.Add(d);
            }
        }

        return triggerDetectors;
    }

    #region Moved to the TriggerDetector.cs
    //private void OnTriggerEnter(Collider col)
    //{
    //    if (childs.Contains(col.transform))
    //    {
    //        return;
    //    }

    //    CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

    //    if (control == null)
    //    {
    //        return;
    //    }

    //    if (col.gameObject == gameObject)
    //    {
    //        return;
    //    }

    //    if (!collidingBoxes.Contains(col))
    //    {
    //        collidingBoxes.Add(col);
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    for (int i = 0; i < collidingBoxes.Count; i++)
    //    {
    //        if (!collidingBoxes[i].gameObject.activeInHierarchy)
    //        {
    //            collidingBoxes.RemoveAt(i);
    //        }
    //    }
    //}
    #endregion

    public void UpdateBoxColliderSize()
    {
        if (!animationProgress.updatingBoxCollider)
        {
            return;
        }

        if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.targetSize) > 0.01f)
        {
            boxCollider.size = Vector3.Lerp(boxCollider.size, animationProgress.targetSize,
                Time.deltaTime * animationProgress.size_Speed);

            animationProgress.updatingSpheres = true;
        }
    }

    public void UpdateBoxColliderCenter()
    {
        if (!animationProgress.updatingBoxCollider)
        {
            return;
        }

        if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.targetCenter) > 0.01f)
        {
            boxCollider.size = Vector3.Lerp(boxCollider.center, animationProgress.targetCenter,
                Time.deltaTime * animationProgress.center_Speed);

            animationProgress.updatingSpheres = true;
        }
    }

    private void FixedUpdate()
    {
        animationProgress.updatingSpheres = false;
        UpdateBoxColliderCenter();
        UpdateBoxColliderSize();

        if (animationProgress.updatingSpheres)
        {
            collisionSpheres.RepositionBackSpheres();
            collisionSpheres.RepositionBottomSpheres();
            collisionSpheres.RepositionFrontSpheres();
            collisionSpheres.RepositionLeftSpheres();
            collisionSpheres.RepositionRightSpheres();
            collisionSpheres.RepositionTopSpheres();
        }
    }

    private void Update()
    {
        //collisionSpheres.RightSideSpheresDrawRay();

        //if (attempt)
        //{
        //    animator.SetBool("Attempt", true);
        //}
        //else
        //{
        //    animator.SetBool("Attempt", false);
        //}
    }

    public void MoveForward(float speed, float speedGraph)
    {
        transform.Translate(Vector3.right * speed * speedGraph * Time.deltaTime);
    }

    public void FaceForward(bool forward)
    {
        if (forward)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public bool IsFacingForward()
    {
        if (transform.right.x > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMovingUp()
    {
        if (moveUp)
        {
            return true;
        }
        //else
        {
            return false;
        }
    }

    public bool IsMovingDown()
    {
        if (moveDown)
        {
            return true;
        }
        //else
        {
            return false;
        }
    }

    private void RegisterCharacter()
    {
        if (!CharacterManager.Instance.characters.Contains(this))
        {
            CharacterManager.Instance.characters.Add(this);
        }
    }

    public void CacheCharacterControl(Animator animator)
    {
        CharacterState[] arr = animator.GetBehaviours<CharacterState>();

        foreach (CharacterState c in arr)
        {
            c.characterControl = this;
        }
    }
}