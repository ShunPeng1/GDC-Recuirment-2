using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager <T>: MonoBehaviour
{
    #region Instance

    private static InstanceManager<T> _instance;

    public static InstanceManager<T> Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType< InstanceManager<T>>();
            if (_instance == null)
            {
                _instance = new GameObject("Spawned WorldTasksManager", typeof(InstanceManager<T>)).GetComponent <InstanceManager<T>>();
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
