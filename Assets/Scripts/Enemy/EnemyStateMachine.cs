public class EnemyStateMachine : StateMachine
{
    private readonly AIEnemy _aiEnemy;

    public EnemyStateMachine(AIEnemy aiEnemy)
    {
        _aiEnemy = aiEnemy;
    }

    public AIEnemy AiEnemy => _aiEnemy;
}
