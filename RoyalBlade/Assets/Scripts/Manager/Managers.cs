using UnityEngine;

public class Managers : SingletonBehaviour<Managers>
{
    public MonsterManager MonsterManager { get; private set; }
    private void Awake()
    {
        SetManager();
    }
    private void SetManager()
    {
        MonsterManager = gameObject.GetComponentInChildrenAssert<MonsterManager>();
    }
}