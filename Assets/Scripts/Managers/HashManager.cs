using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashManager : Singleton<HashManager>
{
    public Dictionary<TransitionParameter, int> dicMainParams = new Dictionary<TransitionParameter, int>();

    private void Awake()
    {
        // TO GET ALL THE TRANSITION PARAMTERS IN AN ARRAY
        TransitionParameter[] arr = System.Enum.GetValues(typeof(TransitionParameter)) as TransitionParameter[];

        // WE TURN THEM TO AN INTEGER AND ADD INTO A DICTIONARY
        foreach(TransitionParameter t in arr)
        {
            dicMainParams.Add(t, Animator.StringToHash(t.ToString()));
        }
    }
}
