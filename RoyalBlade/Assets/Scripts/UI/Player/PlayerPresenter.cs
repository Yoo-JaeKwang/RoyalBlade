using UniRx;
using UniRx.Triggers;
using UnityEngine;
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


    protected override void OnOccuredUserEvent()
    {
        _playerView.JumpButton.OnPointerDownAsObservable().Subscribe(OnJumpButtonDown).AddTo(_compositeDisposable);
        _playerView.JumpButton.OnPointerUpAsObservable().Subscribe(OnJumpButtonUp).AddTo(_compositeDisposable);

        _playerView.AttackButton.OnPointerDownAsObservable().Subscribe(OnAttack).AddTo(_compositeDisposable);

    }
    private void OnJumpButtonDown(PointerEventData pointerEventData)
    {
        PlayerModel.JumpReady();
    }
    private void OnJumpButtonUp(PointerEventData pointerEventData)
    {
        PlayerModel.JumpStart();
    }
    private void OnAttack(PointerEventData pointerEventData)
    {
        PlayerModel.Attack();
    }
}