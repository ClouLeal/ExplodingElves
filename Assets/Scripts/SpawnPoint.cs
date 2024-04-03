using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ElfController _elfController;
    [SerializeField] private SpawnRateUI _spawnRateUI;
    [SerializeField] private SpawnPointData _data;


    private ObjectPool _objectPool;
    private bool _isSpawnning;

    List<string> _collisionObjectID = new List<string>();
    AudioManager _audioManager;
   
    public void UpdateSpawnRate (float newRate)
    {
        _data.SetSpawnRate(newRate);
    }

    void Awake()
    {
        _spawnRateUI.SetUp(_data.SpawnRate, gameObject.tag);
        _spawnRateUI.UpdateSpawnRate += UpdateSpawnRate;

        _objectPool = gameObject.AddComponent<ObjectPool> ();
        _objectPool.SetObjectPool(_elfController,_data.InitAmountInPool, _data.MaxSpawnPool, transform);

        var material = GetComponentInChildren<MeshRenderer>().material;
        material.color = _data.Color;
    }

    float _nextTimeToSpawn = 0;

    public void Update()
    {
        if(!_isSpawnning) return;
        
        if(Time.time > _nextTimeToSpawn){
            SpawNewElf(transform.position);
        }
    }

    public void StartGame ()
    {
        _isSpawnning = true;
        SpawNewElf(transform.position);
    }

    public void StopGame()
    {
        // StopSpawnning();
         _isSpawnning = false;

        var activeElves = GetComponentsInChildren<ElfController>(true);
        foreach(var activeElf in activeElves){
            activeElf.DisableElf(true);
            OnObjectDesable(activeElf);
        }
    }

    public void SetUp(AudioManager audioManager){
        _audioManager = audioManager;
    }


    private void GenerateElfByCollidion(Vector3 initialPosition,string originalObjId, string collisionObjId)
    {
        if(_collisionObjectID.Contains(collisionObjId))
        {
            _collisionObjectID.Remove(collisionObjId);
        }
        else
        {
            _collisionObjectID.Add(collisionObjId);
            SpawNewElf(initialPosition);
        }
    }

    private void SpawNewElf(Vector3 initialPosition)
    {
        if(_objectPool.GetPooledObject(out var playerController))
        {
            playerController.SetUp(initialPosition, _audioManager, _data.Color, _data.Tag);

            playerController.OnDeath += OnObjectDesable;
            playerController.SpawnElf += GenerateElfByCollidion;

            _nextTimeToSpawn = Time.time + 1/_data.SpawnRate;
        }
    }

    private void OnObjectDesable(ElfController playerController)
    {
        playerController.OnDeath -= OnObjectDesable;
        playerController.SpawnElf -= GenerateElfByCollidion;

        _objectPool.ReturntPooledObject(playerController);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _spawnRateUI.OpenPanel();
    }
}
