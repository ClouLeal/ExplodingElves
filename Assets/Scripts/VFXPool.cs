using System.Collections.Generic;
using UnityEngine;


public class VFXPool : Singleton<VFXPool> {

    private Queue<ParticleSystem> _explosionParticlesPool = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> _spawnParticlesPool = new Queue<ParticleSystem>();

    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private ParticleSystem _spawnParticles;
    [SerializeField] private int _initAmountInPool;
     [SerializeField]private int _maxNumber = 20;

    private Transform _parentObject;

   
    private int _explosiontPoolCount =0;
    private int _spawnPoolCount =0;

    void Start()
    {
        _parentObject = transform;

        ParticleSystem tmp;
        for(int i = 0; i < _initAmountInPool; i++)
        {
            tmp = Factory(EffectType.Explosion);
            _explosionParticlesPool.Enqueue(tmp);
        }

        for(int i = 0; i < _initAmountInPool; i++)
        {
            tmp = Factory(EffectType.Spawn);
            _spawnParticlesPool.Enqueue(tmp);
        }
    }

     ParticleSystem Factory(EffectType type )
    {
        var paticle = type == EffectType.Explosion? _explosionParticles : _spawnParticles;

        var tmp = Instantiate(paticle,_parentObject);

        if(type == EffectType.Explosion) _explosiontPoolCount ++;
        else _spawnPoolCount++;

        return tmp;
    }

    public bool GetPooledObject(EffectType type , out ParticleSystem playerController )
    {
        playerController = null;

        var particlepool = type == EffectType.Explosion ? _explosionParticlesPool : _spawnParticlesPool;

        if(particlepool.Count > 0)
        {
            playerController = particlepool.Dequeue();
            return true;
        }

        var poolCount = type == EffectType.Explosion? _explosiontPoolCount : _spawnPoolCount;
        if(poolCount >= _maxNumber) 
        {   
            return false;
        }
    
        playerController = Factory(type);
        return  true;
    }


    public void ReturntPooledObject(EffectType type,ParticleSystem pooledObject)
    {
        pooledObject.transform.SetParent(_parentObject);

        if(type == EffectType.Explosion)
        {
             _explosionParticlesPool.Enqueue(pooledObject);
             return;
        }
        if(type == EffectType.Spawn)
        {
            _spawnParticlesPool.Enqueue(pooledObject);
        }
       
    }
}