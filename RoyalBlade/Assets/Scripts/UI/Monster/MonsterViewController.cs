public class MonsterViewController : ViewController
{
    protected override void Initialize()
    {
        View = transform.Find("MonsterView").GetComponentAssert<MonsterView>();
        Presenter = new MonsterPresenter();
    }
}