using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Monster[] Monsters;
    public List<Monster> CurMonsters = new();
    public Transform Container;
    private Vector3 _startPos;
    private Dictionary<MonsterID, MonsterPool> _monsterPools = new();
    private void Start()
    {
        _startPos = Container.transform.position;
        SetMonsterPool();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SummonMonster(MonsterID.Wood_01);
        }
    }
    private void SetMonsterPool()
    {
        for (int i = 1; i < Monsters.Length; ++i)
        {
            Debug.Assert(_monsterPools.ContainsKey((MonsterID)i));

            MonsterPool monsterPool = new();
            monsterPool.Initialize(i);
            _monsterPools.Add((MonsterID)i, monsterPool);
        }
    }
    private int _summonCount = 6;
    private Vector3 _left = new(-1, 1, 1);
    private Vector3 _right = new(1, 1, 1);
    private int _wave = 1;
    private int _waveCount = 0;
    private void SummonMonster(MonsterID ID)
    {
        Container.transform.position = _startPos;
        Container.gameObject.GetComponentAssert<Rigidbody2D>().velocity = default;
        float offset = Monsters[(int)ID].gameObject.GetComponentAssert<BoxCollider2D>().size.y;
        for (int i = 0; i < _summonCount; ++i)
        {
            Monster monster = _monsterPools[ID].GetMonsterFromPool();
            monster.transform.SetParent(Container);
            monster.transform.localPosition = Vector3.up * (offset * i);
            monster.transform.localScale = Random.Range(0, 2) == 0 ? _left : _right;

            int hp = monster.Default_HP * (int)Mathf.Pow(2, _wave - 1);
            monster.Init(hp, hp * 10, i);

            CurMonsters.Add(monster);
        }

        CurMonsters.Add(null);

        MonsterModel.SetCurHealth(CurMonsters[0].HP);
        MonsterModel.SetMaxHealth(CurMonsters[0].HP);

        _waveCount += 1;
        if (_waveCount == 4)
        {
            _waveCount = 0;
            _wave += 1;
        }
    }
}