using System;
using IInterfaces;

public class Score : IScore
{
    public int Points { get; private set; }

    public event Action<int> OnScoreChanged;

    public void AddPoints(int points)
    {
        Points += points;
        OnScoreChanged?.Invoke(Points);
    }
}
