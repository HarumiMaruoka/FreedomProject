using UnityEngine;
using UnityEditor;

/// <summary>
/// TagNameをインスペクタに描画するためのクラス : <br/>
/// 基底クラスに指定したPropertyAttributeクラスとは、
/// カスタムプロパティー属性を派生させるベースクラス。
/// これを使用してスクリプト変数のカスタム属性を作成します。
/// 
/// カスタム属性は PropertyDrawerクラスと連結して、
/// その属性を持つスクリプト変数が"インスペクター上で"どう表示されるか制御します。
/// </summary>
public class TagNameAttribute : PropertyAttribute { }


/// <summary>
/// CustomPropertyDrawerには属性名を設定する。<br/>
/// このクラスは、属性を付与した相手に合わせた処理をするクラス。<br/>
/// </summary>
[CustomPropertyDrawer(typeof(TagNameAttribute))]
public class TestTagDropBox : PropertyDrawer
{
	/// <summary>
	/// インスペクタに表示する処理。<br/>
	/// 属性に合わせて独自の挙動をするように設定する。<br/>
	/// 今回は、Tagのドロップボックスを作成する。<br/>
	/// </summary>
	/// <param name="position">
	/// 描画する矩形(今回は使用しない。)
	/// </param>
	/// <param name="property">
	/// 属性を付与した相手のプロパティ
	/// </param>
	/// <param name="label">
	/// インスペクタに表示するためのいろいろな情報を持っている。
	/// 多分デフォルトでは変数名を持っている。
	/// </param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// SerializedPropertyを GUI で管理しやすくするようにするための
		// プロパティーラッパーGUI グループを作成します。
		// プロパティを簡単に追加、変更、再利用するためのもの。
		EditorGUI.BeginProperty(position, label, property);
		// ユーザーによって設定された値(Tag一覧から設定された値)をプロパティに設定する。
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		// BeginProperty と開始した Property Wrapper を終了します。
		EditorGUI.EndProperty();
	}
}