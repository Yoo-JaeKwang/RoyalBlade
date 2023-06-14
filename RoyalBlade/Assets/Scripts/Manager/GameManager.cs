using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerHP { get; private set; }
    public int Score { get; private set; }

    private void Start()
    {
        GameSetting();
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
        PlayerModel.SetHP(PlayerHP);
    }
    public void OnGetScore(int score)
    {
        Score += score;
        PlayerModel.SetScore(Score);
    }
}