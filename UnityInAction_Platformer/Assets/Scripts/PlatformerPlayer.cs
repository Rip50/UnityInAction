using System;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 150.0f;
    public float jumpForce = 12.0f;
    
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _boxCollider;

    private Collider2D GetCurrentSurface()
    {
        var bounds = _boxCollider.bounds;
        var (min, max) = (bounds.min, bounds.max);
        var corner1 = new Vector2(max.x, min.y - .1f);
        var corner2 = new Vector2(min.x, min.y - .2f);
        return Physics2D.OverlapArea(corner1, corner2);
    }
    private bool IsGrounded => GetCurrentSurface() != null;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _body.gravityScale = IsGrounded && deltaX == 0 ? 0 : 1;
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        var platform = IsGrounded ?  GetCurrentSurface().GetComponent<MovingPlatform>() : null;
        transform.parent = platform != null ? platform.transform : null;

        var pScale = platform != null ? platform.transform.localScale : Vector3.one;
        if (deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
        
        _anim.SetBool("IsGrounded", IsGrounded);
    }

    private void FixedUpdate()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var movement = new Vector2(deltaX, _body.velocity.y);
        
        _body.velocity = movement;

        _anim.SetFloat("Speed", Mathf.Abs(deltaX));
        
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
}
