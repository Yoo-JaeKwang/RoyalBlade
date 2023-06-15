using UnityEngine;
using Util;

public class CoinEffectPool
{
    private CoinEffect _coinEffectPrefab;
    private ObjectPool<CoinEffect> _coinEffectPool;
    private Transform _transform;
    public CoinEffect GetCoinEffectFromPool() => _coinEffectPool.Get();

    public void Initialize(CoinEffect coinEffectPrefab, Transform transform)
    {
        _coinEffectPrefab = coinEffectPrefab;
        _transform = transform;
        InitializeCoinEffectPool();
    }
    private void InitializeCoinEffectPool() => _coinEffectPool = new ObjectPool<CoinEffect>(CreateCoinEffect, OnGetCoinEffectFromPool, OnReleaseCoinEffectToPool, OnDestroyCoinEffect);
    private CoinEffect CreateCoinEffect()
    {
        CoinEffect coinEffect = Object.Instantiate(_coinEffectPrefab);
        coinEffect.transform.SetParent(_transform, false);
        coinEffect.SetPoolRef(_coinEffectPool);

        return coinEffect;
    }
    private void OnGetCoinEffectFromPool(CoinEffect coinEffect) => coinEffect.gameObject.SetActive(true);
    private void OnReleaseCoinEffectToPool(CoinEffect coinEffect) => coinEffect.gameObject.SetActive(false);
    private void OnDestroyCoinEffect(CoinEffect coinEffect) => Object.Destroy(coinEffect.gameObject);
}
