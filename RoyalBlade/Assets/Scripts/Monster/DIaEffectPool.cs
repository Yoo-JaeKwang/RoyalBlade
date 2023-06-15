using UnityEngine;
using Util;

public class DiaEffectPool
{
    private DiaEffect _diaEffectPrefab;
    private ObjectPool<DiaEffect> _diaEffectPool;
    private Transform _transform;
    public DiaEffect GetDiaEffectFromPool() => _diaEffectPool.Get();

    public void Initialize(DiaEffect diaEffectPrefab, Transform transform)
    {
        _diaEffectPrefab = diaEffectPrefab;
        _transform = transform;
        InitializeDiaEffectPool();
    }
    private void InitializeDiaEffectPool() => _diaEffectPool = new ObjectPool<DiaEffect>(CreateDiaEffect, OnGetDiaEffectFromPool, OnReleaseDiaEffectToPool, OnDestroyDiaEffect);
    private DiaEffect CreateDiaEffect()
    {
        DiaEffect diaEffect = Object.Instantiate(_diaEffectPrefab);
        diaEffect.transform.SetParent(_transform, false);
        diaEffect.SetPoolRef(_diaEffectPool);

        return diaEffect;
    }
    private void OnGetDiaEffectFromPool(DiaEffect diaEffect) => diaEffect.gameObject.SetActive(true);
    private void OnReleaseDiaEffectToPool(DiaEffect diaEffect) => diaEffect.gameObject.SetActive(false);
    private void OnDestroyDiaEffect(DiaEffect diaEffect) => Object.Destroy(diaEffect.gameObject);
}
