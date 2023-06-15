using UnityEngine;

public class MonsterContainer : MonoBehaviour
{
    public GameObject OnAttackEffects;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Util.Layer.Ground)
        {
            Managers.Instance.GameManager.OnAttack();
            PlayerModel.SetOnAttack();

            OnAttackEffects.gameObject.SetActive(false);
            OnAttackEffects.gameObject.SetActive(true);
        }
    }
}
