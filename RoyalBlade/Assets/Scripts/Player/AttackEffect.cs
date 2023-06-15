using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _startScale;
    private Color _startColor;
    private readonly Vector3 End_Left_Pos = new(-150, 150, 0);
    private readonly Vector3 End_Right_Pos = new(100, 150, 0);
    private readonly Vector3 End_Left_Scale = new(0.5f, 0.5f, 0.5f);
    private readonly Vector3 End_Right_Scale = new(-0.5f, 0.5f, 0.5f);
    private readonly Color End_Color = new(1, 1, 1, 0);
    private SpriteRenderer _sr;
    private float _elapsedTime;
    private readonly float Fade_Time = 0.7f;
    private int _combo;
    private void Awake()
    {
        _sr = gameObject.GetComponentAssert<SpriteRenderer>();
        _startPos = transform.localPosition;
        _startScale = transform.localScale;
        _startColor = _sr.color;
    }
    private void OnEnable()
    {
        transform.localPosition = _startPos;
        transform.localScale = _startScale;
        _sr.color = _startColor;
        _elapsedTime = 0;
    }
    private void Update()
    {
        float t = _elapsedTime / Fade_Time;

        transform.localPosition = (_combo & 1) == 1 ? Vector3.Lerp(_startPos, End_Left_Pos, t) : Vector3.Lerp(_startPos, End_Right_Pos, t);
        transform.localScale = (_combo & 1) == 1 ? Vector3.Lerp(_startScale, End_Left_Scale, t) : Vector3.Lerp(_startScale, End_Right_Scale, t);
        _sr.color = Color.Lerp(_startColor, End_Color, t);

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > Fade_Time)
        {
            gameObject.SetActive(false);
        }
    }
    public void Init(int combo) => _combo = combo;
}
