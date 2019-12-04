using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/KickBoxToggle")]
public class KickBoxToggle : StateData
{
    //[Range(.01f, 1f)] public float startTiming;
    //[Range(.01f, 1f)] public float endTiming;
    public float startTiming;
    public float endTiming;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        SetBoxActive(characterState, animator, stateInfo);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        SetBoxInactive(characterState, animator, stateInfo);
    }

    private void SetBoxActive(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        while (startTiming <= stateInfo.normalizedTime && endTiming > stateInfo.normalizedTime)
        {
            for (float t = 0; t <= startTiming; t += Time.deltaTime)
            {
                control.l_kickBox.SetActive(true);
            }
            break;
        }
    }

    private void SetBoxInactive(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (stateInfo.normalizedTime >= endTiming)
        {
            control.l_kickBox.SetActive(false);
        }
    }
}