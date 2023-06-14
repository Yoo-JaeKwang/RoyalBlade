using System;
using UnityEngine;
using Util;

public abstract class Monster : MonoBehaviour
{
    public event Action<int> OnDead;
    private int _hp;
    private int _score;
    public void Init(int hp, int score)
    {
        _hp = hp;
        _score = score;
    }

    public void OnAttack(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _pool.Release(this);
        }

        OnDead?.Invoke(_score);
    }

    private Util.ObjectPool<Monster> _pool;
    /// <summary>
    /// 반환되어야할 풀의 주소를 설정합니다.
    /// </summary>
    public void SetPoolRef(ObjectPool<Monster> pool) => _pool = pool;
}