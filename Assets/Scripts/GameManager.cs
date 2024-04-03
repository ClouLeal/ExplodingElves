using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameUiController _gameUiController;

    [Header("Spawn Points")]
    [SerializeField] private List<SpawnPoint> _spawnPoints =  new List<SpawnPoint>();

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;


    private bool _gameIsRunning;
     
    void Awake()
    {
        _gameIsRunning = false;

        _gameUiController.SetUp(StartGame,EndGame);

         foreach(var _spawnPoint in _spawnPoints)
        {
            _spawnPoint.SetUp(_audioManager);
        }
    }

    
    private void StartGame()
    {
       if(!_gameIsRunning) 
       {
        foreach(var _spawnPoint in _spawnPoints)
        {
            _spawnPoint.StartGame();
        }
        _gameIsRunning = true;
       }
    }

    private void EndGame()
    {
        if(_gameIsRunning) 
        {
            foreach(var _spawnPoint in _spawnPoints)
            {
                _spawnPoint.StopGame();
            }

        _gameIsRunning = false;
        }
    }

}
