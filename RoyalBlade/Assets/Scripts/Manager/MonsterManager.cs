using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Monster[] Monsters;
    public List<Monster> CurMonsters = new();
    public Transform Container;
    private Rigidbody2D _conRb;
    private Vector3 _startPos;
    private Dictionary<MonsterID, MonsterPool> _monsterPools = new();
    private void Awake()
    {
        _conRb = Container.GetComponentAssert<Rigidbody2D>();
        _startPos = Container.transform.position;
        SetMonsterPool();
    }
    private readonly Vector2 _vel = new(0, -750);
    public void GameStart()
    {
        _wave = 1;
        _waveCount = 0;
        for (int i = 0; i < CurMonsters.Count; ++i)
        {
            if (CurMonsters[i].IsReleased)
            {
                continue;
            }

            CurMonsters[i].Release();
        }
        CurMonsters.Clear();
        SummonMonster();
    }
    private void Update()
    {
        if (_conRb.velocity.y < -750)
        {
            _conRb.velocity = _vel;
        }
    }
    private void SetMonsterPool()
    {
        for (int i = 1; i < Monsters.Length; ++i)
        {
            Debug.Assert(false == _monsterPools.ContainsKey((MonsterID)i));

            MonsterPool monsterPool = new();
            monsterPool.Initialize(i);
            _monsterPools.Add((MonsterID)i, monsterPool);
        }
    }
    private int _summonCount;
    private Vector3 _left = new(-1, 1, 1);
    private Vector3 _right = new(1, 1, 1);
    private int _wave = 1;
    private int _waveCount = 0;
    public void SummonMonster()
    {
        MonsterID ID = Random.Range(0, 2) == 0 ? MonsterID.Wood_01 : MonsterID.Slime_01;
        Container.transform.position = _startPos;
        Container.gameObject.GetComponentAssert<Rigidbody2D>().velocity = default;
        float offset = Monsters[(int)ID].transform.Find("Body").GetComponentAssert<BoxCollider2D>().size.y;
        _summonCount = Random.Range(6, 12);
        for (int i = 0; i < _summonCount; ++i)
        {
            MonsterID summonID = ID + (Random.Range(0, 6) == 5 ? 1 : 0);
            Monster monster = _monsterPools[summonID].GetMonsterFromPool();
            monster.transform.SetParent(Container);
            monster.transform.localPosition = Vector3.up * (offset * i);
            monster.transform.localScale = Random.Range(0, 2) == 0 ? _left : _right;

            int hp = monster.Default_HP * (int)Mathf.Pow(2, _wave - 1);
            monster.Init(hp, hp * 20, i);

            monster.OnDead -= Managers.Instance.GameManager.OnGetScore;
            monster.OnDead += Managers.Instance.GameManager.OnGetScore;
            monster.OnDead -= Managers.Instance.Player.GetEffect;
            monster.OnDead += Managers.Instance.Player.GetEffect;

            CurMonsters.Add(monster);
        }

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