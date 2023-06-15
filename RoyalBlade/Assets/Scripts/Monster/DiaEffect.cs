using UnityEngine;
using UnityEngine.UI;
using Util;

public class DiaEffect : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;
    private void Awake()
    {
        _rectTransform = gameObject.GetComponentAssert<RectTransform>();
        _image = gameObject.GetComponentAssert<Image>();
    }

    private Vector2 _startPos;
    private Vector2 _wayPos;
    private Vector2 _endPos;
    private bool _isReleased;
    private Color StartColor = new(1, 1, 1, 1);
    private Color EndColor = new(1, 1, 1, 0);
    public void Initialize()
    {
        float x = Random.Range(-115f, 115f);
        _startPos.Set(0, 0);
        _wayPos.Set(x * 3, 540 + Random.Range(-270, 270));
        _endPos.Set(x * 10, 200);
        _elapsedTime = 0;
        _duration = Random.Range(0.5f, 1f);
        _rectTransform.anchoredPosition = _startPos;
        _rectTransform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
        _isReleased = false;
    }
    private float _elapsedTime;
    private float _duration;
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _rectTransform.anchoredPosition = Util.BezierCurve.Quadratic(_startPos, _wayPos, _endPos, _elapsedTime / _duration);
        _image.color = Color.Lerp(StartColor, EndColor, _elapsedTime / _duration);
        if (_elapsedTime > _duration)
        {
            _isReleased = true;
            _pool.Release(this);
        }
    }
    private void OnDisable()
    {
        if (false == transform.parent.gameObject.activeSelf && false == _isReleased)
        {
            _isReleased = true;
            _pool.Release(this);
        }
    }
    private ObjectPool<DiaEffect> _pool;
    public void SetPoolRef(ObjectPool<DiaEffect> pool) => _pool = pool;
}