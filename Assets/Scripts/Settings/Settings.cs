using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public FrameSettings frameSettings;
    public PhysicsSettings physicsSettings;

    private float pastTimeScale = 0f;

    private void Awake()
    {
        //Frames
        Debug.Log("targetFrameRate: " + frameSettings.targetFPS);
        Application.targetFrameRate = frameSettings.targetFPS;

        //Physics
        Debug.Log("Default Solver Velocity Iterations: " + physicsSettings.DefaultSolverVelocityIterations);
        Physics.defaultSolverVelocityIterations = physicsSettings.DefaultSolverVelocityIterations;

        //Default Keys
        Debug.Log("loading key bindings");
        VirtualInputManager.Instance.LoadKeys();
        //VirtualInputManager.Instance.SetDefaultKeys();
    }

    private void LateUpdate()
    {
        if (pastTimeScale != frameSettings.timeScale)
        {
            pastTimeScale = frameSettings.timeScale;
            Time.timeScale = frameSettings.timeScale;
        }
    }
}
