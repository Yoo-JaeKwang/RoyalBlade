using System;
using UniRx;

public static class PlayerModel
{
    public static readonly ReactiveProperty<int> HP = new();
    public static readonly ReactiveProperty<int> Score = new();
    public static readonly ReactiveProperty<int> ATK = new();
    public static readonly ReactiveProperty<bool> IsOnAttack = new();
    public static event Action OnJumpReady;
    public static event Action OnJumpStart;
    public static event Action OnDefenseStart;
    public static event Action OnDefenseExit;
    public static event Action OnAttack;
    public static event Action OnBuyATK;
    public static event Action OnBuyHP;
    public static void SetHP(int hp)
    {
        HP.Value = hp;
    }
    public static void SetScore(int score)
    {
        Score.Value = score;
    }
    public static void SetATK(int atk)
    {
        ATK.Value = atk;
    }
    public static void SetOnAttack()
    {
        IsOnAttack.Value = !IsOnAttack.Value;
    }
    public static void JumpReady()
    {
        OnJumpReady?.Invoke();
    }
    public static void JumpStart()
    {
        OnJumpStart?.Invoke();
    }
    public static void DefenseStart()
    {
        OnDefenseStart?.Invoke();
    }
    public static void DefenseExit()
    {
        OnDefenseExit?.Invoke();
    }
    public static void Attack()
    {
        OnAttack?.Invoke();
    }
    public static void BuyATK()
    {
        OnBuyATK?.Invoke();
    }
    public static void BuyHP()
    {
        OnBuyHP?.Invoke();
    }
}