using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Title;
    public GameObject InGame;
    public GameObject GameOver;
    public int PlayerHP { get; private set; }
    public int Score { get; private set; }
    private void Start()
    {
        InGame.SetActive(false);
        //GameOver.SetActive(false);
    }
    public void GameStart()
    {
        Invoke(nameof(OnGameStart), 1.1f);
    }
    private void OnGameStart()
    {
        InGame.SetActive(true);

        Managers.Instance.Player.GameStart();
        GameSetting();
        Managers.Instance.MonsterManager.GameStart();

        Title.SetActive(false);
    }
    public void GameEnd()
    {
        Managers.Instance.Player.GameEnd();
    }
    public void GameSetting()
    {
        PlayerHP = 3;
        Score = 0;

        PlayerModel.SetHP(PlayerHP);
        PlayerModel.SetScore(Score);
    }
    public void OnAttack()
    {
        PlayerHP -= 1;
        if (PlayerHP <= 0)
        {
            GameEnd();
        }
        PlayerModel.SetHP(PlayerHP);
    }
    public void OnGetScore(int score)
    {
        Score += score;
        PlayerModel.SetScore(Score);
    }
}