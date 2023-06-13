using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpSpeed = 100_000;
    private bool _isJump = false;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        if (_isJump)
        {
            return;
        }

        _isJump = true;
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
        }
    }
}
