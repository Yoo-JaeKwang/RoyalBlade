public class PlayerViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.Find("PlayerView").GetComponentAssert<PlayerView>();
        Presenter = new PlayerPresenter();
    }
}