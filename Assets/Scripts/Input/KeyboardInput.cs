//using UnityEngine;

//public class KeyboardInput : MonoBehaviour
//{
//    private void Update()
//    {
//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            VirtualInputManager.Instance.doubleSpeed = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.doubleSpeed = false;
//        }
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            VirtualInputManager.Instance.moveRight = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.moveRight = false;
//        }
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            VirtualInputManager.Instance.moveLeft = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.moveLeft = false;
//        }
//        if (Input.GetKey(KeyCode.UpArrow))
//        {
//            VirtualInputManager.Instance.moveUp = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.moveUp = false;
//        }
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            VirtualInputManager.Instance.moveDown = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.moveDown = false;
//        }
//        if (Input.GetKey(KeyCode.Tab))
//        {
//            VirtualInputManager.Instance.attempt = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.attempt = false;
//        }
//        if (Input.GetKey(KeyCode.Space))
//        {
//            VirtualInputManager.Instance.hump = true;
//        }
//        else
//        {
//            VirtualInputManager.Instance.hump = false;
//        }
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            VirtualInputManager.Instance.l_punch = true;

//            if (DoublePunchTap.Instance.doubleTap)
//            {
//                VirtualInputManager.Instance.h_punch = true;
//            }
//        }
//        else
//        {
//            VirtualInputManager.Instance.l_punch = false;
//            VirtualInputManager.Instance.h_punch = false;
//        }
//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            VirtualInputManager.Instance.l_kick = true;

//            if (DoubleKickTap.Instance.doubleTap)
//            {
//                VirtualInputManager.Instance.h_kick = true;
//            }
//        }
//        else
//        {
//            VirtualInputManager.Instance.l_kick = false;
//            VirtualInputManager.Instance.h_kick = false;
//        }
//    }
//}