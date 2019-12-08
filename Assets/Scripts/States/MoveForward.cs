using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/MoveForward")]
public class MoveForward : StateData
{
    public AnimationCurve speedGraph;
    public float speed;
    public float blockDistance;
    public bool constant;
    public bool lockDirection;
    public bool allowEarlyTurn;

    [Header("Momentum")]
    public float startingMomentum;
    public float maxMomentum;
    public bool useMomentum;
    public bool clearMomentumOnExit;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.characterControl.animationProgress.latestMoveForward = this;

        CharacterControl control = characterState.GetCharacterControl(animator);

        if (allowEarlyTurn)
        {
            if (control.moveRight)
            {
                control.FaceForward(true);
            }
            if (control.moveLeft)
            {
                control.FaceForward(false);
            }
        }

        //control.animationProgress.momentum = 0f;

        if (startingMomentum > 0.001f)
        {
            if (control.IsFacingForward())
            {
                control.animationProgress.momentum = startingMomentum;
            }
            else
            {
                control.animationProgress.momentum = -startingMomentum;
            }
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (characterState.characterControl.animationProgress.latestMoveForward != this)
        {
            return;
        }

        if (!control.moveRight && !control.moveLeft && !control.moveUp && !control.moveDown)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }

        if (control.moveRight && control.moveLeft || control.moveUp && control.moveDown)
        {
            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }

        if (useMomentum)
        {
            UpdateMomentum(control, stateInfo);
        }
        else
        {
            if (constant)
            {
                ConstantMove(control, animator, stateInfo);
            }
            else
            {
                ControlledMove(control, animator, stateInfo);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (clearMomentumOnExit)
        {
            control.animationProgress.momentum = 0f;
        }
    }

    private void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
    {
        if (!control.animationProgress.RightSideIsBlocked())
        {
            if (control.moveRight)
            {
                control.animationProgress.momentum += speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }
        }

        if (!control.animationProgress.LeftSideIsBlocked())
        {
            if (control.moveLeft)
            {
                control.animationProgress.momentum += -speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }
        }

        if (!control.animationProgress.UpSideIsBlocked())
        {
            if (control.moveUp)
            {
                control.animationProgress.momentum += speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }
        }

        if (!control.animationProgress.DownSideIsBlocked())
        {
            if (control.moveDown)
            {
                control.animationProgress.momentum += -speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }
        }

        if (Mathf.Abs(control.animationProgress.momentum) >= maxMomentum)
        {
            if (control.animationProgress.momentum > 0f)
            {
                control.animationProgress.momentum = maxMomentum;
            }
            else if (control.animationProgress.momentum < 0f)
            {
                control.animationProgress.momentum = -maxMomentum;
            }
        }

        if (control.animationProgress.momentum > 0)
        {
            control.FaceForward(true);
        }
        else if (control.animationProgress.momentum < 0)
        {
            control.FaceForward(false);
        }

        if (!IsBlocked(control, speed, stateInfo))
        {
            control.MoveForward(speed, Mathf.Abs(control.animationProgress.momentum));
        }
    }

    private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (!IsBlocked(control, speed, stateInfo))
        {
            control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
        }
    }

    private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
    {
        CollisionSpheres collisionSpheres = control.collisionSpheres;

        if (control.moveRight)
        {
            if (!IsBlocked(control, speed, stateInfo))
            {
                control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
            else
            {
                foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                {
                    foreach (GameObject o in collisionSpheres.rightSpheres)
                    {
                        if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(Vector3.right), out RaycastHit hit, blockDistance))
                        {
                            if (control.isEnemy)
                            {
                                float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                if (currentDistance < 2.0f)
                                {
                                    while (IsBlocked(control, speed, stateInfo))
                                    {
                                        Vector3 dist = control.transform.position - hit.transform.position;
                                        control.transform.position += Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.forward;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (control.moveLeft)
        {
            if (!IsBlocked(control, speed, stateInfo))
            {
                control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
            else
            {
                foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                {
                    foreach (GameObject o in collisionSpheres.leftSpheres)
                    {
                        if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(-Vector3.right), out RaycastHit hit, blockDistance))
                        {
                            if (control.isEnemy)
                            {
                                float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                if (currentDistance < 2.0f)
                                {
                                    while (IsBlocked(control, speed, stateInfo))
                                    {
                                        Vector3 dist = control.transform.position - hit.transform.position;
                                        control.transform.position -= Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.forward;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (control.moveUp)
        {
            if (!IsBlocked(control, speed, stateInfo))
            {
                if (control.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    control.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else if (control.transform.rotation == Quaternion.Euler(0, 180, 0))
                {
                    control.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                }
            }
            else
            {
                if (control.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                    {
                        foreach (GameObject o in collisionSpheres.frontSpheres)
                        {
                            if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(Vector3.forward), out RaycastHit hit, blockDistance))
                            {
                                if (control.isEnemy)
                                {
                                    float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                    if (currentDistance < 2f)
                                    {
                                        while (IsBlocked(control, speed, stateInfo))
                                        {
                                            Vector3 dist = control.transform.position - hit.transform.position;
                                            control.transform.position += Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.right;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (control.transform.rotation == Quaternion.Euler(0, 180, 0))
                {
                    foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                    {
                        foreach (GameObject o in collisionSpheres.backSpheres)
                        {
                            if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(Vector3.forward), out RaycastHit hit, blockDistance))
                            {
                                if (control.isEnemy)
                                {
                                    float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                    if (currentDistance < 2f)
                                    {
                                        while (IsBlocked(control, speed, stateInfo))
                                        {
                                            Vector3 dist = control.transform.position - hit.transform.position;
                                            control.transform.position -= Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.right;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (control.moveDown)
        {
            if (!IsBlocked(control, speed, stateInfo))
            {
                if (control.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    control.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                }
                else if (control.transform.rotation == Quaternion.Euler(0, 180, 0))
                {
                    control.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
            else
            {
                if (control.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                    {
                        foreach (GameObject o in collisionSpheres.backSpheres)
                        {
                            if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(-Vector3.forward), out RaycastHit hit, blockDistance))
                            {
                                if (control.isEnemy)
                                {
                                    float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                    if (currentDistance < 2f)
                                    {
                                        while (IsBlocked(control, speed, stateInfo))
                                        {
                                            Vector3 dist = control.transform.position - hit.transform.position;
                                            control.transform.position += Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.right;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (control.transform.rotation == Quaternion.Euler(0, 180, 0))
                {
                    foreach (CharacterControl co in FindObjectOfType<CharacterManager>().characters)
                    {
                        foreach (GameObject o in collisionSpheres.frontSpheres)
                        {
                            if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(-Vector3.forward), out RaycastHit hit, blockDistance))
                            {
                                if (control.isEnemy)
                                {
                                    float currentDistance = Vector3.Distance(control.transform.position, hit.transform.position);

                                    if (currentDistance < 2f)
                                    {
                                        while (IsBlocked(control, speed, stateInfo))
                                        {
                                            Vector3 dist = control.transform.position - hit.transform.position;
                                            control.transform.position -= Vector3.SqrMagnitude(dist) * Time.deltaTime * Vector3.right;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        CheckTurn(control);
    }

    private bool IsBlocked(CharacterControl control, float speed, AnimatorStateInfo stateInfo)
    {
        if (control.animationProgress.blockingObjs.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckTurn(CharacterControl control)
    {
        if (!lockDirection)
        {
            if (control.moveRight)
            {
                control.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (control.moveLeft)
            {
                control.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

        }
    }

    #region Directional Raycast Check
    //public bool CheckFront(CollisionSpheres collisionSpheres, CharacterControl control)
    //{
    //    foreach (GameObject o in collisionSpheres.rightSpheres)
    //    {
    //        Debug.DrawRay(o.transform.position, o.transform.TransformDirection(Vector3.right) * 0.3f, Color.yellow);

    //        if (Physics.Raycast(o.transform.position, o.transform.TransformDirection(Vector3.right), out RaycastHit hit, blockDistance))
    //        {
    //            //Debug.Log(control.childs.Contains(hit.collider.transform));
    //            if (!collisionSpheres.childs.Contains(hit.collider.transform))
    //            {
    //                if (!IsPlayerAttackBox(hit.collider, control))
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}

    //public bool CheckUp(CollisionSpheres collisionSpheres)
    //{
    //    RaycastHit hit;

    //    foreach (GameObject o in collisionSpheres.frontSpheres)
    //    {
    //        if (o.transform.rotation == Quaternion.Euler(0, 0, 0))
    //        {
    //            Debug.DrawRay(o.transform.position, o.transform.forward * 0.3f, Color.yellow);
    //            if (Physics.Raycast(o.transform.position, o.transform.forward, out hit, blockDistance))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    foreach (GameObject o in collisionSpheres.backSpheres)
    //    {
    //        if (o.transform.rotation == Quaternion.Euler(0, 180, 0))
    //        {
    //            Debug.DrawRay(o.transform.position, -o.transform.forward * 0.3f, Color.yellow);
    //            if (Physics.Raycast(o.transform.position, -o.transform.forward, out hit, blockDistance))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    //public bool CheckDown(CollisionSpheres collisionSpheres)
    //{
    //    RaycastHit hit;

    //    foreach (GameObject o in collisionSpheres.backSpheres)
    //    {
    //        if (o.transform.rotation == Quaternion.Euler(0, 0, 0))
    //        {
    //            Debug.DrawRay(o.transform.position, -o.transform.forward * 0.3f, Color.yellow);
    //            if (Physics.Raycast(o.transform.position, -o.transform.forward, out hit, blockDistance))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    foreach (GameObject o in collisionSpheres.frontSpheres)
    //    {
    //        if (o.transform.rotation == Quaternion.Euler(0, 180, 0))
    //        {
    //            Debug.DrawRay(o.transform.position, o.transform.forward * 0.3f, Color.yellow);
    //            if (Physics.Raycast(o.transform.position, o.transform.forward, out hit, blockDistance))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
    #endregion
}