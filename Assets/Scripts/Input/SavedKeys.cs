using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/SavedKeys")]
public class SavedKeys : ScriptableObject
{
    public List<KeyCode> KeyCodesList = new List<KeyCode>();
}
