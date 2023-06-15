using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class PlayerPresenter : Presenter
{
    private PlayerView _playerView;
    private CompositeDisposable _compositeDisposable = new();
    public override void OnInitialize(View view)
    {
        _playerView = view as PlayerView;

        InitializeRx();
    }

    public override void OnRelease()
    {
        _playerView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnUpdatedModel()
    {
        PlayerModel.HP.Subscribe(UpdateHP).AddTo(_compositeDisposable);
        PlayerModel.Score.Subscribe(UpdateScore).AddTo(_compositeDisposable);
        PlayerModel.ATK.Subscribe(UpdateATK).AddTo(_compositeDisposable);
        PlayerModel.IsOnAttack.Subscribe(UpdateOnAttack).AddTo(_compositeDisposable);
    }
    private void UpdateHP(int value)
    {
        for (int i = 0; i < _playerView.HPOnImages.Length; ++i)
        {
            _playerView.HPOnImages[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < value; ++i)
        {
            _playerView.HPOnImages[i].gameObject.SetActive(true);
        }
    }
    private void UpdateScore(int value)
    {
        _playerView.ScoreText.text = Util.Nums.GetNumString(value);
    }
    private void UpdateATK(int value)
    {
        _playerView.ATKText.text = Util.Nums.GetNumString(value);
    }
    private void UpdateOnAttack(bool value)
    {
        _playerView.OnAttack();
    }
    protected override void OnOccuredUserEvent()
    {
        _playerView.JumpButton.OnPointerDownAsObservable().Subscribe(OnJumpButtonDown).AddTo(_compositeDisposable);
        _playerView.JumpButton.OnPointerUpAsObservable().Subscribe(OnJumpButtonUp).AddTo(_compositeDisposable);
        _playerView.DefenseButton.OnPointerDownAsObservable().Subscribe(OnDefenseButtonDown).AddTo(_compositeDisposable);
        _playerView.DefenseButton.OnPointerUpAsObservable().Subscribe(OnDefenseButtonUp).AddTo(_compositeDisposable);
        _playerView.AttackButton.OnPointerDownAsObservable().Subscribe(OnAttack).AddTo(_compositeDisposable);
        _playerView.ATKBuyButton.OnPointerDownAsObservable().Subscribe(OnBuyATK).AddTo(_compositeDisposable);
        _playerView.HPBuyButton.OnPointerDownAsObservable().Subscribe(OnBuyHP).AddTo(_compositeDisposable);
    }
    private void OnJumpButtonDown(PointerEventData pointerEventData)
    {
        PlayerModel.JumpReady();
    }
    private void OnJumpButtonUp(PointerEventData pointerEventData)
    {
        PlayerModel.JumpStart();
    }
    private void OnDefenseButtonDown(PointerEventData pointerEventData)
    {
        PlayerModel.DefenseStart();
    }
    private void OnDefenseButtonUp(PointerEventData pointerEventData)
    {
        PlayerModel.DefenseExit();
    }
    private void OnAttack(PointerEventData pointerEventData)
    {
        PlayerModel.Attack();
    }
    private void OnBuyATK(PointerEventData pointerEventData)
    {
        PlayerModel.BuyATK();
    }
    private void OnBuyHP(PointerEventData pointerEventData)
    {
        PlayerModel.BuyHP();
    }
}