using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 自由にいろいろ属性を作ってみるぞ！ <br/>
/// そのためのクラス <br/>
/// int型のシリアライズフィールドを再現 <br/>
/// </summary>
public class FreeAttribute : PropertyAttribute { }

/// <summary>
/// シリアライズフィールドに表示する処理
/// </summary>
[CustomPropertyDrawer(typeof(FreeAttribute))]
public class FreeInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 処理開始を宣言する命令？
        EditorGUI.BeginProperty(position, label, property);

        // ラベル(インスペクタに表示する名前のエリア)を設定する。
        EditorGUI.LabelField(position, label.text);

        //描画エリアを指定する
        float topX = EditorGUIUtility.labelWidth + 0.0f;                    // 左上頂点x座標
        float topY = position.y + 0.0f;                                     // 左上頂点y座標
        float width = position.width - EditorGUIUtility.labelWidth + 0.0f;  // 幅
        float height = EditorGUIUtility.singleLineHeight + 0.0f;            // 高さ

        position = new Rect(topX, topY, width, height);

        // int型の値を入力するエディットボックスを作成。
        // この属性を付与した値に入力された値を代入する。
        property.intValue = EditorGUI.IntField(position, 0);


        // これはいつものやつを作ってしまうので勉強にならん
        //UnityEditor.EditorGUI.
        //    PropertyField(position, property, label);

        // 処理終了を宣言する命令
        EditorGUI.EndProperty();
    }
}