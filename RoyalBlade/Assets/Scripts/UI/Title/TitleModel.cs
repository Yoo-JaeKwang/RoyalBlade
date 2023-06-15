using System;

public static class TitleModel
{
    public static event Action OnGameStart;

    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }
}
