using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterView : View
{
    public Image HPGaugeImage { get;private set; }
    public TMP_Text CurHPText { get; private set; }
    public TMP_Text MaxHPText { get; private set; }
    private void Awake()
    {
        HPGaugeImage = transform.FindAssert("HPGauge").GetComponentAssert<Image>();
        Transform HPTextGroup = HPGaugeImage.transform.FindAssert("HPTextGroup");
        CurHPText = HPTextGroup.FindAssert("CurHP").GetComponentAssert<TMP_Text>();
        MaxHPText = HPTextGroup.FindAssert("MaxHP").GetComponentAssert<TMP_Text>();
    }
}