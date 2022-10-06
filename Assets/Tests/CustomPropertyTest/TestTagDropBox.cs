using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// TagNameをインスペクタに描画するためのクラス : 
/// </summary>
public class TagNameAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagNameAttribute))]
public class TestTagDropBox : PropertyDrawer
{
	// 説明については、リファレンスから流用しつつ記述

	/// <summary>
	/// このメソッドをオーバーライドしてプロパティーに自身の GUI を作成します。
	/// </summary>
	/// <param name="position">
	/// プロパティーの GUI に使用する画面の Rect <br/>
	/// 描画範囲の矩形 <br/>
	/// </param>
	/// <param name="property">
	/// 対象となるシリアライズ化されたプロパティー <br/>
	/// 属性を付与した変数に設定した値が格納されていると思われる。 <br/>
	/// </param>
	/// <param name="label">
	/// このプロパティーのラベル <br/>
	/// 属性を付与した変数に設定した値が格納されていると思われる。 <br/>
	/// </param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// SerializedPropertyを GUI で管理しやすくするようにするためのプロパティーの
		// ラッパーである GUI グループを作成します。
		EditorGUI.BeginProperty(position, label, property);
		// string ユーザーによって設定された値。
		// タグ選択フィールドを作成します。
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		// BeginProperty と開始した Property Wrapper を終了します。
		EditorGUI.EndProperty();
	}
}
#endif