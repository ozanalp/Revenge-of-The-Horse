using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public List<Collider> collidingBoxes = new List<Collider>();
    public CharacterControl control;

    public Vector3 lastPosition;
    public Quaternion lastRotation;

    private void Awake()
    {
        control = GetComponentInParent<CharacterControl>();
    }

    private void OnTriggerEnter(Collider col)
    {
        CheckCollidingAttackingBox(col);
    }

    private void CheckCollidingAttackingBox(Collider col)
    {
        if (control.collisionSpheres.childs.Contains(col.transform))
        {
            return;
        }

        CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

        if (attacker == null)
        {
            return;
        }

        if (col.gameObject == gameObject)
        {
            return;
        }

        //if (!collidingBoxes.Contains(col))
        //{
        //    collidingBoxes.Add(col);
        //}

        if (!control.animationProgress.collidingAttackBoxes.ContainsKey(this))
        {
            control.animationProgress.collidingAttackBoxes.Add(this, new List<Collider>());
        }

        if (!control.animationProgress.collidingAttackBoxes[this].Contains(col))
        {
            control.animationProgress.collidingAttackBoxes[this].Add(col);
        }
    }

    private void Update()
    {
        for (int i = 0; i < collidingBoxes.Count; i++)
        {
            if (!collidingBoxes[i].gameObject.activeInHierarchy)
            {
                collidingBoxes.RemoveAt(i);
            }
        }
    }
}