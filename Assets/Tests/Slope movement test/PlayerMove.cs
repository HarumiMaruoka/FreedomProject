using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private Vector3 _groundCheckPosOffset = default;
    [SerializeField]
    private Vector3 _groundCheckSize = default;
    [SerializeField]
    private float _radius = 1f;

    private Rigidbody2D _rigidbody2D = default;
    private float _baseGravity = default;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _baseGravity = _rigidbody2D.gravityScale;
    }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        // _rigidbody2D.velocity = new Vector2(h * _moveSpeed, _rigidbody2D.velocity.y);

        // 真下にレイを飛ばす
        // （今回はレイの長さは考えない。本来の長さは足元までにすべきだと思う。
        // 理由は空中でも反映されるのは困るから。）
        var hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (IsGrounded())
        {
            if (Mathf.Approximately(0f, h))
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.gravityScale = 0f;
            }
            else if (hit.normal != Vector2.up)
            {
                _rigidbody2D.gravityScale = _baseGravity;
                var dir = hit.normal;
                if (hit.normal.x > 0f && h > 0f)
                {
                    dir = new Vector2(hit.normal.x, -hit.normal.y).normalized;
                }
                else
                {
                    dir = new Vector2(-hit.normal.x, hit.normal.y).normalized;
                }
                _rigidbody2D.velocity = (dir * _moveSpeed) + new Vector2(0f, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.gravityScale = _baseGravity;
                _rigidbody2D.velocity = new Vector2(h * _moveSpeed, _rigidbody2D.velocity.y);
            }
        }
        else
        {
            _rigidbody2D.gravityScale = _baseGravity;
            _rigidbody2D.velocity = new Vector2(h * _moveSpeed, _rigidbody2D.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position + _groundCheckPosOffset, _groundCheckSize);
        Gizmos.DrawSphere(transform.position + _groundCheckPosOffset, _radius);
    }

    private bool IsGrounded()
    {
        var pos = transform.position + _groundCheckPosOffset;
        var size = _groundCheckSize;
        return Physics2D.OverlapCircleAll(pos,_radius).Length > 1;
        // return Physics2D.OverlapBoxAll(pos, size, 0f).Length > 1;
    }
}