using Cysharp.Text;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    public Image[] HPOnImages { get; private set; }
    public Image OnAttackImage { get; private set; }
    public TMP_Text ScoreText { get; private set; }
    public TMP_Text ATKText { get; private set; }
    public Button JumpButton { get; private set; }
    public Button DefenseButton { get; private set; }
    public Button AttackButton { get; private set; }
    public Button ATKBuyButton { get; private set; }
    public Button HPBuyButton { get; private set; }
    private void Awake()
    {
        Transform HPGroup = transform.FindAssert("HPGroup");

        HPOnImages = new Image[HPGroup.childCount];
        for (int i = 0; i < HPOnImages.Length; ++i)
        {
            HPOnImages[i] = HPGroup.FindAssert(ZString.Concat("HPOn_0", Util.Nums.GetNumString(i + 1))).GetComponentAssert<Image>();
        }
        OnAttackImage = transform.FindAssert("OnAttackImage").GetComponentAssert<Image>();
        ScoreText = transform.FindAssert("ScoreText").GetComponentAssert<TMP_Text>();
        ATKText = transform.FindAssert("ATKText").GetComponentAssert<TMP_Text>();
        JumpButton = transform.FindAssert("JumpButton").GetComponentAssert<Button>();
        DefenseButton = transform.FindAssert("DefenseButton").GetComponentAssert<Button>();
        AttackButton = transform.FindAssert("AttackButton").GetComponentAssert<Button>();
        ATKBuyButton = transform.FindAssert("ATKBuyButton").GetComponentAssert<Button>();
        HPBuyButton = transform.FindAssert("HPBuyButton").GetComponentAssert<Button>();

        _onAttackCo = OnAttackCo();
    }
    public void OnAttack()
    {
        if (Managers.Instance.GameManager.PlayerHP <= 0)
        {
            return;
        }

        StartCoroutine(_onAttackCo);
    }

    private readonly Color Start = new(1, 0, 0, 0);
    private readonly Color End = new(1, 0, 0, 0.5f);
    private float _elapsedTime;
    private IEnumerator _onAttackCo;
    private IEnumerator OnAttackCo()
    {
        while (true)
        {
            OnAttackImage.color = Color.Lerp(Start, End, Mathf.Sin(_elapsedTime * 20));
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > 0.2f)
            {
                _elapsedTime = 0;
                OnAttackImage.color = Start;
                StopCoroutine(_onAttackCo);
            }
            yield return null;
        }
    }
}