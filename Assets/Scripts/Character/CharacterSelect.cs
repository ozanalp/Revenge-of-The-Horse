using UnityEngine;

public enum PlayableCharacterType
{
    NONE, HORSE, BULL, TIGER, KONG,
}

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/CharacterSelect")]
public class CharacterSelect : ScriptableObject
{
    public PlayableCharacterType selectedCharacterType;
}
