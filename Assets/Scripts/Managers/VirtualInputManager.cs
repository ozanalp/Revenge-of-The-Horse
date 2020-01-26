using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputKeyType
{
    KEY_MOVE_UP, KEY_MOVE_DOWN, KEY_MOVE_LEFT, KEY_MOVE_RIGHT, KEY_DOUBLE_SPEED,
    KEY_ATTEMPT, KEY_GRAB, KEY_HUMP,
    KEY_LIGHT_PUNCH, KEY_LIGHT_KICK, KEY_HEAVY_PUNCH, KEY_HEAVY_KICK,
}
public class VirtualInputManager : Singleton<VirtualInputManager>
{
    public PlayerInput playerInput;
    public bool moveUp, moveDown, moveLeft, moveRight, attempt, grab, hump, 
        l_punch, l_kick, h_punch, h_kick, doubleSpeed;

    [Header("Custom Key Binding")]
    public bool useCustomKeys;
    [Space(5)]
    public bool bind_MoveUp;
    public bool bind_MoveDown;
    public bool bind_MoveRight;
    public bool bind_MoveLeft;
    public bool bind_Attmept;
    public bool bind_Grab;
    public bool bind_Hump;
    public bool bind_DoubleSpeed;
    public bool bind_LPunch;
    public bool bind_LKick;
    public bool bind_HPunch;
    public bool bind_HKick;


    [Space(10)]
    public Dictionary<InputKeyType, KeyCode> DicKeys = new Dictionary<InputKeyType, KeyCode>();
    public KeyCode[] PossibleKeys;

    private void Awake()
    {
        PossibleKeys = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];

        GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;
        playerInput = obj.GetComponent<PlayerInput>();
    }

    public void LoadKeys()
    {
        if (playerInput.savedKeys.KeyCodesList.Count > 0)
        {
            foreach (KeyCode k in playerInput.savedKeys.KeyCodesList)
            {
                if (k == KeyCode.None)
                {
                    SetDefaultKeys();
                    break;
                }
            }
        }
        else
        {
            SetDefaultKeys();
        }

        for (int i = 0; i < playerInput.savedKeys.KeyCodesList.Count; i++)
        {
            DicKeys[(InputKeyType)i] = playerInput.savedKeys.KeyCodesList[i];
        }
    }

    public void SaveKeys()
    {
        playerInput.savedKeys.KeyCodesList.Clear();

        int count = System.Enum.GetValues(typeof(InputKeyType)).Length;

        for (int i = 0; i < count; i++)
        {
            playerInput.savedKeys.KeyCodesList.Add(DicKeys[(InputKeyType)i]);
        }
    }

    public void SetDefaultKeys()
    {
        DicKeys.Clear();

        DicKeys.Add(InputKeyType.KEY_MOVE_LEFT, KeyCode.LeftArrow);
        DicKeys.Add(InputKeyType.KEY_MOVE_RIGHT, KeyCode.RightArrow);
        DicKeys.Add(InputKeyType.KEY_MOVE_UP, KeyCode.UpArrow);
        DicKeys.Add(InputKeyType.KEY_MOVE_DOWN, KeyCode.DownArrow);
        DicKeys.Add(InputKeyType.KEY_DOUBLE_SPEED, KeyCode.LeftShift);

        DicKeys.Add(InputKeyType.KEY_ATTEMPT, KeyCode.Tab);
        DicKeys.Add(InputKeyType.KEY_HUMP, KeyCode.Space);
        DicKeys.Add(InputKeyType.KEY_LIGHT_KICK, KeyCode.W);
        DicKeys.Add(InputKeyType.KEY_LIGHT_PUNCH, KeyCode.Q);
        DicKeys.Add(InputKeyType.KEY_GRAB, KeyCode.Alpha1);
        DicKeys.Add(InputKeyType.KEY_HEAVY_PUNCH, KeyCode.Alpha2);
        DicKeys.Add(InputKeyType.KEY_HEAVY_KICK, KeyCode.S);

        SaveKeys();
    }

    private void Update()
    {
        //if (!UseCustomKeys)
        //{
        //    return;
        //}

        if (useCustomKeys)
        {
            if (bind_MoveUp)
            {
                if (KeyIsChanged(InputKeyType.KEY_MOVE_UP))
                {
                    bind_MoveUp = false;
                }
            }

            if (bind_MoveDown)
            {
                if (KeyIsChanged(InputKeyType.KEY_MOVE_DOWN))
                {
                    bind_MoveDown = false;
                }
            }

            if (bind_MoveRight)
            {
                if (KeyIsChanged(InputKeyType.KEY_MOVE_RIGHT))
                {
                    bind_MoveRight = false;
                }
            }

            if (bind_MoveLeft)
            {
                if (KeyIsChanged(InputKeyType.KEY_MOVE_LEFT))
                {
                    bind_MoveLeft = false;
                }
            }

            if (bind_DoubleSpeed)
            {
                if (KeyIsChanged(InputKeyType.KEY_DOUBLE_SPEED))
                {
                    bind_DoubleSpeed = false;
                }
            }

            if (bind_Attmept)
            {
                if (KeyIsChanged(InputKeyType.KEY_ATTEMPT))
                {
                    bind_Attmept = false;
                }
            }

            //if (bind_Grab)
            //{
            //    if (KeyIsChanged(InputKeyType.KEY_GRAB))
            //    {
            //        bind_Grab = false;
            //    }
            //}

            if (bind_Hump)
            {
                if (KeyIsChanged((InputKeyType.KEY_HUMP)))
                {
                    bind_Hump = false;
                }
            }
            
            if (bind_LPunch)
            {
                if (KeyIsChanged((InputKeyType.KEY_LIGHT_PUNCH)))
                {
                    bind_LPunch = false;
                }
            }
            
            //if (bind_HPunch)
            //{
            //    if (KeyIsChanged((InputKeyType.KEY_HEAVY_PUNCH)))
            //    {
            //        bind_HPunch = false;
            //    }
            //}
            
            if (bind_LKick)
            {
                if (KeyIsChanged((InputKeyType.KEY_LIGHT_KICK)))
                {
                    bind_LKick = false;
                }
            }
            
            //if (bind_HKick)
            //{
            //    if (KeyIsChanged((InputKeyType.KEY_HEAVY_KICK)))
            //    {
            //        bind_HKick = false;
            //    }
            //}
        }
    }

    void SetCustomKey(InputKeyType inputKey, KeyCode key)
    {
        Debug.Log("key changed: " + inputKey.ToString() + " -> " + key.ToString());

        if (!DicKeys.ContainsKey(inputKey))
        {
            DicKeys.Add(inputKey, key);
        }
        else
        {
            DicKeys[inputKey] = key;
        }

        SaveKeys();
    }

    bool KeyIsChanged(InputKeyType inputKey)
    {
        if (Input.anyKey)
        {
            foreach (KeyCode k in PossibleKeys)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    continue;
                }

                if (Input.GetKeyDown(k))
                {
                    SetCustomKey(inputKey, k);
                    return true;
                }
            }
        }

        return false;
    }
}