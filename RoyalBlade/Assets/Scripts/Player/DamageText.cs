using TMPro;
using UnityEngine;
using Util;

public class DamageText : MonoBehaviour
{
    private TMP_Text _text;

    private Vector2 _startPoint;
    private Vector2 _wayPoint;
    private Vector2 _endPoint;

    private float _elapsedTime;
    private const float FLOATING_TIME = 0.5f;
    private const float FADE_START_TIME = 0.3f;
    private const float FADE_DURATION_TIME = 0.2f;

    private void Awake() => _text = GetComponent<TMP_Text>();

    public void Initialize(Vector2 dir)
    {
        _startPoint = transform.parent.position;
        transform.position = _startPoint;

        _wayPoint = _startPoint + dir * 100 + Vector2.up * 40;
        _endPoint = _startPoint + dir * 200;

        _elapsedTime = 0;
        _text.color = new Color(0, 1, 1, 1);
    }
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.position = Util.BezierCurve.Quadratic(_startPoint, _wayPoint, _endPoint, _elapsedTime / FLOATING_TIME);
        if (_elapsedTime >= FADE_START_TIME)
        {
            float fadeRate = 1 - (_elapsedTime - FADE_START_TIME) / FADE_DURATION_TIME;
            _text.color = new Color(0, 1, 1, fadeRate);
        }

        if (_elapsedTime >= FLOATING_TIME)
        {
            _pool.Release(this);
        }
    }

    private ObjectPool<DamageText> _pool;

    public void SetPoolRef(ObjectPool<DamageText> pool) => _pool = pool;
}
