using Cysharp.Text;
using TMPro;
using UnityEngine;
using Util;

public class Player : MonoBehaviour
{
    public float JumpSpeed = 100_000;
    private int _damage = 1;
    private bool _isJump = false;
    private Rigidbody2D _rb;
    private GameObject _jumpEffect;
    private AttackEffect[] _attackEffects;
    private Animator _am;
    private CircleCollider2D _col;
    private Attack _attack;
    public DamageText DamageText;
    private Transform _damageTexts;
    private DamageTextPool _damageTextPool = new();
    public CoinEffect CoinEffect;
    private Transform _effects;
    private CoinEffectPool _coinEffectPool = new();
    public DiaEffect DiaEffect;
    private DiaEffectPool _diaEffectPool = new();
    private GameObject _defenseEffect;
    private void Awake()
    {
        _rb = gameObject.GetComponentAssert<Rigidbody2D>();
        _am = gameObject.GetComponentAssert<Animator>();
        _col = gameObject.GetComponentAssert<CircleCollider2D>();
        _col.enabled = false;

        _effects = transform.FindAssert("Effects");
        _jumpEffect = _effects.FindAssert("JumpEffect").gameObject;
        _jumpEffect.transform.parent = default;
        _jumpEffect.SetActive(false);
        _attackEffects = new AttackEffect[5];
        for (int i = 1; i < _attackEffects.Length; ++i)
        {
            _attackEffects[i] = _effects.FindAssert(ZString.Concat("AttackEffect_0", Util.Nums.GetNumString(i))).GetComponentAssert<AttackEffect>();
            _attackEffects[i].Init(i);
            _attackEffects[i].gameObject.SetActive(false);
        }
        _defenseEffect = _effects.FindAssert("DefenseEffect").gameObject;
        _defenseEffect.SetActive(false);

        _attack = transform.FindAssert("Attack").GetComponentAssert<Attack>();

        _damageTexts = transform.FindAssert("DamageTexts");

        _damageTextPool.Initialize(DamageText);
        _coinEffectPool.Initialize(CoinEffect,_damageTexts);
        _diaEffectPool.Initialize(DiaEffect,_damageTexts);
    }
    public void GetEffect(int score)
    {
        for (int i = 0; i < 10; ++i)
        {
            _coinEffectPool.GetCoinEffectFromPool().Initialize();
            _diaEffectPool.GetDiaEffectFromPool().Initialize();
        }
    }
    public void GameStart()
    {
        _damage = 20;
        _isJump = false;
        _comboCount = 1;
        CMCamera.SetCameraFollow(transform);

        AddEvent();
        PlayerModel.SetATK(_damage);
    }
    public void GameEnd()
    {
        _am.Play(Idle_01);
        RemoveEvent();
        CMCamera.SetCameraFollow(default);
    }
    private void Update()
    {
        if (transform.position.y < -1)
        {
            _rb.velocity = default;
            transform.position = default;
        }
    }
    private void AddEvent()
    {
        PlayerModel.OnJumpReady -= JumpReady;
        PlayerModel.OnJumpReady += JumpReady;
        PlayerModel.OnJumpStart -= JumpStart;
        PlayerModel.OnJumpStart += JumpStart;
        PlayerModel.OnDefenseStart -= DefenseStart;
        PlayerModel.OnDefenseStart += DefenseStart;
        PlayerModel.OnDefenseExit -= DefenseExit;
        PlayerModel.OnDefenseExit += DefenseExit;
        PlayerModel.OnAttack -= Attack;
        PlayerModel.OnAttack += Attack;
        PlayerModel.OnBuyATK -= BuyATK;
        PlayerModel.OnBuyATK += BuyATK;
        PlayerModel.OnBuyHP -= Managers.Instance.GameManager.OnBuyHP;
        PlayerModel.OnBuyHP += Managers.Instance.GameManager.OnBuyHP;
    }
    private void RemoveEvent()
    {
        PlayerModel.OnJumpReady -= JumpReady;
        PlayerModel.OnJumpStart -= JumpStart;
        PlayerModel.OnDefenseStart -= DefenseStart;
        PlayerModel.OnDefenseExit -= DefenseExit;
        PlayerModel.OnAttack -= Attack;
        PlayerModel.OnBuyATK -= BuyATK;
        PlayerModel.OnBuyHP -= Managers.Instance.GameManager.OnBuyHP;
    }
    public void JumpReady()
    {
        if (_isJump)
        {
            return;
        }

        CMCamera.ZoomIn();
    }
    private readonly int Jump_01 = Animator.StringToHash("Jump_01");
    private readonly int Jump_02 = Animator.StringToHash("Jump_02");
    public void JumpStart()
    {
        if (_isJump)
        {
            return;
        }

        CMCamera.ZoomOut();

        _isJump = true;
        _jumpEffect.SetActive(false);
        _jumpEffect.SetActive(true);
        _rb.AddForce(Vector2.up * JumpSpeed);
        _am.SetTrigger((_comboCount % 2) == 1 ? Jump_01 : Jump_02);

        Managers.Instance.SoundManager.PlaySound(SoundID.Jump);
    }
    private readonly int Defense = Animator.StringToHash("Defense");
    public void DefenseStart()
    {
        _am.SetTrigger(Defense);
        _col.enabled = true;
    }
    public void DefenseExit()
    {
        _am.Play((_comboCount % 2) == 1 ? Idle_01 : Idle_02);
        _col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Util.Layer.MonsterBody)
        {
            Monster monster = collision.transform.parent.GetComponentAssert<Monster>();
            monster.OnDefense();
            _rb.velocity = default;
            _rb.AddForce(Vector2.down * JumpSpeed);
            DefenseExit();

            _defenseEffect.SetActive(false);
            _defenseEffect.SetActive(true);

            Managers.Instance.SoundManager.PlaySound(SoundID.Defense).SetVolume(0.6f);
        }
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

        _attack.OnAttack();

        Managers.Instance.SoundManager.PlaySound(SoundID.Attack);
    }

    public int OnAttack()
    {
        int exDamage = _damage / 10;
        int damage = _damage + Random.Range(-exDamage, exDamage + 1);
        DamageText text = _damageTextPool.GetDamageTextFromPool();
        text.transform.SetParent(_damageTexts, false);
        text.Initialize((_comboCount % 2) == 1 ? Vector2.left : Vector2.right);
        text.gameObject.GetComponentAssert<TMP_Text>().text = Util.Nums.GetNumString(damage);

        return damage;
    }
    public void BuyATK()
    {
        if (Managers.Instance.GameManager.Score < 1000)
        {
            return;
        }

        Managers.Instance.GameManager.OnGetScore(-1000);
        _damage += 10;
        PlayerModel.SetATK(_damage);
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

            Managers.Instance.SoundManager.PlaySound(SoundID.Land);
        }
    }
}
