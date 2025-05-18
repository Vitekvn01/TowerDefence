using System;

namespace IInterfaces
{
    public interface IScore
    {
        public event Action<int> OnScoreChanged;

        public void AddPoints(int count);
    }
}