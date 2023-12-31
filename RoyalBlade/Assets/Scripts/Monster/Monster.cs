﻿using System;
using UnityEngine;
using Util;

public class Monster : MonoBehaviour
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
        IsReleased = false;
    }
    public void OnDefense()
    {
        Rigidbody2D rb = Managers.Instance.MonsterManager.Container.GetComponentAssert<Rigidbody2D>();
        rb.velocity = default;
        rb.AddForce(Vector2.up * 200_000_000);
    }
    public void OnAttack(int damage)
    {
        HP -= damage;
        MonsterModel.SetCurHealth(HP);

        if (HP <= 0)
        {
            OnDead?.Invoke(Score);
            IsReleased = true;
            _pool.Release(this);

            if (Managers.Instance.MonsterManager.CurMonsters.Count == Index + 1)
            {
                HP = 0;
                MonsterModel.SetCurHealth(HP);
                Managers.Instance.MonsterManager.CurMonsters.Clear();
                Managers.Instance.MonsterManager.SummonMonster();
                return;
            }

            Monster next = Managers.Instance.MonsterManager.CurMonsters[Index + 1];
            MonsterModel.SetCurHealth(next.HP);
            MonsterModel.SetMaxHealth(next.HP);
        }
    }
    public bool IsReleased { get; set; }
    private Util.ObjectPool<Monster> _pool;
    public void SetPoolRef(ObjectPool<Monster> pool) => _pool = pool;
    public void Release() =>_pool.Release(this);
}