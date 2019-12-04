using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _intance;

    public static T Instance
    {
        get
        {
            //_intance = (T)FindObjectOfType(typeof(T));
            if (_intance == null)
            {
                GameObject obj = new GameObject();
                _intance = obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
            }
            return _intance;
        }
    }
}
