using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ړ��R���|�[�l���g : �C���|�[�g��ł��̃Q�[���p�ɕҏW����Ƃ悢 
/// 2D�A�N�V�����p : �ړ��R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController_2DAction : MonoBehaviour
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

    [Header("�E�ɉ������Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchRight;
    [Header("���ɉ������Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchLeft;
    [Header("���E�ɉ����Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃T�C�Y"), SerializeField]
    private Vector3 _overLapBoxSize_IsTouchLeftOrRight;

    [Header("�ڒn����p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsGround;
    [Header("�ڒn����p�I�[�o�[���b�v�{�b�N�X�̃T�C�Y"), SerializeField]
    private Vector3 _overLapBoxSize_IsGround;
    LayerMask _layerMask_GroundCheck;


    //<===== ���̃N���X�Ŏg�p����l =====>//
    float _moveSpeedX = 0f;
    float _moveSpeedY = 0f;
    bool _isDash;
    Vector2 _newVelocity;
    [Header("�f�o�b�O�p : Gizmo�\�����邩�ǂ���"), SerializeField] bool _isGizmo;


    void Start()
    {
        Initialized();
    }

    void Update()
    {
        
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
        _moveSpeedX = Input.GetAxisRaw(_horizontalButtonName);
        _isDash = Input.GetButton(_dashButtonName);
    }

    /// <summary> �ړ� </summary>
    void Update_Move()
    {
        //���x = ���͂Ɋ�Â��������x�N�g�� * �_�b�V�����͂�����΁u2�v�B�����łȂ���΁u1�v 
        _newVelocity += Vector2.right * _moveSpeedX * (_isDash ? 1f : 2f);


        _rigidbody2D.velocity = _newVelocity;
    }

    /// <summary> ���ɉ������邩�ǂ������肷�� </summary>
    /// <returns></returns>
    bool LeftCheck()
    {
        return false;
    }

    /// <summary> �E�ɉ������邩�ǂ������肷�� </summary>
    /// <returns></returns>
    bool RightCheck()
    {
        return false;
    }

    /// <summary> �ڒn���� </summary>
    /// <returns> �ڒn���Ă���� true ��Ԃ��B </returns>
    bool GroundCheck()
    {
        Collider2D[] collision = Physics2D.OverlapBoxAll(
            _overLapBoxOffset_IsGround+transform.position,
            _overLapBoxSize_IsGround,
            0f,
            _layerMask_GroundCheck);

        if (collision.Length != 0)
        {
            return true;
        }
        return false;
    }

    /// <summary> Gizmo�\�� </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            //�E�ɉ������邩���肷��p�̃I�[�o�[���b�v�{�b�N�X��`�悷��
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchRight + transform.position, _overLapBoxSize_IsTouchLeftOrRight);
            //���ɉ������邩���肷��p�̃I�[�o�[���b�v�{�b�N�X��`�悷��
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchLeft + transform.position, _overLapBoxSize_IsTouchLeftOrRight);
            //�ڒn����p�̃I�[�o�[���b�v�{�b�N�X��`�悷��
            Gizmos.DrawCube(_overLapBoxOffset_IsGround, _overLapBoxSize_IsGround);
        }
    }
}
