using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterControllerSample : MonoBehaviour
{
    [SerializeField]
    private float _testHorizontalMoveSpeed;
    [SerializeField]
    private float _jumpSpeed = 8f;
    [SerializeField]
    private float _gravity = 9.8f;
    [SerializeField]
    private float _rotationSpeed = 600f;
    [SerializeField]
    private bool _canMove = true;
    private CharacterController _characterController = null;
    private Vector3 _moveSpeed = default;
    private float _moveSpeedY = 0f;
    Quaternion _targetRotation = default;
    Rigidbody _rigidbody = null;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        ChangeMovementMethod(MovementMethodType.Rigidbody);
        Debug.Log("Rigidbody移動に切り替えます");
    }
    private void Update()
    {
        MoveRb();
        MoveCC();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeMovementMethod(MovementMethodType.Rigidbody);
            Debug.Log("Rigidbody移動に切り替えます");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMovementMethod(MovementMethodType.CharacterController);
            Debug.Log("CharacterController移動に切り替えます");
        }
    }
    private void MoveRb()
    {
        Vector3 MoveHorizontalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveSpeed = MoveHorizontalDir.normalized;
        //　メインカメラを基準に方向を決める。
        _moveSpeed = Camera.main.transform.TransformDirection(_moveSpeed);
        // 移動方向を向く
        if (_moveSpeed.magnitude > 0.5f)
        {
            _targetRotation = Quaternion.LookRotation(_moveSpeed, Vector3.up);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

        var rotation = transform.rotation;
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = rotation;
        _moveSpeed *= _testHorizontalMoveSpeed;

        _rigidbody.velocity = _moveSpeed + Vector3.up * _rigidbody.velocity.y;
    }
    private void MoveCC()
    {
        // 水平方向の移動計算
        Vector3 MoveHorizontalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveSpeed = MoveHorizontalDir.normalized;
        //　メインカメラを基準に方向を決める。
        _moveSpeed = Camera.main.transform.TransformDirection(_moveSpeed);
        // 移動方向を向く
        if (_moveSpeed.magnitude > 0.5f)
        {
            _targetRotation = Quaternion.LookRotation(_moveSpeed, Vector3.up);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

        var rotation = transform.rotation;
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = rotation;
        _moveSpeed *= _testHorizontalMoveSpeed * Time.deltaTime;

        // 垂直方向の移動計算

        if (!_characterController.isGrounded)
        {
            _moveSpeedY -= _gravity * Time.deltaTime; // 重力の計算
            _moveSpeed.y = _moveSpeedY;
        }
        else
        {
            _moveSpeed.y = _moveSpeedY = 0f;
        }
        if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded) // ジャンプの処理
        {
            _moveSpeedY = _jumpSpeed;
        }

        // キャラクターコントローラーに値を渡す。
        _characterController.Move(_moveSpeed);
    }

    /// <summary> 移動方法の変更 </summary>
    public void ChangeMovementMethod(MovementMethodType useType)
    {
        if (useType == MovementMethodType.CharacterController)
        {
            _rigidbody.isKinematic = true;       // 物理計算を無効化する
            _characterController.enabled = true; // キャラクターコントローラーを起動する
        }
        else if (useType == MovementMethodType.Rigidbody)
        {
            _rigidbody.isKinematic = false;       // 物理計算を有効化する
            _characterController.enabled = false; // キャラクターコントローラーを停止する
        }
        else
        {
            Debug.LogError("想定していない値が渡されました");
        }
    }
    public enum MovementMethodType
    {
        CharacterController,
        Rigidbody,
    }
}