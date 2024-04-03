using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovimentController : MonoBehaviour
{
    [SerializeField] private float _movimentRange;
    private NavMeshAgent _playerAgent;

    private bool _isMoving;

    void Awake()
    {
        _playerAgent = GetComponent<NavMeshAgent>();
    }

    void OnEnable(){
        _playerAgent.isStopped = false;
       SetAgentDestination();
    }

    void OnDisable() => _playerAgent.isStopped = true;

    void Update()
    {
        if(_playerAgent.remainingDistance <= _playerAgent.stoppingDistance)
        {
            SetAgentDestination();
        }
    }

    void SetAgentDestination(){

         if(RandomPoint(transform.position, out var destinationPoint))
        {
            _playerAgent.SetDestination(destinationPoint);
        }
    }

    bool RandomPoint(Vector3 center, out Vector3 destinationPoint)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * _movimentRange;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            destinationPoint = hit.position;
            return true;
        }

        destinationPoint = center;
        return false;
    }
}
