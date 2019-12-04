using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public FrameSettings frameSettings;

    private void Awake()
    {
        Time.timeScale = frameSettings.timeScale;
        Application.targetFrameRate = frameSettings.targetFPS;
    }
}
