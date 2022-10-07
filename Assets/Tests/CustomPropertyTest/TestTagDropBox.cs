using UnityEngine;
using UnityEditor;

/// <summary>
/// TagName���C���X�y�N�^�ɕ`�悷�邽�߂̃N���X : <br/>
/// ���N���X�Ɏw�肵��PropertyAttribute�N���X�Ƃ́A
/// �J�X�^���v���p�e�B�[������h��������x�[�X�N���X�B
/// ������g�p���ăX�N���v�g�ϐ��̃J�X�^���������쐬���܂��B
/// 
/// �J�X�^�������� PropertyDrawer�N���X�ƘA�����āA
/// ���̑��������X�N���v�g�ϐ���"�C���X�y�N�^�[���"�ǂ��\������邩���䂵�܂��B
/// </summary>
public class TagNameAttribute : PropertyAttribute { }

/// <summary>
/// CustomPropertyDrawer�ɂ͑�������ݒ肷��B<br/>
/// ���̃N���X�́A������t�^��������ɍ��킹������������N���X�B<br/>
/// </summary>
[CustomPropertyDrawer(typeof(TagNameAttribute))]
public class TestTagDropBox : PropertyDrawer
{
	/// <summary>
	/// �C���X�y�N�^�ɕ\�����鏈���B<br/>
	/// �����ɍ��킹�ēƎ��̋���������悤�ɐݒ肷��B<br/>
	/// ����́ATag�̃h���b�v�{�b�N�X���쐬����B<br/>
	/// </summary>
	/// <param name="position">
	/// �`�悷���`(����͎g�p���Ȃ��B)
	/// </param>
	/// <param name="property">
	/// ������t�^��������̃v���p�e�B
	/// </param>
	/// <param name="label">
	/// �C���X�y�N�^�ɕ\�����邽�߂̂��낢��ȏ��������Ă���B
	/// �����f�t�H���g�ł͕ϐ����������Ă���B
	/// </param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// SerializedProperty�� GUI �ŊǗ����₷������悤�ɂ��邽�߂�
		// �v���p�e�B�[���b�p�[GUI �O���[�v���쐬���܂��B
		// �v���p�e�B���ȒP�ɒǉ��A�ύX�A�ė��p���邽�߂̂��́B
		EditorGUI.BeginProperty(position, label, property);
		// ���[�U�[�ɂ���Đݒ肳�ꂽ�l(Tag�ꗗ����ݒ肳�ꂽ�l)���v���p�e�B�ɐݒ肷��B
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		// BeginProperty �ƊJ�n���� Property Wrapper ���I�����܂��B
		EditorGUI.EndProperty();
	}
}