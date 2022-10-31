using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動コンポーネント : インポート先でそのゲーム用に編集するとよい
/// 2D全方向用 : 移動コンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController_EveryDirectionMove2D
    : MonoBehaviour
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

    //<===== このクラスで使用する値 =====>//
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

    /// <summary> 初期化 </summary>
    private void Initialized()
    {
        // コンポーネントを取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary> 入力 </summary>
    void Input_Move()
    {
        _inputX = Input.GetAxisRaw(_horizontalButtonName);
        _inputY = Input.GetAxisRaw(_verticalButtonName);
        _isDash = Input.GetButton(_dashButtonName);
    }

    /// <summary> 移動 </summary>
    void Update_Move()
    {
        //速度 = 入力に基づいた方向ベクトル * ダッシュ入力があれば「2」。そうでなければ「1」 
        _rigidbody2D.velocity = (Vector2.up * _inputY + Vector2.right * _inputX).normalized * (_isDash ? 1f : 2f);
    }
}
