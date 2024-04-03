using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private Queue<ElfController> _objectsPool = new Queue<ElfController>();

    private ElfController _objectToPool;

    private Transform _parentObject;

    private int _idCount =0;

    private  int _maxNumber = 50;

    public void SetObjectPool(ElfController objectToPool,int amountToPool, int maxSpawnPool, Transform parent)
    {
        _objectToPool = objectToPool;
        _parentObject = parent;
        _maxNumber = maxSpawnPool;

        ElfController tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Factory();
            tmp.DisableElf(true);
            _objectsPool.Enqueue(tmp);
        }
    }

     ElfController Factory()
    {
        var tmp = Instantiate(_objectToPool,_parentObject);
        tmp.SetID(_idCount++);
        return tmp;
    }

    public bool GetPooledObject(out ElfController playerController )
    {
        playerController = null;

        if(_objectsPool.Count > 0)
        {
            playerController = _objectsPool.Dequeue();
            return true;
        }

        if(_idCount >= _maxNumber) return false;
    
        playerController = Factory();
        return  true;
    }


    public void ReturntPooledObject(ElfController pooledObject, float delay = 0f)
    {
        _objectsPool.Enqueue(pooledObject);
    }
}