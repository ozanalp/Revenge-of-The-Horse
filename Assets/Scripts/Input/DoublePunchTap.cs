using System.Collections;
using UnityEngine;

public class DoublePunchTap : Singleton<DoublePunchTap>
{
    private float firstClickTime, timeBetweenClicks;
    private bool coroutineAllowed = true;
    private int clickCounter;
    public bool doubleTap;
    [SerializeField] private CharacterControl control; //DEBUG PURPOSE
    private Animator animator;

    private void Start()
    {
        firstClickTime = 0f;
        timeBetweenClicks = 0.3f;
        clickCounter = 0;
        coroutineAllowed = true;
        control = GameObject.Find("Player").GetComponent<CharacterControl>();
        animator = control.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            clickCounter += 1;
        }
        if (clickCounter == 1 && coroutineAllowed)
        {
            firstClickTime = Time.time;

            StartCoroutine(DoubleTapDetection(animator));
        }
    }

    public IEnumerator DoubleTapDetection(Animator animator)
    {
        coroutineAllowed = false;
        while (Time.time < firstClickTime + timeBetweenClicks)
        {
            if (clickCounter >= 2)
            {
                doubleTap = true;
                animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.H_Punch], true);
                animator.SetBool(HashManager.Instance.dicMainParams[TransitionParameter.L_Punch], false);
                Debug.Log("Double Punch");
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clickCounter = 0;
        firstClickTime = 0f;
        doubleTap = false;
        coroutineAllowed = true;
    }
}