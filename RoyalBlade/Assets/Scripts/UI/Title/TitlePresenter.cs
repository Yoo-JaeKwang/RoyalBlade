using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class TitlePresenter : Presenter
{
    private TitleView _titleView;
    private CompositeDisposable _compositeDisposable = new();
    public override void OnInitialize(View view)
    {
        _titleView = view as TitleView;

        InitializeRx();
    }

    public override void OnRelease()
    {
        _titleView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _titleView.StartPanel.OnPointerDownAsObservable().Subscribe(OnGameStart).AddTo(_compositeDisposable);
    }
    private void OnGameStart(PointerEventData pointerEventData)
    {
        _titleView.StartPanel.gameObject.SetActive(false);
        TitleModel.GameStart();
        _titleView.Move();
        Managers.Instance.GameManager.GameStart();        
    }

    protected override void OnUpdatedModel()
    {
        
    }
}