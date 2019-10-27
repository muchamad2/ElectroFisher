﻿
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _shutdown = false;
    private static object _lock = new object();
    private static T _instance;

    public static T Instance{
        get{
            /* if(_shutdown){
                return null;
            } */
            lock(_lock){
                if(_instance == null){
                    _instance = (T)FindObjectOfType(typeof(T));
                    if(_instance == null){
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
    }
    private void OnApplicationQuit()
    {
        _shutdown = true;
    }
    private void OnDestroy()
    {
        _shutdown = true;
    }
}
