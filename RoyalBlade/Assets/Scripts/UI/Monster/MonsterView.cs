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
        HPGaugeImage = transform.Find("HPGauge").GetComponentAssert<Image>();
        Transform HPTextGroup = HPGaugeImage.transform.Find("HPTextGroup");
        CurHPText = HPTextGroup.Find("CurHP").GetComponentAssert<TMP_Text>();
        MaxHPText = HPTextGroup.Find("MaxHP").GetComponentAssert<TMP_Text>();
    }
}