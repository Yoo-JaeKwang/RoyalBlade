using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float JumpSpeed = 100_000;
    private int _damage = 20;
    private bool _isJump = false;
    private Rigidbody2D _rb;
    private GameObject _jumpEffect;
    private AttackEffect[] _attackEffects = new AttackEffect[5];
    private Animator _am;

    private void Awake()
    {
        _rb = gameObject.GetComponentAssert<Rigidbody2D>();
        _am = gameObject.GetComponentAssert<Animator>();

        Transform effects = transform.FindAssert("Effects");
        _jumpEffect = effects.FindAssert("JumpEffect").gameObject;
        _jumpEffect.transform.parent = default;
        _jumpEffect.SetActive(false);
        for (int i = 1; i < _attackEffects.Length; ++i)
        {
            _attackEffects[i] = effects.FindAssert("AttackEffect_0" + i.ToString()).GetComponentAssert<AttackEffect>();
            _attackEffects[i].Init(i);
            _attackEffects[i].gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        AddEvent();
    }
    private void AddEvent()
    {
        PlayerModel.OnJumpReady -= JumpReady;
        PlayerModel.OnJumpReady += JumpReady;
        PlayerModel.OnJumpStart -= JumpStart;
        PlayerModel.OnJumpStart += JumpStart;

        PlayerModel.OnAttack -= Attack;
        PlayerModel.OnAttack += Attack;
    }
    private void RemoveEvent()
    {
        PlayerModel.OnJumpReady -= JumpReady;
        PlayerModel.OnJumpStart -= JumpStart;

        PlayerModel.OnAttack -= Attack;

    }
    public void JumpReady()
    {

    }
    private readonly int Jump_01 = Animator.StringToHash("Jump_01");
    private readonly int Jump_02 = Animator.StringToHash("Jump_02");
    public void JumpStart()
    {
        if (_isJump)
        {
            return;
        }

        _isJump = true;
        _jumpEffect.SetActive(false);
        _jumpEffect.SetActive(true);
        _rb.AddForce(Vector2.up * JumpSpeed);
        _am.SetTrigger((_comboCount % 2) == 1 ? Jump_01 : Jump_02);
    }
    private int _comboCount = 1;
    private readonly int Attack_01 = Animator.StringToHash("Attack_01");
    private readonly int Attack_02 = Animator.StringToHash("Attack_02");
    public void Attack()
    {
        _attackEffects[_comboCount].gameObject.SetActive(false);
        _attackEffects[_comboCount].gameObject.SetActive(true);
        _am.SetTrigger((_comboCount % 2) == 1 ? Attack_01 : Attack_02);
        _comboCount += 1;
        if (_comboCount == _attackEffects.Length)
        {
            _comboCount = 1;
        }

        Collider2D collider = Physics2D.OverlapBox(transform.position + (Vector3.up * 50), Vector2.one * 50, 0, 7) 
            ?? Physics2D.OverlapBox(transform.position + (Vector3.up * 200), Vector2.one * 200, 0, 7);
        if (collider == null)
        {
            return;
        }

        int exDamage = _damage / 10;
        int damage = _damage + Random.Range(-exDamage, exDamage + 1);

        Monster monster = collider.gameObject.GetComponentAssert<Monster>();
        monster.OnAttack(damage);
    }
    private readonly int Idle_01 = Animator.StringToHash("Idle_01");
    private readonly int Idle_02 = Animator.StringToHash("Idle_02");
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Util.Layer.Ground)
        {
            _isJump = false;
            _jumpEffect.SetActive(true);
            _am.Play((_comboCount % 2) == 1 ? Idle_01 : Idle_02);
        }
    }
}
