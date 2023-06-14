using System;
using UnityEngine;
using Util;

public abstract class Monster : MonoBehaviour
{
    public event Action<int> OnDead;
    public int Default_HP;
    public int HP { get; private set; }
    public int Score { get; private set; }
    public int Index { get; private set; }
    public void Init(int hp, int score, int index)
    {
        HP = hp;
        Score = score;
        Index = index;
    }

    public void OnAttack(int damage)
    {
        HP -= damage;
        MonsterModel.SetCurHealth(HP);

        if (HP <= 0)
        {
            OnDead?.Invoke(Score);
            _pool.Release(this);

            Monster next = Managers.Instance.MonsterManager.CurMonsters[Index + 1];
            if (next == null)
            {
                HP = 0;
                MonsterModel.SetCurHealth(HP);
                Managers.Instance.MonsterManager.CurMonsters.Clear();
                return;
            }

            MonsterModel.SetCurHealth(next.HP);
            MonsterModel.SetMaxHealth(next.HP);
        }
    }

    private Util.ObjectPool<Monster> _pool;
    /// <summary>
    /// 반환되어야할 풀의 주소를 설정합니다.
    /// </summary>
    public void SetPoolRef(ObjectPool<Monster> pool) => _pool = pool;
}