using UnityEngine;


public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            return _instance;
        }
        
    }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            print(typeof(T).ToString());
            _instance = this as T;
        }
    }
    
    
}


