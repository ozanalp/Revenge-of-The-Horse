using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Catch")]
public class Catch : StateData
{
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
                animator.SetBool(TransitionParameter.Grab.ToString(), true);
            }
        }
        else
        {
            animator.SetBool(TransitionParameter.Grab.ToString(), false);
        }

        if (control.hump)
        {
            animator.SetBool(TransitionParameter.Hump.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public bool IsGrabbable(CharacterControl control)
    {
        foreach (GameObject o in control.collisionSpheres.frontSpheres)
        {
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(Vector3.right), out hit, distance))
            {                
                return true;
            }
        }
        return false;
    }
}