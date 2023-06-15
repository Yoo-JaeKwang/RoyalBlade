using System;

public static class GameOverModel
{
    public static event Action OnGoTitle;
    public static void GoTitle()
    {
        OnGoTitle?.Invoke();
    }
}