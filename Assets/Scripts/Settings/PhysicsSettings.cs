using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/PhysicsSettings")]
public class PhysicsSettings : ScriptableObject
{
    public int DefaultSolverVelocityIterations;
}
