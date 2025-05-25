using IInterfaces;

public class GameResultHandler
{
    private readonly WinPanel _winPanel;
    private readonly LosePanel _losePanel;

    private ILevelManager _levelManager;
    
    public GameResultHandler(WinPanel winPanel, LosePanel losePanel, ILevelManager levelManager)
    {
        _winPanel = winPanel;
        _losePanel = losePanel;
        _levelManager = levelManager;

        _winPanel.OnNextLevelClickEvent += _levelManager.LoadNextLevel;
        _winPanel.OnExitClickEvent += _levelManager.LoadFirstScene;

        _losePanel.OnRestartClickEvent += _levelManager.RestartLevel;
        _losePanel.OnExitClickEvent += _levelManager.LoadFirstScene;
    }
    public void Win()
    {
        _winPanel.Show();
    }

    public void Lose()
    {
        _losePanel.Show();
    }

    public void Dispose()
    {
        _winPanel.OnNextLevelClickEvent -= _levelManager.LoadNextLevel;
        _winPanel.OnExitClickEvent -= _levelManager.LoadFirstScene;

        _losePanel.OnRestartClickEvent -= _levelManager.RestartLevel;
        _losePanel.OnExitClickEvent -= _levelManager.LoadFirstScene;
    }
}
