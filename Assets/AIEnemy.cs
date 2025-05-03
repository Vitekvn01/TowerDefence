using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
public class AIEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        SetTargetMove(_enemy.Target);
        SetSpeedMove(_enemy.Speed);
    }

    private void SetTargetMove(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    private void SetSpeedMove(float speed)
    {
        _agent.speed = speed;
    }
}
