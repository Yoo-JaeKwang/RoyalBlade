using UniRx;

public class MonsterPresenter : Presenter
{
    private MonsterView _monsterView;
    private CompositeDisposable _compositeDisposable = new();

    public override void OnInitialize(View view)
    {
        _monsterView = view as MonsterView;

        InitializeRx();
    }

    public override void OnRelease()
    {
        _monsterView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnUpdatedModel()
    {
        MonsterModel.CurHealth.Subscribe(UpdateGauge).AddTo(_compositeDisposable);
        MonsterModel.CurHealth.Subscribe(UpdateCurText).AddTo(_compositeDisposable);
        MonsterModel.MaxHealth.Subscribe(UpdateGauge).AddTo(_compositeDisposable);
        MonsterModel.MaxHealth.Subscribe(UpdateMaxText).AddTo(_compositeDisposable);
    }
    private void UpdateGauge(int value)
    {
        _monsterView.HPGaugeImage.fillAmount = (float)MonsterModel.CurHealth.Value / MonsterModel.MaxHealth.Value;
    }
    private void UpdateCurText(int value)
    {
        _monsterView.CurHPText.text = Util.Nums.GetNumString(value);
    }
    private void UpdateMaxText(int value)
    {
        _monsterView.MaxHPText.text = Util.Nums.GetNumString(value);
    }
    protected override void OnOccuredUserEvent()
    {

    }
}