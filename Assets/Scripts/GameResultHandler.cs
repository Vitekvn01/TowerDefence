public class GameResultHandler
{
    private readonly UIElement _winPanel;
    private readonly UIElement _losePanel;
    
    public GameResultHandler(UIElement winPanel, UIElement losePanel)
    {
        _winPanel = winPanel;
        _losePanel = losePanel;
    }
    public void Win()
    {
        _winPanel.Show();
    }

    public void Lose()
    {
        _losePanel.Show();
    }
}
