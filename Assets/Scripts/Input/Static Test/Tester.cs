using InputExtender;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Update()
    {
        if (KeyExtender.isDoubleTap(KeyCode.L))
        {
            Debug.Log("keypress triggered");
        }
    }
}
