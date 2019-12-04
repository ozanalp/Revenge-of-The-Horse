using System.Collections;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public PoolObjectType poolObjectType;
    public float scheduledOffTime;
    private Coroutine offRoutine;

    private void OnEnable()
    {
        if (offRoutine != null)
        {
            StopCoroutine(offRoutine);
        }

        if (scheduledOffTime > 0)
        {
            offRoutine = StartCoroutine(_ScheduledOff());
        }
    }

    public void TurnOff()
    {
        PoolManager.Instance.AddObject(this);
    }

    private IEnumerator _ScheduledOff()
    {
        yield return new WaitForSeconds(scheduledOffTime);

        if (!PoolManager.Instance.poolDictionary[poolObjectType].Contains(gameObject))
        {
            TurnOff();
        }
    }
}
