using System;

public class Score
{
    public int Points { get; private set; }

    public event Action<int> OnScoreChanged;

    public void AddPoints(int points)
    {
        Points += points;
        OnScoreChanged?.Invoke(Points);
    }
}
