using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class SampleMove : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 1f;

    Rigidbody2D _rb2D;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // MoveInputManager();
        // MoveInputSystemSample1();
    }

    private void MoveInputManager()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        _rb2D.velocity = new Vector2(h, v).normalized * _moveSpeed;
    }

    private void MoveInputSystemSample1()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("Ç¢Ç∏ÇÍÇ©ÇÃÉLÅ[Ç™âüÇ≥ÇÍÇΩ");
        }
        var h = 0f;
        if (Keyboard.current.aKey.IsPressed())
        {
            h--;
        }
        if (Keyboard.current.dKey.IsPressed())
        {
            h++;
        }
        var v = 0f;
        if (Keyboard.current.wKey.IsPressed())
        {
            v++;
        }
        if (Keyboard.current.sKey.IsPressed())
        {
            v--;
        }
        _rb2D.velocity = new Vector2(h, v).normalized * _moveSpeed;
    }
    public void MoveInputSystemSample2(InputAction.CallbackContext context)
    {
        _rb2D.velocity = context.ReadValue<Vector2>().normalized * _moveSpeed;
    }
}