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
    [Header("�W�����v�Ɋ��蓖�Ă�ꂽ�{�^���̖��O"), SerializeField]
    string _jumpButtonName = "";

    [Header("�E�ɉ������Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchRight;
    [Header("���ɉ������Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchLeft;
    [Header("���E�ɉ����Ȃ�������p�I�[�o�[���b�v�{�b�N�X�̃T�C�Y"), SerializeField]
    private Vector3 _overLapBoxSize_IsTouchLeftAndRight;
    [Header("���E����p�I�[�o�[���b�v�{�b�N�X�̃��C���[�}�X�N : �K�v�ȕ��������₷"), SerializeField]
    private LayerMask _layerMask_IsTouchLeftAndRight;

    [Header("�ڒn����p�I�[�o�[���b�v�{�b�N�X�̃I�t�Z�b�g"), SerializeField]
    private Vector3 _overLapBoxOffset_IsGround;
    [Header("�ڒn����p�I�[�o�[���b�v�{�b�N�X�̃T�C�Y"), SerializeField]
    private Vector3 _overLapBoxSize_IsGround;
    LayerMask _layerMask_GroundCheck;

    [Header("���ړ����x"), SerializeField]
    float _moveSpeedX = 0f;
    [Header("�W�����v��"), SerializeField]
    float _jumpPower = 0f;

    //<===== ���̃N���X�Ŏg�p����l =====>//
    float _inputHorizontal;
    bool _isDash;
    bool _isJump;
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
    void Initialized()
    {
        // �R���|�[�l���g���擾
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary> ���͏��� </summary>
    void Input_Move()
    {
        //������
        _inputHorizontal = Input.GetAxisRaw(_horizontalButtonName);
        //�_�b�V���L�[����
        _isDash = Input.GetButton(_dashButtonName);
        //�W�����v�L�[����
        _isJump = Input.GetButton(_jumpButtonName);
    }

    /// <summary> �ړ����� </summary>
    void Update_Move()
    {
        //===== ���x�����Z�b�g =====//
        _newVelocity = Vector2.zero;

        //===== ���ړ����� =====//
        //�ړ��\���ǂ������肷�� : �ړ������ɕǂ�����΁A�߂荞�����Ƃ��Ď~�܂����Ⴄ�̂ŁA���E�ɕǂ��Ȃ������肷��B ���C���[�}�X�N�͒n�ʁB
        if (CheckPlayer_Near(_overLapBoxOffset_IsTouchRight, _overLapBoxSize_IsTouchLeftAndRight, _layerMask_GroundCheck) &&
            CheckPlayer_Near(_overLapBoxOffset_IsTouchLeft, _overLapBoxSize_IsTouchLeftAndRight, _layerMask_GroundCheck))
        {
            //���͂Ɋ�Â��đ��x��^���� 
            _newVelocity.x +=_inputHorizontal * _moveSpeedX * (_isDash ? 1f : 2f);
        }

        //===== �W�����v���� =====//
        //���͂����� �ڒn��Ԃł���� �W�����v����B
        if (_isJump && GroundCheck())
        {
            //������ɗ͂�������
            _newVelocity += Vector2.up * _jumpPower;
        }
        //�W�����v���Ȃ����͒ʏ�̏d�͂̉e�����󂯂�B
        //�t���I�ɃW�����v����t���[���́A�d�͂̉e�����󂯂Ȃ��B
        else
        {
            _newVelocity.y += _rigidbody2D.velocity.y;
        }

        //===== �������ʂ��ARigidbody2D�ɗ^����B =====//
        _rigidbody2D.velocity = _newVelocity;
    }

    /// <summary> �v���C���[�̕t�߂ɉ����Ȃ���LayerMask����ɔ��肷��B </summary>
    /// <param name="checkPlayerPosOffset"> �`�F�b�N����|�W�V���� </param>
    /// <param name="checkSize"> �`�F�b�N����T�C�Y </param>
    /// <returns></returns>
    bool CheckPlayer_Near(Vector3 checkPlayerPosOffset, Vector3 checkSize, LayerMask layerMask)
    {
        Collider2D[] collision = Physics2D.OverlapBoxAll(
               checkPlayerPosOffset + transform.position,
               checkSize,
               0f,
               layerMask);

        if (collision.Length != 0)
        {
            return true;
        }
        return false;
    }

    /// <summary> �ڒn���� </summary>
    /// <returns> �ڒn���Ă���� true ��Ԃ��B </returns>
    bool GroundCheck()
    {
        Collider2D[] collision = Physics2D.OverlapBoxAll(
            _overLapBoxOffset_IsGround + transform.position,
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
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchRight + transform.position, _overLapBoxSize_IsTouchLeftAndRight);
            //���ɉ������邩���肷��p�̃I�[�o�[���b�v�{�b�N�X��`�悷��
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchLeft + transform.position, _overLapBoxSize_IsTouchLeftAndRight);
            //�ڒn����p�̃I�[�o�[���b�v�{�b�N�X��`�悷��
            Gizmos.DrawCube(_overLapBoxOffset_IsGround, _overLapBoxSize_IsGround);
        }
    }
}
