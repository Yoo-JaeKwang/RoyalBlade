using UnityEngine;
using Util;

public abstract class Monster : MonoBehaviour
{
    private Util.ObjectPool<Monster> _pool;
    /// <summary>
    /// 반환되어야할 풀의 주소를 설정합니다.
    /// </summary>
    public void SetPoolRef(ObjectPool<Monster> pool) => _pool = pool;
    protected virtual void ReleaseToPool() => _pool.Release(this);
}