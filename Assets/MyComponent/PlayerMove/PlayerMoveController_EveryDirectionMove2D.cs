using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ړ��R���|�[�l���g : �C���|�[�g��ł��̃Q�[���p�ɕҏW����Ƃ悢
/// 2D�S�����p : �ړ��R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController_EveryDirectionMove2D
    : MonoBehaviour
{
    //<===== ���̃N���X�Ŏg�p����R���|�[�l���g =====>//
    Rigidbody2D _rigidbody2D;

    //<===== �C���X�y�N�^����ݒ肷�ׂ��l =====>//
    [Header("�ړ����x"), SerializeField]
    float _moveSpeed = 1f;
    [Header("���ړ��Ɋ��蓖�Ă�ꂽ�{�^���̖��O"), SerializeField]
    string _horizontalButtonName = "";
    [Header("�c�ړ��Ɋ��蓖�Ă�ꂽ�{�^���̖��O"), SerializeField]
    string _verticalButtonName = "";
    [Header("�_�b�V���Ɋ��蓖�Ă�ꂽ�{�^���̖��O"), SerializeField]
    string _dashButtonName = "";

    //<===== ���̃N���X�Ŏg�p����l =====>//
    float _inputX = 0f;
    float _inputY = 0f;
    bool _isDash;

    void Start()
    {
        Initialized();
    }

    void Update()
    {
        Input_Move();
        Update_Move();
    }

    /// <summary> ������ </summary>
    private void Initialized()
    {
        // �R���|�[�l���g���擾
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary> ���� </summary>
    void Input_Move()
    {
        _inputX = Input.GetAxisRaw(_horizontalButtonName);
        _inputY = Input.GetAxisRaw(_verticalButtonName);
        _isDash = Input.GetButton(_dashButtonName);
    }

    /// <summary> �ړ� </summary>
    void Update_Move()
    {
        //���x = ���͂Ɋ�Â��������x�N�g�� * �_�b�V�����͂�����΁u2�v�B�����łȂ���΁u1�v 
        _rigidbody2D.velocity = (Vector2.up * _inputY + Vector2.right * _inputX).normalized * (_isDash ? 1f : 2f);
    }
}
