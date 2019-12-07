using System.Collections.Generic;
using UnityEngine;

public class CollisionSpheres : MonoBehaviour
{
    public CharacterControl owner;
    public List<GameObject> topSpheres = new List<GameObject>();
    public List<GameObject> bottomSpheres = new List<GameObject>();
    public List<GameObject> rightSpheres = new List<GameObject>();
    public List<GameObject> leftSpheres = new List<GameObject>();
    public List<GameObject> frontSpheres = new List<GameObject>();
    public List<GameObject> backSpheres = new List<GameObject>();
    public List<Transform> childs = new List<Transform>();

    public static void SetupColliders(BoxCollider box, out float bottom, out float top, out float front, out float back, out float right, out float left)
    {
        bottom = box.bounds.center.y - box.bounds.extents.y;
        top = box.bounds.center.y + box.bounds.extents.y;
        front = box.bounds.center.z + box.bounds.extents.z;
        back = box.bounds.center.z - box.bounds.extents.z;
        right = box.bounds.center.x + box.bounds.extents.x;
        left = box.bounds.center.x - box.bounds.extents.x;
    }

    #region Place Edge Sphere
    public void PlaceEdgeSphere(float bottom, float top, float front, float back, float left, float right)
    {
        ParentingSpheres();

        for (int i = 0; i < 24; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            frontSpheres.Add(obj);
            obj.transform.parent = transform.Find("Front");
        }
        RepositionFrontSpheres();

        for (int i = 0; i < 24; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            backSpheres.Add(obj);
            obj.transform.parent = transform.Find("Back");
        }
        RepositionBackSpheres();

        for (int i = 0; i < 28; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            rightSpheres.Add(obj);
            obj.transform.parent = transform.Find("Right");
        }
        RepositionRightSpheres();

        for (int i = 0; i < 28; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            leftSpheres.Add(obj);
            obj.transform.parent = transform.Find("Left");
        }
        RepositionLeftSpheres();

        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            bottomSpheres.Add(obj);
            obj.transform.parent = transform.Find("Bottom");
        }
        RepositionBottomSpheres();

        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            topSpheres.Add(obj);
            obj.transform.parent = transform.Find("Top");
        }
        RepositionTopSpheres();
    }

    private void ParentingSpheres()
    {
        foreach (GameObject go in topSpheres)
        {
            go.transform.parent = transform.Find("Top");
        }
        foreach (GameObject go in bottomSpheres)
        {
            go.transform.parent = transform.Find("Bottom");
        }
        foreach (GameObject go in leftSpheres)
        {
            go.transform.parent = transform.Find("Left");
        }
        foreach (GameObject go in rightSpheres)
        {
            go.transform.parent = transform.Find("Right");
        }
        foreach (GameObject go in frontSpheres)
        {
            go.transform.parent = transform.Find("Front");
        }
        foreach (GameObject go in backSpheres)
        {
            go.transform.parent = transform.Find("Back");
        }
    }
    #endregion

    public void RemoveSpheres(List<GameObject> spheres)
    {
        foreach (GameObject go in spheres)
        {
            go.SetActive(false);
        }
    }

    //#87 PLACING THE SKILL (UPDATEBOXCOLLIDER)
    #region Repositioning Spheres
    public void RepositionFrontSpheres()
    {
        float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
        float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
        float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.extents.z;
        float left = owner.boxCollider.bounds.center.x - owner.boxCollider.bounds.extents.x;
        float right = owner.boxCollider.bounds.center.x + owner.boxCollider.bounds.extents.x;
        float sizeX = owner.boxCollider.bounds.size.x;
        float sizeY = owner.boxCollider.bounds.size.y;

        frontSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, front) - transform.position;
        frontSpheres[1].transform.localPosition = new Vector3(left, top, front) - transform.position;
        frontSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, front) - transform.position;
        frontSpheres[3].transform.localPosition = new Vector3(right, top, front) - transform.position;

        float intervalVertical = (sizeY + 0.05f) / 9;
        float intervalHorizontal = (sizeX / 3);

        for (int i = 4; i < frontSpheres.Count; i++)
        {
            frontSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 3)), front) - transform.position;
        }
        for (int i = 12; i < frontSpheres.Count; i++)
        {
            frontSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 11)), front) - transform.position;
        }
        for (int i = 20; i < frontSpheres.Count; i++)
        {
            frontSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 19)), bottom + 0.05f, front) - transform.position;
        }
        for (int i = 22; i < frontSpheres.Count; i++)
        {
            frontSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 21)), top, front) - transform.position;
        }
    }

    public void RepositionBackSpheres()
    {
        float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
        float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
        float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.extents.z;
        float left = owner.boxCollider.bounds.center.x - owner.boxCollider.bounds.extents.x;
        float right = owner.boxCollider.bounds.center.x + owner.boxCollider.bounds.extents.x;
        float sizeX = owner.boxCollider.bounds.size.x;
        float sizeY = owner.boxCollider.bounds.size.y;

        backSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, back) - transform.position;
        backSpheres[1].transform.localPosition = new Vector3(left, top, back) - transform.position;
        backSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, back) - transform.position;
        backSpheres[3].transform.localPosition = new Vector3(right, top, back) - transform.position;

        float intervalVertical = (sizeY + 0.05f) / 9;
        float intervalHorizontal = (sizeX / 3);

        for (int i = 4; i < backSpheres.Count; i++)
        {
            backSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 3)), back) - transform.position;
        }
        for (int i = 12; i < backSpheres.Count; i++)
        {
            backSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 11)), back) - transform.position;
        }
        for (int i = 20; i < backSpheres.Count; i++)
        {
            backSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 19)), bottom + 0.05f, back) - transform.position;
        }
        for (int i = 22; i < backSpheres.Count; i++)
        {
            backSpheres[i].transform.localPosition = new Vector3(left + (intervalHorizontal * (i - 21)), top, back) - transform.position;
        }
    }

    public void RepositionRightSpheres()
    {
        float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
        float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
        float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.extents.z;
        float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.extents.z;
        float right = owner.boxCollider.bounds.center.x + owner.boxCollider.bounds.extents.x;
        float sizeY = owner.boxCollider.bounds.size.y;
        float sizeZ = owner.boxCollider.bounds.size.z;

        rightSpheres[0].transform.localPosition = new Vector3(right, bottom + 0.05f, front) - transform.position;
        rightSpheres[1].transform.localPosition = new Vector3(right, top, front) - transform.position;
        rightSpheres[2].transform.localPosition = new Vector3(right, bottom + 0.05f, back) - transform.position;
        rightSpheres[3].transform.localPosition = new Vector3(right, top, back) - transform.position;

        float intervalVertical = (sizeY + 0.05f) / 9;
        float intervalDepth = sizeZ / 5;
        for (int i = 4; i < rightSpheres.Count; i++)
        {
            rightSpheres[i].transform.localPosition = new Vector3(right, bottom + 0.05f, front - (intervalDepth * (i - 3))) - transform.position;
        }
        for (int i = 8; i < rightSpheres.Count; i++)
        {
            rightSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 7)), front) - transform.position;
        }
        for (int i = 16; i < rightSpheres.Count; i++)
        {
            rightSpheres[i].transform.localPosition = new Vector3(right, top, front - (intervalDepth * (i - 15))) - transform.position;
        }
        for (int i = 20; i < rightSpheres.Count; i++)
        {
            rightSpheres[i].transform.localPosition = new Vector3(right, bottom + (intervalVertical * (i - 19)), back) - transform.position;
        }
    }

    public void RepositionLeftSpheres()
    {
        float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
        float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
        float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.extents.z;
        float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.extents.z;
        float left = owner.boxCollider.bounds.center.x - owner.boxCollider.bounds.extents.x;
        float sizeY = owner.boxCollider.bounds.size.y;
        float sizeZ = owner.boxCollider.bounds.size.z;

        leftSpheres[0].transform.localPosition = new Vector3(left, bottom + 0.05f, front) - transform.position;
        leftSpheres[1].transform.localPosition = new Vector3(left, top, front) - transform.position;
        leftSpheres[2].transform.localPosition = new Vector3(left, bottom + 0.05f, back) - transform.position;
        leftSpheres[3].transform.localPosition = new Vector3(left, top, back) - transform.position;

        float intervalVertical = (sizeY + 0.05f) / 9;
        float intervalDepth = sizeZ / 5;
        for (int i = 4; i < leftSpheres.Count; i++)
        {
            leftSpheres[i].transform.localPosition = new Vector3(left, bottom + 0.05f, front - (intervalDepth * (i - 3))) - transform.position;
        }
        for (int i = 8; i < leftSpheres.Count; i++)
        {
            leftSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 7)), front) - transform.position;
        }
        for (int i = 16; i < leftSpheres.Count; i++)
        {
            leftSpheres[i].transform.localPosition = new Vector3(left, top, front - (intervalDepth * (i - 15))) - transform.position;
        }
        for (int i = 20; i < leftSpheres.Count; i++)
        {
            leftSpheres[i].transform.localPosition = new Vector3(left, bottom + (intervalVertical * (i - 19)), back) - transform.position;
        }
    }

    public void RepositionTopSpheres()
    {
        float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
        float left = owner.boxCollider.bounds.center.x - owner.boxCollider.bounds.extents.x;
        float right = owner.boxCollider.bounds.center.x + owner.boxCollider.bounds.extents.x;
        float sizeX = owner.boxCollider.bounds.size.x;

        topSpheres[0].transform.localPosition = 
            new Vector3(left - transform.position.x, top - transform.position.y, 0);
        topSpheres[1].transform.localPosition = 
            new Vector3(right - transform.position.x, top - transform.position.y, 0);

        float intervalHorizontal = (sizeX / 3);

        for (int i = 2; i < topSpheres.Count; i++)
        {
            topSpheres[i].transform.localPosition = 
                new Vector3(left + (intervalHorizontal * (i - 1)) - transform.position.x, top - transform.position.y, 0);
        }
    }

    public void RepositionBottomSpheres()
    {
        float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
        float left = owner.boxCollider.bounds.center.x - owner.boxCollider.bounds.extents.x;
        float right = owner.boxCollider.bounds.center.x + owner.boxCollider.bounds.extents.x;
        float sizeX = owner.boxCollider.bounds.size.x;

        bottomSpheres[0].transform.localPosition = 
            new Vector3(left - transform.position.x, bottom - transform.position.y, 0);
        bottomSpheres[1].transform.localPosition = 
            new Vector3(right - transform.position.x, bottom - transform.position.y, 0);

        float intervalHorizontal = (sizeX / 3);

        for (int i = 2; i < bottomSpheres.Count; i++)
        {
            bottomSpheres[i].transform.localPosition = 
                new Vector3(left + (intervalHorizontal * (i - 1)) - transform.position.x, bottom - transform.position.y, 0);
        }
    }
    #endregion

    //public void RightSideSpheresDrawRay()
    //{
    //    foreach (GameObject o in rightSpheres)
    //    {
    //        Debug.DrawRay(o.transform.position, o.transform.TransformDirection(Vector3.right) * 0.7f, Color.yellow);
    //    }
    //}

    public void FindEveryChild(Transform parent)
    {
        int count = parent.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = parent.GetChild(i);

            childs.Add(child);

            if (child.childCount > 0)
            {
                FindEveryChild(child);
            }

            if (!child.gameObject.name.Contains("ColliderEdge"))
            {
                childs.Remove(child);
            }
        }
    }
}