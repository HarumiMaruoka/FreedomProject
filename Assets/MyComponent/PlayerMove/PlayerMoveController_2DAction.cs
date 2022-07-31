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

    [Header("右に何かがないか判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchRight;
    [Header("左に何かがないか判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsTouchLeft;
    [Header("左右に何かないか判定用オーバーラップボックスのサイズ"), SerializeField]
    private Vector3 _overLapBoxSize_IsTouchLeftOrRight;

    [Header("接地判定用オーバーラップボックスのオフセット"), SerializeField]
    private Vector3 _overLapBoxOffset_IsGround;
    [Header("接地判定用オーバーラップボックスのサイズ"), SerializeField]
    private Vector3 _overLapBoxSize_IsGround;
    LayerMask _layerMask_GroundCheck;


    //<===== このクラスで使用する値 =====>//
    float _moveSpeedX = 0f;
    float _moveSpeedY = 0f;
    bool _isDash;
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
    private void Initialized()
    {
        // コンポーネントを取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary> 入力 </summary>
    void Input_Move()
    {
        _moveSpeedX = Input.GetAxisRaw(_horizontalButtonName);
        _isDash = Input.GetButton(_dashButtonName);
    }

    /// <summary> 移動 </summary>
    void Update_Move()
    {
        //速度 = 入力に基づいた方向ベクトル * ダッシュ入力があれば「2」。そうでなければ「1」 
        _newVelocity += Vector2.right * _moveSpeedX * (_isDash ? 1f : 2f);


        _rigidbody2D.velocity = _newVelocity;
    }

    /// <summary> 左に何かあるかどうか判定する </summary>
    /// <returns></returns>
    bool LeftCheck()
    {
        return false;
    }

    /// <summary> 右に何かあるかどうか判定する </summary>
    /// <returns></returns>
    bool RightCheck()
    {
        return false;
    }

    /// <summary> 接地判定 </summary>
    /// <returns> 接地していれば true を返す。 </returns>
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

    /// <summary> Gizmo表示 </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            //右に何があるか判定する用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchRight + transform.position, _overLapBoxSize_IsTouchLeftOrRight);
            //左に何があるか判定する用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsTouchLeft + transform.position, _overLapBoxSize_IsTouchLeftOrRight);
            //接地判定用のオーバーラップボックスを描画する
            Gizmos.DrawCube(_overLapBoxOffset_IsGround, _overLapBoxSize_IsGround);
        }
    }
}
