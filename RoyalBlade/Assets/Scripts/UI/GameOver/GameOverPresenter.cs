using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class GameOverPresenter : Presenter
{
    private GameOverView _gameOverView;
    private CompositeDisposable _compositeDisposable = new();
    public override void OnInitialize(View view)
    {
        _gameOverView = view as GameOverView;

        InitializeRx();
    }

    public override void OnRelease()
    {
        _gameOverView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _gameOverView.HomeButton.OnPointerUpAsObservable().Subscribe(OnHomeButtonUp).AddTo(_compositeDisposable);
    }
    private void OnHomeButtonUp(PointerEventData pointerEventData)
    {
        GameOverModel.GoTitle();
    }
    protected override void OnUpdatedModel()
    {
        
    }
}