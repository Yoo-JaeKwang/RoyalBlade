using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _am;
    private GameObject _jumpEffect;
    private void Awake()
    {
        _rb = gameObject.GetComponentAssert<Rigidbody2D>();
        _am = gameObject.GetComponentAssert<Animator>();

        Transform effects = transform.FindAssert("Effects");
        _jumpEffect = effects.FindAssert("JumpEffect").gameObject;
        _jumpEffect.transform.parent = default;
        _jumpEffect.SetActive(false);

        TitleModel.OnGameStart -= GameStart;
        TitleModel.OnGameStart += GameStart;
    }
    private void OnEnable()
    {
        _rb.velocity = default;
        transform.position = default;
    }
    private readonly int Jump_01 = Animator.StringToHash("Jump_01");
    public void GameStart()
    {
        _jumpEffect.SetActive(false);
        _jumpEffect.SetActive(true);
        _rb.AddForce(Vector2.up * 200_000);
        _am.SetTrigger(Jump_01);
    }
}
