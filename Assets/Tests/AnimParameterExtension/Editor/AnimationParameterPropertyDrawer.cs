using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimationParameterAttribute))]
public class AnimationParameterPropertyDrawer : PropertyDrawer
{
    private int _index = -1;
    private GUIContent[] _parameterNames = default;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool isSuccessSetUp = default;
        if (_index == -1)
        {
            isSuccessSetUp = SetUp(property);
        }
        if (isSuccessSetUp)
        {
            int oldIndex = _index;
            _index = EditorGUI.Popup(position, label, _index, _parameterNames);

            if (oldIndex != _index)
            {
                property.stringValue = _parameterNames[_index].text;
            }
        }
        else
        {
            label.text = "AnimationParameter属性が使用されていますが、AnimatorComponentがアタッチされていません";
            EditorGUI.LabelField(position, label.text);
        }
    }

    private bool SetUp(SerializedProperty property)
    {
        // アニメーターを取得
        Animator animator = (property.serializedObject.targetObject as Component).GetComponent<Animator>();
        if (animator == null) return false; // 取得に失敗したら処理を終了する。

        // アニメーションコントローラを取得
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

        // パラメータ名の配列を作成
        string[] parameterNames = animatorController.parameters.Select(p => p.name).ToArray();

        // GUIに描画するためのコンテントを作成
        _parameterNames = new GUIContent[parameterNames.Length];

        // コンテントに名前を設定する。
        for (int i = 0; i < parameterNames.Length; i++)
        {
            _parameterNames[i] = new GUIContent($"{parameterNames[i]}");
        }

        if (!string.IsNullOrEmpty(property.stringValue))
        {
            bool sceneNameFound = false;
            for (int i = 0; i < _parameterNames.Length; i++)
            {
                if (_parameterNames[i].text == property.stringValue)
                {
                    _index = i;
                    sceneNameFound = true;
                    break;
                }
            }
            if (!sceneNameFound)
                _index = 0;
        }
        else _index = 0;

        // プロパティに値を設定する。
        property.stringValue = _parameterNames[_index].text;

        return true;
    }
}