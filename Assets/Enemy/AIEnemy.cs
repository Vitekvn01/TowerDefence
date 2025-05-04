using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
public class AIEnemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Enemy Enemy { get; private set; }
    
    public EnemyStateMachine FSM { get; private set; }

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();

        FSM = new EnemyStateMachine(this);
    }
    
    private void OnEnable()
    {
        TryInitializeAI();
    }
    
    private void Start()
    {
        TryInitializeAI();
    }

    private void Update()
    {
        FSM.Update();
    }

    public void MoveToTarget()
    {
        _agent.SetDestination(Enemy.Target.position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }
    
    public bool TryCheckDistanceToAttack()
    {
        bool isAttackRadius;
        
        if (Enemy.Target != null)
        {
            if (Vector3.Distance(transform.position, Enemy.Target.position) < Enemy.RadiusAttack)
            {
                isAttackRadius = true;
            }
            else
            {
                isAttackRadius = false;
            }
        }
        else
        {
            isAttackRadius = false;
        }

        return isAttackRadius;
    }
    
    private void TryInitializeAI()
    {
        if (Enemy.Target != null)
        {
            SetTargetMove(Enemy.Target);
            SetSpeedMove(Enemy.Speed);
            
            FSM.ChangeState(new MoveToTargetState(FSM));
        }
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
