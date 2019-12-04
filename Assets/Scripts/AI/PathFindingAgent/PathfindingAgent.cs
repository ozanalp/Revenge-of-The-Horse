using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingAgent : MonoBehaviour
{
    public GameObject target; //WHERE THE OBJECT GONNA GO
    public bool targetPlayableCharacter; //THE OBJECT GONNA FOLLOW WHATEVER THE PLAYABLE CHRACTER IS IN THE SCENE

    private NavMeshAgent navMesh;
    private Coroutine moveRoutine;


    public GameObject startSphere; // THE AGENT WILL HAVE THE INFO OF THE PLATFORM
    public GameObject endSphere; // AND THE NEXT PLATFORM
    public bool startWalk;

    public CharacterControl owner = null;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void GoToTarget()
    {
        navMesh.enabled = true;
        startSphere.transform.parent = null;
        endSphere.transform.parent = null;
        startWalk = false;

        navMesh.isStopped = false; // WHEN THE FUNCTION IS CALLED WE WANT THE AGENT TO BE MOVING

        if (targetPlayableCharacter)
        {
            target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
        }

        navMesh.SetDestination(target.transform.position);
        moveRoutine = StartCoroutine(_Move());

        //if (moveRoutines.Count != 0)
        //{
        //    if (moveRoutines[0] != null)
        //    {
        //        StopCoroutine(moveRoutines[0]);
        //    }
        //    moveRoutines.RemoveAt(0);
        //}

        //moveRoutines.Add(StartCoroutine(_Move()));
    }

    private void OnEnable()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
    }

    private IEnumerator _Move()
    {
        while (true)
        {
            if (navMesh.isOnOffMeshLink) // AGENT GOES FIRST TO THE OFFMESHLINK
            {
                startSphere.transform.position = navMesh.currentOffMeshLinkData.startPos; // FIRST WE GET THE START POSITION
                endSphere.transform.position = navMesh.currentOffMeshLinkData.endPos; // THEN WE GET THE END POSITION

                navMesh.CompleteOffMeshLink(); // AGENT MOVES TO THE NEXT LINK

                navMesh.isStopped = true; // WHEN THE AGENT FINISHES GOING THROUGH THE LINK WE STOP IT
                startWalk = true; // WHEN THE AGENT FINDS ITS DESTINATION
                break;
            }

            // DEFINING WHAT HAPPENS WHEN THE AGENT REACHES ITS DESTINATION
            Vector3 dist = transform.position - navMesh.destination;
            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                startSphere.transform.position = navMesh.destination;
                endSphere.transform.position = navMesh.destination;

                navMesh.isStopped = true;
                startWalk = true;
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        owner.obstacle.carving = true;
    }
}