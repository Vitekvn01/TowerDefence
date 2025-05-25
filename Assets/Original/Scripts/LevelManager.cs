using System;
using System.Collections.Generic;
using IInterfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : ILevelManager
{
   private const string UnlockCountKey = "UnlockCount";

   private const int StartUnlockCount = 1;
   
   private readonly List<Level> _levels;

   private SceneData _firstScene;
      
   private Level _currentLevel;

   private int _unlockCount;

   public LevelManager(List<Level> levels, SceneData firstScene)
   {
      _levels = levels;
      _firstScene = firstScene;
      
      for (int i = 0; i < levels.Count; i++)
      {
         if (i + 1 < levels.Count)
         {
            levels[i].SetNextLevel(levels[i + 1]);
         }
         else
         {
            levels[i].SetNextLevel(levels[0]);
         }
      }
      
      _unlockCount = LoadUnlockLevel();
      
      Debug.Log("UnlockLevels:" + _unlockCount);
      
      for (int i = 0; i < _unlockCount; i++)
      {
         _levels[i].UnlockLevel();
      }
   }
   
   public void UnlockNextLevel()
   {
      
      if (_currentLevel != null)
      {
         if (_currentLevel.NextLevel.IsLock == true)
         {
            _currentLevel.NextLevel.UnlockLevel();
            _unlockCount++;
            SaveUnlockLevel();
            Debug.Log("UnlockNextLevel");
         }
      }
   }
   
   public void LoadNextLevel()
   {
      if (_currentLevel.NextLevel != null)
      {
         LoadLevel(_currentLevel.NextLevel);
         _currentLevel = _currentLevel.NextLevel;
      }
      else
      {
         LoadFirstScene();
      }
   }

   public void LoadFirstScene()
   {
      SceneManager.LoadScene(_firstScene.Name);
   }
   
   public void LoadLevel(Level level)
   {
      SceneManager.LoadScene(level.SceneData.Name);
      _currentLevel = level;
   }

   public void RestartLevel()
   {
      SceneManager.LoadScene(_currentLevel.SceneData.Name);
   }
   
   public IReadOnlyList<Level> GetLevels()
   {
      return _levels;
   }
   private int LoadUnlockLevel()
   {
      return PlayerPrefs.GetInt(UnlockCountKey, StartUnlockCount);
   }

   private void SaveUnlockLevel()
   {
      PlayerPrefs.SetInt(UnlockCountKey, _unlockCount);
   }

}

[Serializable]
public class Level
{
   public SceneData SceneData;

   public Level NextLevel {  get; private set; }

   public bool IsLock { get; private set; } = true;

   public void SetNextLevel(Level level)
   {
      NextLevel = level;
   }
   
   public void UnlockLevel()
   {
      IsLock = false;
   }
}

