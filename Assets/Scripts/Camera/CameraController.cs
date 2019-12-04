using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraTrigger
{
    Default, Shake,
}
public class CameraController : MonoBehaviour
{
    Animator _animator;
    public Animator animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            return _animator;
        }
    }

    public void TriggerCamera(CameraTrigger trigger)
    {
        animator.SetTrigger(trigger.ToString());
    }
}