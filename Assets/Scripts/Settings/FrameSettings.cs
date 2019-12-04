using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "AbilityData/FrameSettings")]
public class FrameSettings : ScriptableObject
{
    [Range(0.01f,1f)]public float timeScale;
    public int targetFPS = 200;
}
