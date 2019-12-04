using UnityEngine;

public class ManualInput : MonoBehaviour
{
    private CharacterControl characterControl;

    private void Start()
    {
        characterControl = GetComponent<CharacterControl>();
    }

    private void Update()
    {
        if (VirtualInputManager.Instance.doubleSpeed)
        {
            characterControl.doubleSpeed = true;
        }
        else
        {
            characterControl.doubleSpeed = false;
        }
        if (VirtualInputManager.Instance.moveRight)
        {
            characterControl.moveRight = true;
        }
        else
        {
            characterControl.moveRight = false;
        }
        if (VirtualInputManager.Instance.moveLeft)
        {
            characterControl.moveLeft = true;
        }
        else
        {
            characterControl.moveLeft = false;
        }
        if (VirtualInputManager.Instance.moveUp)
        {
            characterControl.moveUp = true;
        }
        else
        {
            characterControl.moveUp = false;
        }
        if (VirtualInputManager.Instance.moveDown)
        {
            characterControl.moveDown = true;
        }
        else
        {
            characterControl.moveDown = false;
        }
        if (VirtualInputManager.Instance.attempt)
        {
            characterControl.attempt = true;
        }
        else
        {
            characterControl.attempt = false;
        }
        if (VirtualInputManager.Instance.grab)
        {
            characterControl.grab = true;
        }
        else
        {
            characterControl.grab = false;
        }
        if (VirtualInputManager.Instance.hump)
        {
            characterControl.hump = true;
        }
        else
        {
            characterControl.hump = false;
        }
        if (VirtualInputManager.Instance.l_punch)
        {
            characterControl.l_punch = true;
        }
        else
        {
            characterControl.l_punch = false;
        }
        if (VirtualInputManager.Instance.l_kick)
        {
            characterControl.l_kick = true;
        }
        else
        {
            characterControl.l_kick = false;
        }
        if (VirtualInputManager.Instance.h_punch)
        {
            characterControl.h_punch = true;
        }
        else
        {
            characterControl.h_punch = false;
        }
        if (VirtualInputManager.Instance.h_kick)
        {
            characterControl.h_kick = true;
        }
        else
        {
            characterControl.h_kick = false;
        }
    }
}
