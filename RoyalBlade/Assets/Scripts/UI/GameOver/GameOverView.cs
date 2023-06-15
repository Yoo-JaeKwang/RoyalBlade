using UnityEngine.UI;

public class GameOverView : View
{
    public Button HomeButton { get; private set; }
    private void Awake()
    {
        HomeButton = transform.FindAssert("HomeButton").GetComponentAssert<Button>();
    }
}