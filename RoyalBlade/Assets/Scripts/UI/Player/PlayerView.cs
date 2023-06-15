using Cysharp.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    public Image[] HPOnImages { get; private set; }
    public TMP_Text ScoreText { get; private set; }
    public Button JumpButton { get; private set; }
    public Button DefenseButton { get; private set; }
    public Button AttackButton { get; private set; }
    private void Awake()
    {
        Transform HPGroup = transform.FindAssert("HPGroup");

        HPOnImages = new Image[HPGroup.childCount];
        for(int i = 0; i < HPOnImages.Length; ++i)
        {
            HPOnImages[i] = HPGroup.FindAssert(ZString.Concat("HPOn_0", Util.Nums.GetNumString(i + 1))).GetComponentAssert<Image>();
        }
        ScoreText = transform.Find("ScoreText").GetComponentAssert<TMP_Text>();
        JumpButton = transform.Find("JumpButton").GetComponentAssert<Button>();
        DefenseButton = transform.Find("DefenseButton").GetComponentAssert<Button>();
        AttackButton = transform.Find("AttackButton").GetComponentAssert<Button>();
    }
}