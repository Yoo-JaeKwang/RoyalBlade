using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpSpeed = 100_000;
    private bool _isJump = false;
    private Rigidbody2D _rb;
    private GameObject _jumpEffect;
    private void Awake()
    {
        _rb = gameObject.GetComponentAssert<Rigidbody2D>();
        Transform effects = transform.FindAssert("Effects");
        _jumpEffect = effects.FindAssert("JumpEffect").gameObject;
        _jumpEffect.transform.parent = default;
        _jumpEffect.SetActive(false);
    }
    public void Jump()
    {
        if (_isJump)
        {
            return;
        }

        _isJump = true;
        _jumpEffect.SetActive(false);
        _jumpEffect.SetActive(true);
        _rb.AddForce(Vector2.up * JumpSpeed);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == Util.Layer.Ground)
        {
            _isJump = false;
            _jumpEffect.SetActive(true);
        }
    }
}
