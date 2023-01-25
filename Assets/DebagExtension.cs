using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebagExtension
#if UNITY_EDITOR
    : EditorWindow
#endif
{
    /// <summary>
    /// この値以上の重要度が設定された値をコンソールに表示する
    /// </summary>
    private static int _importance = 0;
    /// <summary>
    /// 種類フラグ: このフラグに登録されている値をコンソールに表示する <br/>
    /// 登録や削除などの使い方 : https://programming.pc-note.net/csharp/bit2.html
    /// </summary>
    private static LogType _type = LogType.None;

    /// <summary>
    /// 引数をもとにフィルタリングしてログをコンソールに表示する
    /// </summary>
    /// <param name="message"> コンソールに表示する内容 </param>
    /// <param name="importance"> 重要度 </param>
    /// <param name="type"> 種類 </param>
    public static void DebugLog(string message, int importance, LogType type)
    {
        if (_importance <= importance && _type.HasFlag(type))
        {
            Debug.Log($"重要度 :{importance}, 種類 :{type}\n" + message);
        }
    }
    /// <summary>
    /// 引数をもとにフィルタリングしてログをコンソールに表示する
    /// </summary>
    /// <param name="message"> コンソールに表示する内容 </param>
    /// <param name="importance"> 重要度 </param>
    /// <param name="type"> 種類 </param>
    public static void DebugLogWarning(string message, int importance, LogType type)
    {
        if (true/*ここに条件文を記述する*/)
        {
            Debug.LogWarning($"重要度 :{importance}, 種類 :{type}\n" + message);
        }
    }
    /// <summary>
    /// 引数をもとにフィルタリングしてログをコンソールに表示する
    /// </summary>
    /// <param name="message"> コンソールに表示する内容 </param>
    /// <param name="importance"> 重要度 </param>
    /// <param name="type"> 種類 </param>
    public static void DebugLogError(string message, int importance, LogType type)
    {
        if (true/*ここに条件文を記述する*/)
        {
            Debug.LogError($"重要度 :{importance}, 種類 :{type}\n" + message);
        }
    }
#if UNITY_EDITOR
    // ===================== 以下エディター上でのみ稼働するコード ===================== //
    /// <summary>
    /// チェックボックス用配列
    /// </summary>
    private bool[] _flags = new bool[Enum.GetValues(typeof(LogType)).Length];
    /// <summary>
    /// セットアップが完了しているかどうかを表す値
    /// </summary>
    private bool _isSetup = false;
    /// <summary>
    /// "重要度を表す値"をPlayerPrefsへ保存・取得の際に使用するキー
    /// </summary>
    private const string _importancePlayerPrefsKey = "ImportancePPKey";
    /// <summary>
    /// "種類フラグを表す値"をPlayerPrefsへ保存・取得の際に使用するキー<br/>
    /// 使用する際は末尾にIndex番号を付与する。
    /// </summary>
    private const string _flagsPlayerPrefsKey = "FlagsPPKey";

    //ウインドウを開くメニューのパス
    [MenuItem("Window/Debug Extension")]
    public static void ShowWindow()
    {
        //ウインドウを作成して表示
        EditorWindow.GetWindow(typeof(DebagExtension));
    }

    /// <summary>
    /// ここにウインドウの内容を描画する処理を書く
    /// </summary>
    private void OnGUI()
    {
        if (!_isSetup)
        {
            LoadTheValue();
            _isSetup = true;
        }
        EditorGUILayout.LabelField("重要度値 :0が重要度が小さく, 数値が大きくなるほど重要度が高くなる。最大10。");
        EditorGUILayout.LabelField("                 この値以上の値のみコンソールウィンドウに出力する。");
        // 重要度をスライダーから取得しフィールドに保存する
        _importance = EditorGUILayout.IntSlider(
            "DrawImportance", _importance, 0, 5);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("フィルタリングタイプ :選択されている種類のみコンソールウィンドウに出力する。");
        // 種類を表すチェックボックス群の処理
        for (int i = 0; i < _flags.Length; i++)
        {
            RegisterType(i);
        }
    }
    /// <summary>
    /// 入力を適用する処理
    /// </summary>
    /// <param name="index"> 
    /// トグルに値が設定されたら対応するフラグを切り替える
    /// </param>
    private void RegisterType(int index)
    {
        if (_flags[index] != EditorGUILayout.Toggle(IntToType(index).ToString(), _flags[index]))
        {
            if (index == 0)
            {
                // 全てのFlagを寝かせる
                _type &= IntToType(index);
            }
            else if (index == 1)
            {
                // 全てのFlagを立たせる
                _type |= IntToType(index);
            }
            else if (!EditorGUILayout.Toggle(IntToType(index).ToString(), _flags[index]))
            {
                _type |= IntToType(index);
            }
            else
            {
                _type &= ~IntToType(index);
            }
            OnUpdateValue();
            SaveTheValue();
        }
    }
    /// <summary>
    /// フラグ配列の値を更新する
    /// </summary>
    private void OnUpdateValue()
    {
        for (int i = 0; i < _flags.Length; i++)
        {
            if (i == 0)
            {
                // Noneの処理
                _flags[i] = IsNone();
            }
            else if (i == 1)
            {
                // Everyの処理 :
                _flags[i] = _type == LogType.Every;
            }
            else
            {
                _flags[i] = (_type & IntToType(i)) != 0;
            }
        }
    }
    /// <summary>
    /// PlayerPrefsから値を取得しフィールドに保存する
    /// </summary>
    private void LoadTheValue()
    {
        // 重要度をPlayerPrefsから値を取得しフィールドに保存する。
        _importance = PlayerPrefs.GetInt(_importancePlayerPrefsKey, 0);
        // 種類フラグをPlayerPrefsから値を取得しフィールドに保存する。
        for (int i = 0; i < _flags.Length; i++)
        {
            _flags[i] = PlayerPrefs.GetInt(_flagsPlayerPrefsKey + i.ToString(), 0) == 1;
        }
    }
    /// <summary>
    /// PlayerPrefsに値を保存する
    /// </summary>
    private void SaveTheValue()
    {
        // 重要度をPlayerPrefsに保存する。
        PlayerPrefs.SetInt(_importancePlayerPrefsKey, _importance);
        // 種類フラグをPlayerPrefsに保存する。
        for (int i = 0; i < _flags.Length; i++)
        {
            PlayerPrefs.SetInt(_flagsPlayerPrefsKey + i.ToString(), _flags[i] ? 1 : 0);
        }
    }

    // =================== 以下 :補助用関数 =================== //
    private bool IsNone()
    {
        // どのFlagとも合致しないとき, trueを返す
        // NoneとEveryの判定は除外する為, 2から開始する
        for (int i = 2; i < Enum.GetValues(typeof(LogType)).Length; i++)
        {
            if ((_type & IntToType(i)) != 0)
            {
                return false;
            }
        }
        return true;
    }
    private LogType IntToType(int index)
    {
        switch (index)
        {
            case 0: return LogType.None;
            case 1: return LogType.Every;
            case 2: return LogType.Player;
            case 3: return LogType.Enemy;
            case 4: return LogType.Gimmick;
            case 5: return LogType.Other;
            default:
                Debug.LogError("不正値が渡されました！");
                return LogType.None;
        }
    }
#endif
}

[Flags]
public enum LogType
{
    None = 0,
    Every = ~0,
    Player = 1 << 0,
    Enemy = 1 << 1,
    Gimmick = 1 << 2,
    Other = 1 << 3,
}