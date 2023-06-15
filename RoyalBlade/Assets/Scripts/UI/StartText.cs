using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    private TMP_Text _text;
    private readonly Color Start = new(1, 1, 1, 1);
    private readonly Color End = new(1, 1, 1, 0.2f);
    private float _elapsedTime;
    private void Awake() => _text = gameObject.GetComponentAssert<TMP_Text>();
    private void Update()
    {
        _text.color = Color.Lerp(Start, End, (Mathf.Sin(_elapsedTime * 4) + 1) / 2);
        _elapsedTime += Time.deltaTime;
    }
}
