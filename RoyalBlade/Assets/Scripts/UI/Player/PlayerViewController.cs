public class PlayerViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.FindAssert("PlayerView").GetComponentAssert<PlayerView>();
        Presenter = new PlayerPresenter();
    }
}