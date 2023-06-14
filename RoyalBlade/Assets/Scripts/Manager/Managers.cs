using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public MonsterManager MonsterManager { get; private set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        SetManager();
    }
    private void SetManager()
    {
        MonsterManager = gameObject.GetComponentInChildrenAssert<MonsterManager>();
    }
}