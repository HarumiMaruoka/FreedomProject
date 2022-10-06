using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestMove : MonoBehaviour
{
    Rigidbody2D _rb2D;
    [SerializeField] float _moveSpeed = 1f;
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        //�E�Ɉړ����鏈��
        if (h > 0.5f)
        {
            _rb2D.velocity = Vector2.right * _moveSpeed;
        }
        //���Ɉړ����鏈��
        else if (h < -0.5f)
        {
            _rb2D.velocity = Vector2.left * _moveSpeed;
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }
}
