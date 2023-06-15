using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public GameManager GameManager { get; private set; }
    public MonsterManager MonsterManager { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public Player Player;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        SetManager();
    }
    private void SetManager()
    {
        GameManager = gameObject.GetComponentInChildrenAssert<GameManager>();
        MonsterManager = gameObject.GetComponentInChildrenAssert<MonsterManager>();
        SoundManager = gameObject.GetComponentInChildrenAssert<SoundManager>();
    }
}