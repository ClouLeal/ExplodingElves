using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "ScriptableObjects/SpawnPointData", order = 1)]
public class SpawnPointData : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private string _tag;
    [SerializeField] private float _spawnRate = 1f; 
    [SerializeField] private int _initAmountInPool = 5;
    [SerializeField] private int _maxSpawnPool = 100;

    public Color Color => _color;
    public string Tag=>_tag;

    public  float SpawnRate =>_spawnRate;
    public  int InitAmountInPool =>_initAmountInPool;
    public int MaxSpawnPool => _maxSpawnPool;

    public void SetSpawnRate(float spawnRate){
        _spawnRate = spawnRate;
    }

}