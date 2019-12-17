using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public List<Collider> collidingBoxes = new List<Collider>();
    public CharacterControl control;

    private void Awake()
    {
        control = GetComponentInParent<CharacterControl>();
    }

    private void OnTriggerEnter(Collider col)
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

        if (!collidingBoxes.Contains(col))
        {
            collidingBoxes.Add(col);
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