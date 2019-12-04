using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public List<CharacterControl> characters = new List<CharacterControl>();

    public CharacterControl GetCharacter(PlayableCharacterType playableCharacterType)
    {
        foreach (CharacterControl control in characters)
        {
            if (control.playableCharacterType == playableCharacterType)
            {
                return control;
            }
        }

        return null;
    }

    public CharacterControl GetCharacter(Animator animator)
    {
        foreach (CharacterControl control in characters)
        {
            if (control.animator == animator)
            {
                return control;
            }
        }

        return null;
    }

    public CharacterControl GetPlayableCharacter()
    {   
        //WE CHECK THROUGH ALL THE CHARACTERS IN THE MANAGER
        foreach (CharacterControl control in characters)
        {
            // IF THE CHARACTER HAS MANUAL INPUT THEN IT'S CONTROLLED BY A HUMAN
            ManualInput manualInput = control.GetComponent<ManualInput>();

            if (manualInput != null)
            {
                if (manualInput.enabled)
                {
                    return control;
                }
            }
        }

        return null;
    }

    public CharacterControl GetCharacter(GameObject obj)
    {
        foreach (CharacterControl control in characters)
        {
            if (control.gameObject == obj)
            {
                return control;
            }
        }

        return null;
    }
}