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

    #region Moved to CollisionSpheres.cs
    //private static void SetupColliders(BoxCollider box, out float bottom, out float top, out float front, out float back, out float right, out float left)
    //{
    //    bottom = box.bounds.center.y - box.bounds.extents.y;
    //    top = box.bounds.center.y + box.bounds.extents.y;
    //    front = box.bounds.center.z + box.bounds.extents.z;
    //    back = box.bounds.center.z - box.bounds.extents.z;
    //    right = box.bounds.center.x + box.bounds.extents.x;
    //    left = box.bounds.center.x - box.bounds.extents.x;
    //}

    //#region Place Edge Sphere
    //public void PlaceEdgeSphere(float bottom, float top, float front, float back, float left, float right)
    //{
    //    ParentingSpheres();

    //    for (int i = 0; i < 24; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        frontSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(7);
    //    }
    //    RepositionFrontSpheres();

    //    for (int i = 0; i < 24; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        backSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(8);
    //    }
    //    RepositionBackSpheres();

    //    for (int i = 0; i < 24; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        rightSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(6);
    //    }
    //    RepositionRightSpheres();

    //    for (int i = 0; i < 24; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        leftSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(5);
    //    }
    //    RepositionLeftSpheres();

    //    for (int i = 0; i < 4; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        bottomSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(4);
    //    }
    //    RepositionBottomSpheres();

    //    for (int i = 0; i < 4; i++)
    //    {
    //        GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
    //        topSpheres.Add(obj);
    //        obj.transform.parent = gameObject.transform.GetChild(3);
    //    }
    //    RepositionTopSpheres();
    //}

    //private void ParentingSpheres()
    //{
    //    foreach (GameObject go in topSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(3);
    //    }
    //    foreach (GameObject go in bottomSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(4);
    //    }
    //    foreach (GameObject go in leftSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(5);
    //    }
    //    foreach (GameObject go in rightSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(6);
    //    }
    //    foreach (GameObject go in frontSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(7);
    //    }
    //    foreach (GameObject go in backSpheres)
    //    {
    //        go.transform.parent = gameObject.transform.GetChild(8);
    //    }
    //}
    //#endregion

    //public void RemoveSpheres(List<GameObject> spheres)
    //{
    //    foreach (GameObject go in spheres)
    //    {
    //        go.SetActive(false);
    //    }
    //}

    ////#87 PLACING THE SKILL (UPDATEBOXCOLLIDER)
    //#region Repositioning Spheres
    //public void RepositionFrontSpheres()
    //{
    //    float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
    //    float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
    //    float front = boxCollider.bounds.center.z + boxCollider.bounds.extents.z;
    //    float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
    //    float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
    //    float sizeX = boxCollider.bounds.size.x;
    //    float sizeY = boxCollider.bounds.size.y;

    //    frontSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, front) - transform.position;
    //    frontSpheres[1].transform.localPosition = new Vector3(left, top, front) - transform.position;
    //    frontSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, front) - transform.position;
    //    frontSpheres[3].transform.localPosition = new Vector3(right, top, front) - transform.position;

    //    float intervalVertical = (sizeY + 0.05f) / 9;
    //    float intervalHorizontal = (sizeX / 3);

    //    for (int i = 4; i < frontSpheres.Count; i++)
    //    {
    //        frontSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 3)), front) - transform.position;
    //    }
    //    for (int i = 12; i < frontSpheres.Count; i++)
    //    {
    //        frontSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 11)), front) - transform.position;
    //    }
    //    for (int i = 20; i < frontSpheres.Count; i++)
    //    {
    //        frontSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 19)), bottom + 0.05f, front) - transform.position;
    //    }
    //    for (int i = 22; i < frontSpheres.Count; i++)
    //    {
    //        frontSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 21)), top, front) - transform.position;
    //    }
    //}

    //public void RepositionBackSpheres()
    //{
    //    float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
    //    float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
    //    float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;
    //    float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
    //    float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
    //    float sizeX = boxCollider.bounds.size.x;
    //    float sizeY = boxCollider.bounds.size.y;

    //    backSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, back) - transform.position;
    //    backSpheres[1].transform.localPosition = new Vector3(left, top, back) - transform.position;
    //    backSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, back) - transform.position;
    //    backSpheres[3].transform.localPosition = new Vector3(right, top, back) - transform.position;

    //    float intervalVertical = (sizeY + 0.05f) / 9;
    //    float intervalHorizontal = (sizeX / 3);

    //    for (int i = 4; i < backSpheres.Count; i++)
    //    {
    //        backSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 3)), back) - transform.position;
    //    }
    //    for (int i = 12; i < backSpheres.Count; i++)
    //    {
    //        backSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 11)), back) - transform.position;
    //    }
    //    for (int i = 20; i < backSpheres.Count; i++)
    //    {
    //        backSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 19)), bottom + 0.05f, back) - transform.position;
    //    }
    //    for (int i = 22; i < backSpheres.Count; i++)
    //    {
    //        backSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 21)), top, back) - transform.position;
    //    }
    //}

    //public void RepositionRightSpheres()
    //{
    //    float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
    //    float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
    //    float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;
    //    float front = boxCollider.bounds.center.z + boxCollider.bounds.extents.z;
    //    float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
    //    float sizeY = boxCollider.bounds.size.y;
    //    float sizeZ = boxCollider.bounds.size.z;

    //    rightSpheres[0].transform.localPosition = new Vector3(right, bottom + 0.05f, front) - transform.position;
    //    rightSpheres[1].transform.localPosition = new Vector3(right, top, front) - transform.position;
    //    rightSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, back) - transform.position;
    //    rightSpheres[3].transform.localPosition = new Vector3(right, top, back) - transform.position;

    //    float intervalVertical = (sizeY + 0.05f) / 9;
    //    float intervalDepth = sizeZ / 3;
    //    for (int i = 4; i < rightSpheres.Count; i++)
    //    {
    //        rightSpheres[i].transform.localPosition = new Vector3(right, bottom + 0.05f, front - (intervalDepth * (i - 3))) - transform.position;
    //    }
    //    for (int i = 6; i < rightSpheres.Count; i++)
    //    {
    //        rightSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 5)), front) - transform.position;
    //    }
    //    for (int i = 14; i < rightSpheres.Count; i++)
    //    {
    //        rightSpheres[i].transform.localPosition = new Vector3(right, top, front - (intervalDepth * (i - 13))) - transform.position;
    //    }
    //    for (int i = 16; i < rightSpheres.Count; i++)
    //    {
    //        rightSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 15)), back) - transform.position;
    //    }
    //}

    //public void RepositionLeftSpheres()
    //{
    //    float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
    //    float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
    //    float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;
    //    float front = boxCollider.bounds.center.z + boxCollider.bounds.extents.z;
    //    float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
    //    float sizeY = boxCollider.bounds.size.y;
    //    float sizeZ = boxCollider.bounds.size.z;

    //    leftSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, front) - transform.position;
    //    leftSpheres[1].transform.localPosition = new Vector3(left, top, front) - transform.position;
    //    leftSpheres[2].transform.localPosition = new Vector3(left, bottom + 0.05f, back) - transform.position;
    //    leftSpheres[3].transform.localPosition = new Vector3(left, top, back) - transform.position;

    //    float intervalVertical = (sizeY + 0.05f) / 9;
    //    float intervalDepth = sizeZ / 3;
    //    for (int i = 4; i < leftSpheres.Count; i++)
    //    {
    //        leftSpheres[i].transform.localPosition = new Vector3(left, bottom + 0.05f, front - (intervalDepth * (i - 3))) - transform.position;
    //    }
    //    for (int i = 6; i < leftSpheres.Count; i++)
    //    {
    //        leftSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 5)), front) - transform.position;
    //    }
    //    for (int i = 14; i < leftSpheres.Count; i++)
    //    {
    //        leftSpheres[i].transform.localPosition = new Vector3(left, top, front - (intervalDepth * (i - 13))) - transform.position;
    //    }
    //    for (int i = 16; i < leftSpheres.Count; i++)
    //    {
    //        leftSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 15)), back) - transform.position;
    //    }
    //}

    //public void RepositionBottomSpheres()
    //{
    //    float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
    //    float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
    //    float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
    //    float sizeX = boxCollider.bounds.size.x;

    //    bottomSpheres[0].transform.localPosition = new Vector3(left, bottom, 0) - transform.position;
    //    bottomSpheres[1].transform.localPosition = new Vector3(right, bottom, 0) - transform.position;

    //    float intervalHorizontal = (sizeX / 3);

    //    for (int i = 2; i < bottomSpheres.Count; i++)
    //    {
    //        bottomSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 1)), bottom, 0) - transform.position;
    //    }
    //}

    //public void RepositionTopSpheres()
    //{
    //    float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
    //    float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
    //    float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
    //    float sizeX = boxCollider.bounds.size.x;

    //    topSpheres[0].transform.localPosition = new Vector3(left, top, 0) - transform.position;
    //    topSpheres[1].transform.localPosition = new Vector3(right, top, 0) - transform.position;

    //    float intervalHorizontal = (sizeX / 3);

    //    for (int i = 2; i < topSpheres.Count; i++)
    //    {
    //        topSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 1)), top, 0) - transform.position;
    //    }
    //}
    //#endregion


    //private void RightSideSpheresDrawRay()
    //{
    //    foreach (GameObject o in rightSpheres)
    //    {
    //        Debug.DrawRay(o.transform.position, o.transform.TransformDirection(Vector3.right) * 0.7f, Color.yellow);
    //    }
    //}

    //public void FindEveryChild(Transform parent)
    //{
    //    int count = parent.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        Transform child = parent.GetChild(i);

    //        childs.Add(child);

    //        if (child.childCount > 0)
    //        {
    //            FindEveryChild(child);
    //        }

    //        if (!child.gameObject.name.Contains("ColliderEdge"))
    //        {
    //            childs.Remove(child);
    //        }
    //    }

    //    //Debug.Log(childs.Count);
    //}
    #endregion

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
        else
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
        else
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