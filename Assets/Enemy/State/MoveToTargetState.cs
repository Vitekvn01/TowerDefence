
using UnityEngine;

public class MoveToTargetState : IState
{
    private readonly EnemyStateMachine _fsm;
    private readonly AIEnemy _aiEnemy;

    public MoveToTargetState(EnemyStateMachine fsm)
    {
        _fsm = fsm;
        _aiEnemy = fsm.AiEnemy;
    }

    public void Enter()
    {
        Debug.Log("Вход состояния движения");
        _aiEnemy.MoveToTarget();
    }

    public void Update()
    {
        if (_aiEnemy.TryCheckDistanceToAttack())
        {
            _fsm.ChangeState(new AttackState(_fsm));
        }
    }

    public void Exit()
    {
        Debug.Log("Выход состояния движения");
        _aiEnemy.Stop();
    }
}
