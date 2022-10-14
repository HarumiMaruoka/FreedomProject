using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveUp : MonoBehaviour
{
    Rigidbody2D _rigidBody2D;

    [SerializeField] float _moveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _rigidBody2D.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody2D.velocity = Vector2.up * _moveSpeed;
    }
}
