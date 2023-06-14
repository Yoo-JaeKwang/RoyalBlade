public abstract class Presenter
{
    public abstract void OnInitialize(View view);

    public abstract void OnRelease();

    protected void InitializeRx()
    {
        OnOccuredUserEvent();
        OnUpdatedModel();
    }

    protected abstract void OnOccuredUserEvent();

    protected abstract void OnUpdatedModel();
}