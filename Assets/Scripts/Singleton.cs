using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T _instace; 

    public static T Instance
    {
        get 
        {
            if(_instace == null)
            {
                _instace = FindObjectOfType<T>();
                if(_instace == null){
                    GameObject gameObject = new GameObject("Controller");
                    _instace  = gameObject.AddComponent<T>();
                }
            }
            return _instace;
        }
    }


    void Awake()
    {
        if(_instace == null)
        {
            _instace = this as T;
        }
        else
        {
            if(_instace != this)
            {
                Debug.Log("Singleton Destroy");
                Destroy(gameObject);
            }
        }
    }
}
