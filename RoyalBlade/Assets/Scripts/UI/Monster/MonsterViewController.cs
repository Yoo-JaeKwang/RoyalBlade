public class MonsterViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.FindAssert("MonsterView").GetComponentAssert<MonsterView>();
        Presenter = new MonsterPresenter();
    }
}