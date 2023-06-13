using UnityEngine;
using Util;

public class MonsterPool
{
    private int _monsterID;
    private ObjectPool<Monster> _monsterPool;

    public Monster GetMonsterFromPool() => _monsterPool.Get();

    public void Initialize(int monsterID)
    {
        _monsterID = monsterID;
        InitializeMonsterPool();
    }
    private void InitializeMonsterPool() => _monsterPool = new ObjectPool<Monster>(CreateMonster, OnGetMonsterFromPool, OnReleaseMonsterToPool, OnDestroyMonster);
    private Monster CreateMonster()
    {
        Monster monster = Object.Instantiate(Managers.Instance.MonsterManager.Monsters[_monsterID]);
        monster.SetPoolRef(_monsterPool);

        return monster;
    }
    private void OnGetMonsterFromPool(Monster monster) => monster.gameObject.SetActive(true);
    private void OnReleaseMonsterToPool(Monster monster) => monster.gameObject.SetActive(false);
    private void OnDestroyMonster(Monster monster) => Object.Destroy(monster.gameObject);
}