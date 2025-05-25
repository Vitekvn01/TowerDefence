using System.Collections.Generic;

namespace IInterfaces
{
    public interface ILevelManager
    {
        public IReadOnlyList<Level> GetLevels();
        public void LoadFirstScene();
        public void UnlockNextLevel();
        public void LoadNextLevel();

        public void LoadLevel(Level level);

        public void RestartLevel();
    }
}