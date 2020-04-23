using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Catch")]
public class Catch : StateData
{
    RaycastHit hit;
    public float distance;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (IsGrabbable(control))
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], true);
            }
        }
        else
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Grab], false);
        }

        if (control.hump)
        {
            animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.Hump], true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);
        
        if (control.grab) control.DeactiveCaller(hit);
    }

    private bool IsGrabbable(CharacterControl control)
    {
        foreach (GameObject o in control.collisionSpheres.frontSpheres)
        {
            if (Physics.Raycast(o.transform.position, o.transform.right, out RaycastHit hit, distance))
            {
                this.hit = hit;
                control.grab = true;
                return true;
            }
        }

        control.grab = false;
        return false;
    }
}