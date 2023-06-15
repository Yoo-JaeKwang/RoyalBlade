using UnityEngine;
using Util;

public class GameManager : MonoBehaviour
{
    public GameObject Title;
    public GameObject InGame;
    public GameObject GameOver;

    public Sound[] BGMs;

    public int PlayerHP { get; private set; }
    public int Score { get; private set; }
    private void Start()
    {
        InGame.SetActive(false);
        GameOver.SetActive(false);

        GameOverModel.OnGoTitle -= GoTitle;
        GameOverModel.OnGoTitle += GoTitle;

        BGMs = new Sound[3];
        BGMs[0] = Managers.Instance.SoundManager.PlaySound(SoundID.TitleBGM);
        BGMs[1] = Managers.Instance.SoundManager.PlaySound(SoundID.InGameBGM);
        BGMs[1].StopPlaying();
        BGMs[2] = Managers.Instance.SoundManager.PlaySound(SoundID.GameOver);
        BGMs[2].StopPlaying();
    }
    public void GameStart()
    {
        Invoke(nameof(OnGameStart), 1.1f);

        Managers.Instance.SoundManager.PlaySound(SoundID.Start).SetVolume(0.6f);
    }
    private void OnGameStart()
    {
        InGame.SetActive(true);

        Managers.Instance.Player.GameStart();
        GameSetting();
        Managers.Instance.MonsterManager.GameStart();

        Title.SetActive(false);

        BGMs[(int)SoundID.TitleBGM].StopPlaying();
        BGMs[(int)SoundID.InGameBGM].Play();
    }
    public void GameEnd()
    {
        GameOver.SetActive(true);

        Managers.Instance.Player.GameEnd();
        InGame.SetActive(false);

        BGMs[(int)SoundID.InGameBGM].StopPlaying();
        BGMs[(int)SoundID.GameOver].Play();
    }
    public void GoTitle()
    {
        GameOver.SetActive(false);
        Title.SetActive(true);

        BGMs[(int)SoundID.GameOver].StopPlaying();
        BGMs[(int)SoundID.TitleBGM].Play();
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
        CMCamera.Shake(0.7f, 60);        
        if (PlayerHP <= 0)
        {
            GameEnd();
        }
        PlayerModel.SetHP(PlayerHP);

        Managers.Instance.SoundManager.PlaySound(SoundID.Hit);
    }
    public void OnGetScore(int score)
    {
        Score += score;
        PlayerModel.SetScore(Score);
    }
    public void OnBuyHP()
    {
        if (Score < 1000)
        {
            return;
        }
        if (PlayerHP >= 3)
        {
            return;
        }
        OnGetScore(-1000);
        PlayerHP += 1;
        PlayerModel.SetHP(PlayerHP);
    }
}