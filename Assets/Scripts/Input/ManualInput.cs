using System.Collections.Generic;
using UnityEngine;

public class ManualInput : MonoBehaviour
{
    public List<InputKeyType> doubleTaps = new List<InputKeyType>();

    private CharacterControl characterControl;
    private List<InputKeyType> upKeys = new List<InputKeyType>();
    private Dictionary<InputKeyType, float> dicDoubleTapTimings = new Dictionary<InputKeyType, float>();

    private void Start()
    {
        characterControl = GetComponent<CharacterControl>();
    }

    private void Update()
    {
        if (VirtualInputManager.Instance.doubleSpeed)
        {
            characterControl.doubleSpeed = true;
            ProcDoubleTap(InputKeyType.KEY_DOUBLE_SPEED);
        }
        else
        {
            characterControl.doubleSpeed = false;
            RemoveDoubleTap(InputKeyType.KEY_DOUBLE_SPEED);
        }

        if (VirtualInputManager.Instance.moveRight)
        {
            characterControl.moveRight = true;
            ProcDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
        }
        else
        {
            characterControl.moveRight = false;
            RemoveDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
        }

        if (VirtualInputManager.Instance.moveLeft)
        {
            characterControl.moveLeft = true;
            ProcDoubleTap(InputKeyType.KEY_MOVE_LEFT);
        }
        else
        {
            characterControl.moveLeft = false;
            RemoveDoubleTap(InputKeyType.KEY_MOVE_LEFT);
        }

        if (VirtualInputManager.Instance.moveUp)
        {
            characterControl.moveUp = true;
            ProcDoubleTap(InputKeyType.KEY_MOVE_UP);
        }
        else
        {
            characterControl.moveUp = false;
            RemoveDoubleTap(InputKeyType.KEY_MOVE_UP);
        }

        if (VirtualInputManager.Instance.moveDown)
        {
            characterControl.moveDown = true;
            ProcDoubleTap(InputKeyType.KEY_MOVE_DOWN);
        }
        else
        {
            characterControl.moveDown = false;
            RemoveDoubleTap(InputKeyType.KEY_MOVE_DOWN);
        }

        if (VirtualInputManager.Instance.attempt)
        {
            characterControl.attempt = true;
            ProcDoubleTap(InputKeyType.KEY_ATTEMPT);
        }
        else
        {
            characterControl.attempt = false;
            RemoveDoubleTap(InputKeyType.KEY_ATTEMPT);
        }

        if (VirtualInputManager.Instance.grab)
        {
            characterControl.grab = true;
            ProcDoubleTap(InputKeyType.KEY_GRAB);
        }
        else
        {
            characterControl.grab = false;
            RemoveDoubleTap(InputKeyType.KEY_GRAB);
        }

        if (VirtualInputManager.Instance.hump)
        {
            characterControl.hump = true;
            ProcDoubleTap(InputKeyType.KEY_HUMP);
        }
        else
        {
            characterControl.hump = false;
            RemoveDoubleTap(InputKeyType.KEY_HUMP);
        }

        if (VirtualInputManager.Instance.l_punch)
        {
            characterControl.l_punch = true;
            ProcDoubleTap(InputKeyType.KEY_LIGHT_PUNCH);
        }
        else
        {
            characterControl.l_punch = false;
            RemoveDoubleTap(InputKeyType.KEY_LIGHT_PUNCH);
        }

        if (VirtualInputManager.Instance.l_kick)
        {
            characterControl.l_kick = true;
            ProcDoubleTap(InputKeyType.KEY_LIGHT_KICK);
        }
        else
        {
            characterControl.l_kick = false;
            RemoveDoubleTap(InputKeyType.KEY_LIGHT_KICK);
        }

        if (VirtualInputManager.Instance.h_punch)
        {
            characterControl.h_punch = true;
            ProcDoubleTap(InputKeyType.KEY_HEAVY_PUNCH);
        }
        else
        {
            characterControl.h_punch = false;
            RemoveDoubleTap(InputKeyType.KEY_HEAVY_PUNCH);
        }

        if (VirtualInputManager.Instance.h_kick)
        {
            characterControl.h_kick = true;
            ProcDoubleTap(InputKeyType.KEY_HEAVY_KICK);
        }
        else
        {
            characterControl.h_kick = false;
            RemoveDoubleTap(InputKeyType.KEY_HEAVY_KICK);
        }

        //double tap running
        if (doubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
            doubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT) ||
            doubleTaps.Contains(InputKeyType.KEY_MOVE_DOWN) ||
            doubleTaps.Contains(InputKeyType.KEY_MOVE_UP))
        {
            characterControl.doubleSpeed = true;
        }

        //double tap running turn
        if (characterControl.moveRight && characterControl.moveLeft)
        {
            if (doubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
                doubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
            {
                if (!doubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
                {
                    doubleTaps.Add(InputKeyType.KEY_MOVE_RIGHT);
                }

                if (!doubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                {
                    doubleTaps.Add(InputKeyType.KEY_MOVE_LEFT);
                }
            }
        }
    }

    void ProcDoubleTap(InputKeyType keyType)
    {
        if (!dicDoubleTapTimings.ContainsKey(keyType))
        {
            dicDoubleTapTimings.Add(keyType, 0f);
        }

        if (dicDoubleTapTimings[keyType] == 0f ||
            upKeys.Contains(keyType))
        {
            if (Time.time < dicDoubleTapTimings[keyType])
            {
                if (!doubleTaps.Contains(keyType))
                {
                    doubleTaps.Add(keyType);
                }
            }

            if (upKeys.Contains(keyType))
            {
                upKeys.Remove(keyType);
            }

            dicDoubleTapTimings[keyType] = Time.time + 0.18f;
        }
    }

    void RemoveDoubleTap(InputKeyType keyType)
    {
        if (doubleTaps.Contains(keyType))
        {
            doubleTaps.Remove(keyType);
        }

        if (!upKeys.Contains(keyType))
        {
            upKeys.Add(keyType);
        }
    }
}