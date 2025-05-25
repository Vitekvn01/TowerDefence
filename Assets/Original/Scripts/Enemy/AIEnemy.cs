using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
public class AIEnemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    private Vector3 _targetPos;
    public Enemy Enemy { get; private set; }
    public EnemyStateMachine FSM { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = true;
        _agent.updateRotation = true;
        if (TryGetComponent<Rigidbody>(out var rb))
            rb.isKinematic = true;

        Enemy = GetComponent<Enemy>();
        FSM = new EnemyStateMachine(this);
    }

    private void ReInitializeNavMeshAgent()
    {
        _agent.enabled = false;
        _agent.Warp(transform.position);
        _agent.enabled = true;
        _agent.ResetPath();
        _agent.isStopped = false;
        _agent.updatePosition = true;
        _agent.updateRotation = true;
    }

    private void Update()
    {
        FSM.Update();
    }

    public void MoveToTarget()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_targetPos);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public bool TryCheckDistanceToAttack()
    {
        return Vector3.Distance(transform.position, Enemy.Target.position) <= Enemy.RadiusAttack;
    }

    public void Initialize(Transform target)
    {
        ReInitializeNavMeshAgent();
        
        _targetPos = target.position;
        _agent.isStopped = false;
        FSM.ChangeState(new MoveToTargetState(FSM));
    }
}
