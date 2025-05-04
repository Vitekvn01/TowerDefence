using UnityEngine;

public class AttackState : IState
{
    private readonly EnemyStateMachine _fsm;
    private readonly AIEnemy _aiEnemy;
    
    private float _attackCooldownTimer;
    
    public AttackState(EnemyStateMachine fsm)
    {
        _fsm = fsm;
        _aiEnemy = fsm.AiEnemy;
    }

    public void Enter()
    {
        Debug.Log("Состояние атаки");
        _aiEnemy.Stop();
    }

    public void Update()
    {
        _attackCooldownTimer += Time.deltaTime;

        if (_attackCooldownTimer >= _aiEnemy.Enemy.TimeToAttack)
        {
            _attackCooldownTimer = 0f;
            _aiEnemy.Enemy.Attack();
        }
        
        if (_aiEnemy.Enemy.Target != null && !_aiEnemy.TryCheckDistanceToAttack())
        {
            _fsm.ChangeState(new MoveToTargetState(_fsm));
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из атаки");
    }
}
