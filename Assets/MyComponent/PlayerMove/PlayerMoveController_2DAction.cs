using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動コンポーネント : インポート先でそのゲーム用に編集するとよい 
/// 2Dアクション用 : 移動コンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController_2DAction : MonoBehaviour
{
    //<===== このクラスで使用するコンポーネント =====>//
    Rigidbody2D _rigidbody2D;

    //<===== インスペクタから設定すべき値 =====>//
    [Header("移動速度"), SerializeField]
    float _moveSpeed = 1f;
    [Header("横移動に割り当てられたボタンの名前"), SerializeField]
    string _horizontalButtonName = "";
    [Header("縦移動に割り当てられたボタンの名前"), SerializeField]
    string _verticalButtonName = "";
    [Header("ダッシュに割り当てられたボタンの名前"), SerializeField]
    string _dashButtonName = "";
    [Header("ジャンプに割り当てられたボタンの名前"), SerializeField]
    string _jumpButtonName = "";

    [Header("右に何かがないか判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchRight;
    [Header("左に何かがないか判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchLeft;
    [Header("左右に何かないか判定用オーバーラップボックスのサイズ"), SerializeField]
    private Vector3 _overLapBoxSize_IsTouchLeftAndRight;
    [Header("左右判定用オーバーラップボックスのレイヤーマスク : 必要な分だけ増やす"), SerializeField]
    private LayerMask _layerMask_IsTouchLeftAndRight;

    [Header("接地判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsGround;
    [Header("接地判定用オーバーラップボックスのサイズ"), SerializeField]
    private Vector3 _overLapBoxSize_IsGround;
    LayerMask _layerMask_GroundCheck;

    [Header("横移動速度"), SerializeField]
    float _moveSpeedX = 0f;
    [Header("ジャンプ力"), SerializeField]
    float _jumpPower = 0f;

    //<===== このクラスで使用する値 =====>//
    float _inputHorizontal;
    bool _isDash;
    bool _isJump;
    Vector2 _newVelocity;
    [Header("デバッグ用 : Gizmo表示するかどうか"), SerializeField] bool _isGizmo;


    void Start()
    {
        Initialized();
    }

    void Update()
    {

    }

    /// <summary> 初期化 </summary>
    void Initialized()
    {
        // コンポーネントを取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary> 入力処理 </summary>
    void Input_Move()
    {
        //横入力
        _inputHorizontal = Input.GetAxisRaw(_horizontalButtonName);
        //ダッシュキー入力
        _isDash = Input.GetButton(_dashButtonName);
        //ジャンプキー入力
        _isJump = Input.GetButton(_jumpButtonName);
    }

    /// <summary> 移動処理 </summary>
    void Update_Move()
    {
        //===== 速度をリセット =====//
        _newVelocity = Vector2.zero;

        //===== 横移動処理 =====//
        //移動可能かどうか判定する : 移動方向に壁があれば、めり込もうとして止まっちゃうので、左右に壁がないか判定する。 レイヤーマスクは地面。
        if (CheckPlayer_Near(_overLapBoxOffset_IsTouchRight, _overLapBoxSize_IsTouchLeftAndRight, _layerMask_GroundCheck) &&
            CheckPlayer_Near(_overLapBoxOffset_IsTouchLeft, _overLapBoxSize_IsTouchLeftAndRight, _layerMask_GroundCheck))
        {
            //入力に基づいて速度を与える 
            _newVelocity.x +=_inputHorizontal * _moveSpeedX * (_isDash ? 1f : 2f);
        }

        //===== ジャンプ処理 =====//
        //入力があり 接地状態であれば ジャンプする。
        if (_isJump && GroundCheck())
        {
            //上方向に力を加える
            _newVelocity += Vector2.up * _jumpPower;
        }
        //ジャンプしない時は通常の重力の影響を受ける。
        //逆説的にジャンプするフレームは、重力の影響を受けない。
        else
        {
            _newVelocity.y += _rigidbody2D.velocity.y;
        }

        //===== 処理結果を、Rigidbody2Dに与える。 =====//
        _rigidbody2D.velocity = _newVelocity;
    }

    /// <summary> プレイヤーの付近に何かないかLayerMaskを基準に判定する。 </summary>
    /// <param name="checkPlayerPosOffset"> チェックするポジション </param>
    /// <param name="checkSize"> チェックするサイズ </param>
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

    /// <summary> 接地判定 </summary>
    /// <returns> 接地していれば true を返す。 </returns>
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

    /// <summary> Gizmo表示 </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            //右に何があるか判定する用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchRight + transform.position, _overLapBoxSize_IsTouchLeftAndRight);
            //左に何があるか判定する用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchLeft + transform.position, _overLapBoxSize_IsTouchLeftAndRight);
            //接地判定用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsGround, _overLapBoxSize_IsGround);
        }
    }
}
