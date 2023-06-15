using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Player _player;
    private BoxCollider2D _col;
    private void Awake()
    {
        _player = transform.root.GetComponentAssert<Player>();
        _col = gameObject.GetComponentAssert<BoxCollider2D>();
        _col.enabled = false;
        _attackCo = AttackCo();
    }
    public void OnAttack()
    {
        StartCoroutine(_attackCo);
    }
    private IEnumerator _attackCo;
    private WaitForSeconds _sec = new(0.1f);
    private IEnumerator AttackCo()
    {
        while (true)
        {
            _col.enabled = true;
            yield return _sec;
            _col.enabled = false;
            StopCoroutine(_attackCo);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Util.Layer.Monster)
        {
            Monster monster = collision.gameObject.GetComponentAssert<Monster>();
            monster.OnAttack(_player.OnAttack());
        }
    }
}
