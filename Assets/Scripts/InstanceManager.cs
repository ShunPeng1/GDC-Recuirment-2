using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager <T>: MonoBehaviour where T : Object
{
    #region Instance

    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                _instance = new GameObject("Spawned WorldTasksManager", typeof(T)).GetComponent <T>();
            }

            return _instance;
        }
        set
        {
            _instance = value;    
        }
    }
    #endregion

}
