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
        Debug.Log("Rigidbody�ړ��ɐ؂�ւ��܂�");
    }
    private void Update()
    {
        MoveRb();
        MoveCC();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeMovementMethod(MovementMethodType.Rigidbody);
            Debug.Log("Rigidbody�ړ��ɐ؂�ւ��܂�");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMovementMethod(MovementMethodType.CharacterController);
            Debug.Log("CharacterController�ړ��ɐ؂�ւ��܂�");
        }
    }
    private void MoveRb()
    {
        Vector3 MoveHorizontalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveSpeed = MoveHorizontalDir.normalized;
        //�@���C���J��������ɕ��������߂�B
        _moveSpeed = Camera.main.transform.TransformDirection(_moveSpeed);
        // �ړ�����������
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
        // ���������̈ړ��v�Z
        Vector3 MoveHorizontalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveSpeed = MoveHorizontalDir.normalized;
        //�@���C���J��������ɕ��������߂�B
        _moveSpeed = Camera.main.transform.TransformDirection(_moveSpeed);
        // �ړ�����������
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

        // ���������̈ړ��v�Z

        if (!_characterController.isGrounded)
        {
            _moveSpeedY -= _gravity * Time.deltaTime; // �d�͂̌v�Z
            _moveSpeed.y = _moveSpeedY;
        }
        else
        {
            _moveSpeed.y = _moveSpeedY = 0f;
        }
        if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded) // �W�����v�̏���
        {
            _moveSpeedY = _jumpSpeed;
        }

        // �L�����N�^�[�R���g���[���[�ɒl��n���B
        _characterController.Move(_moveSpeed);
    }

    /// <summary> �ړ����@�̕ύX </summary>
    public void ChangeMovementMethod(MovementMethodType useType)
    {
        if (useType == MovementMethodType.CharacterController)
        {
            _rigidbody.isKinematic = true;       // �����v�Z�𖳌�������
            _characterController.enabled = true; // �L�����N�^�[�R���g���[���[���N������
        }
        else if (useType == MovementMethodType.Rigidbody)
        {
            _rigidbody.isKinematic = false;       // �����v�Z��L��������
            _characterController.enabled = false; // �L�����N�^�[�R���g���[���[���~����
        }
        else
        {
            Debug.LogError("�z�肵�Ă��Ȃ��l���n����܂���");
        }
    }
    public enum MovementMethodType
    {
        CharacterController,
        Rigidbody,
    }
}