using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// TagName���C���X�y�N�^�ɕ`�悷�邽�߂̃N���X : 
/// </summary>
public class TagNameAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagNameAttribute))]
public class TestTagDropBox : PropertyDrawer
{
	// �����ɂ��ẮA���t�@�����X���痬�p���L�q

	/// <summary>
	/// ���̃��\�b�h���I�[�o�[���C�h���ăv���p�e�B�[�Ɏ��g�� GUI ���쐬���܂��B
	/// </summary>
	/// <param name="position">
	/// �v���p�e�B�[�� GUI �Ɏg�p�����ʂ� Rect <br/>
	/// �`��͈͂̋�` <br/>
	/// </param>
	/// <param name="property">
	/// �ΏۂƂȂ�V���A���C�Y�����ꂽ�v���p�e�B�[ <br/>
	/// ������t�^�����ϐ��ɐݒ肵���l���i�[����Ă���Ǝv����B <br/>
	/// </param>
	/// <param name="label">
	/// ���̃v���p�e�B�[�̃��x�� <br/>
	/// ������t�^�����ϐ��ɐݒ肵���l���i�[����Ă���Ǝv����B <br/>
	/// </param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// SerializedProperty�� GUI �ŊǗ����₷������悤�ɂ��邽�߂̃v���p�e�B�[��
		// ���b�p�[�ł��� GUI �O���[�v���쐬���܂��B
		EditorGUI.BeginProperty(position, label, property);
		// string ���[�U�[�ɂ���Đݒ肳�ꂽ�l�B
		// �^�O�I���t�B�[���h���쐬���܂��B
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		// BeginProperty �ƊJ�n���� Property Wrapper ���I�����܂��B
		EditorGUI.EndProperty();
	}
}
#endif