using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : View
{
    public Image TitleImage { get; private set; }
    public Vector3[] ImagePos { get; private set; }
    public TMP_Text StartText { get; private set; }
    public Vector3[] TextPos { get; private set; }
    public Button StartPanel { get; private set; }
    private void Awake()
    {
        TitleImage = transform.FindAssert("TitleImage").GetComponentAssert<Image>();
        StartText = transform.FindAssert("StartText").GetComponentAssert<TMP_Text>();
        StartPanel = transform.FindAssert("StartPanel").GetComponentAssert<Button>();

        ImagePos = new Vector3[2];
        TextPos = new Vector3[2];

        ImagePos[0] = TitleImage.transform.position;
        TextPos[0] = StartText.transform.position;

        ImagePos[1] = ImagePos[0] + Vector3.up * 2000;
        TextPos[1] = TextPos[0] + Vector3.right * 2000;

        _moveCo = MoveCo();
    }
    public void Move() => StartCoroutine(_moveCo);
    private float _elapsedTime;
    private IEnumerator _moveCo;
    private IEnumerator MoveCo()
    {
        while (true)
        {
            TitleImage.transform.position = Vector3.Lerp(ImagePos[0], ImagePos[1], _elapsedTime);
            StartText.transform.position = Vector3.Lerp(TextPos[0], TextPos[1], _elapsedTime);
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > 1)
            {
                _elapsedTime = 0;

                StopCoroutine(_moveCo);
            }
            yield return null;
        }
    }
}