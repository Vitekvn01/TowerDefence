using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
public class AIEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private NavMeshAgent _agent;

    private Transform _target;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        if (_enemy.Target != null)
        {
            SetTargetMove(_enemy.Target);
            SetSpeedMove(_enemy.Speed);
        }
    }

    private void Update()
    {
        CheckDistanceToTarget();
    }

    private void OnEnable()
    {
        if (_enemy.Target != null)
        {
            SetTargetMove(_enemy.Target);
            SetSpeedMove(_enemy.Speed);
        }
    }


    private void SetTargetMove(Transform target)
    {
        _target = target;
        _agent.SetDestination(target.position);
    }

    private void SetSpeedMove(float speed)
    {
        _agent.speed = speed;
    }
    

    private void CheckDistanceToTarget()
    {
        if (_target != null)
        {
            if (Vector3.Distance(gameObject.transform.position, _target.position) < 2f)
            {
                Debug.Log("Атакую!");
                Attack();
            }
        }

    }

    private void Attack()
    {
        _agent.isStopped = true;
        _enemy.Attack();
    }
}
