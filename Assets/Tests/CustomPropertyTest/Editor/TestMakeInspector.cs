using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���R�ɂ��낢�둮��������Ă݂邼�I <br/>
/// ���̂��߂̃N���X <br/>
/// int�^�̃V���A���C�Y�t�B�[���h���Č� <br/>
/// </summary>
public class FreeAttribute : PropertyAttribute { }

/// <summary>
/// �V���A���C�Y�t�B�[���h�ɕ\�����鏈��
/// </summary>
[CustomPropertyDrawer(typeof(FreeAttribute))]
public class FreeInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �����J�n��錾���閽�߁H
        EditorGUI.BeginProperty(position, label, property);

        // ���x��(�C���X�y�N�^�ɕ\�����閼�O�̃G���A)��ݒ肷��B
        EditorGUI.LabelField(position, label.text);

        //�`��G���A���w�肷��
        float topX = EditorGUIUtility.labelWidth + 0.0f;                    // ���㒸�_x���W
        float topY = position.y + 0.0f;                                     // ���㒸�_y���W
        float width = position.width - EditorGUIUtility.labelWidth + 0.0f;  // ��
        float height = EditorGUIUtility.singleLineHeight + 0.0f;            // ����

        position = new Rect(topX, topY, width, height);

        // int�^�̒l����͂���G�f�B�b�g�{�b�N�X���쐬�B
        // ���̑�����t�^�����l�ɓ��͂��ꂽ�l��������B
        property.intValue = EditorGUI.IntField(position, 0);


        // ����͂����̂������Ă��܂��̂ŕ׋��ɂȂ��
        //UnityEditor.EditorGUI.
        //    PropertyField(position, property, label);

        // �����I����錾���閽��
        EditorGUI.EndProperty();
    }
}