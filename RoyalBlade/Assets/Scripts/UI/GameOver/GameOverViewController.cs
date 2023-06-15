public class GameOverViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.FindAssert("GameOverView").GetComponentAssert<GameOverView>();
        Presenter = new GameOverPresenter();
    }
}