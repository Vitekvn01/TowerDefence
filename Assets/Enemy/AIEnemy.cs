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
        _agent = GetComponent<NavMeshAgent>();
        Enemy = GetComponent<Enemy>();
        
        FSM = new EnemyStateMachine(this);
    }
    
    private void OnEnable()
    {
        _agent.Resume();
        
        Debug.Log($"[{name}] OnEnable start");
    
        // 1. Перенести агента внутрь NavMesh
        if (NavMesh.SamplePosition(transform.position, out var hit, 2f, NavMesh.AllAreas))
        {
            _agent.enabled = false;
            _agent.Warp(hit.position);
            _agent.enabled = true;
        }
        else Debug.LogWarning($"[{name}] Spawn off NavMesh!");
    
        // 2. Сброс остановки и пути
        _agent.isStopped = false;
        _agent.updatePosition = true;
        _agent.updateRotation = true;
        
    
        // 4. Лог финального состояния
        Debug.Log($"[{name}] OnEnable end — isOnNavMesh={_agent.isOnNavMesh}, dest={_agent.destination}");
        
        Vector3 pos = transform.position;
        bool onMesh = NavMesh.SamplePosition(pos, out var hit2, 2f, NavMesh.AllAreas);
        Debug.DrawRay(pos, Vector3.up * 2f, onMesh ? Color.green : Color.red, 5f); 
        Debug.Log($"SamplePosition returned {onMesh}, hit.pos={hit2.position}");  // :contentReference[oaicite:0]{index=0}
        
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
        _agent.enabled = false;
        _agent.enabled = true;
            
        _agent.isStopped = false;
        
        if (Enemy.Target != null)
        {
            SetTargetMove(Enemy.Target);
            SetSpeedMove(Enemy.Speed);
            
            FSM.ChangeState(new MoveToTargetState(FSM));
        }
        else
        {
            Debug.Log("Enemy.Target == null");
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
