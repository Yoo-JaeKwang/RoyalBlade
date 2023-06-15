public class TitleViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.FindAssert("TitleView").GetComponentAssert<TitleView>();
        Presenter = new TitlePresenter();
    }
}