using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public SavedKeys savedKeys;

    private void Update()
    {
        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_DOUBLE_SPEED]))
        {
            VirtualInputManager.Instance.doubleSpeed = true;
        }
        else
        {
            VirtualInputManager.Instance.doubleSpeed = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_UP]))
        {
            VirtualInputManager.Instance.moveUp = true;
        }
        else
        {
            VirtualInputManager.Instance.moveUp = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_DOWN]))
        {
            VirtualInputManager.Instance.moveDown = true;
        }
        else
        {
            VirtualInputManager.Instance.moveDown = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_RIGHT]))
        {
            VirtualInputManager.Instance.moveRight = true;
        }
        else
        {
            VirtualInputManager.Instance.moveRight = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_MOVE_LEFT]))
        {
            VirtualInputManager.Instance.moveLeft = true;
        }
        else
        {
            VirtualInputManager.Instance.moveLeft = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_ATTEMPT]))
        {
            VirtualInputManager.Instance.attempt = true;
        }
        else
        {
            VirtualInputManager.Instance.attempt = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_GRAB]))
        {
            VirtualInputManager.Instance.grab = true;
        }
        else
        {
            VirtualInputManager.Instance.grab = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_HUMP]))
        {
            VirtualInputManager.Instance.hump = true;
        }
        else
        {
            VirtualInputManager.Instance.hump = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_HEAVY_PUNCH]))
        {
            VirtualInputManager.Instance.h_punch = true;
        }
        else
        {
            VirtualInputManager.Instance.h_punch = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_LIGHT_PUNCH]))
        {
            VirtualInputManager.Instance.l_punch = true;
        }
        else
        {
            VirtualInputManager.Instance.l_punch = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_HEAVY_KICK]))
        {
            VirtualInputManager.Instance.h_kick = true;
        }
        else
        {
            VirtualInputManager.Instance.h_kick = false;
        }

        if (Input.GetKey(VirtualInputManager.Instance.DicKeys[InputKeyType.KEY_LIGHT_KICK]))
        {
            VirtualInputManager.Instance.l_kick = true;
        }
        else
        {
            VirtualInputManager.Instance.l_kick = false;
        }
    }
}