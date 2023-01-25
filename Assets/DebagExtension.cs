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
    /// ���̒l�ȏ�̏d�v�x���ݒ肳�ꂽ�l���R���\�[���ɕ\������
    /// </summary>
    private static int _importance = 0;
    /// <summary>
    /// ��ރt���O: ���̃t���O�ɓo�^����Ă���l���R���\�[���ɕ\������ <br/>
    /// �o�^��폜�Ȃǂ̎g���� : https://programming.pc-note.net/csharp/bit2.html
    /// </summary>
    private static LogType _type = LogType.None;

    /// <summary>
    /// ���������ƂɃt�B���^�����O���ă��O���R���\�[���ɕ\������
    /// </summary>
    /// <param name="message"> �R���\�[���ɕ\��������e </param>
    /// <param name="importance"> �d�v�x </param>
    /// <param name="type"> ��� </param>
    public static void DebugLog(string message, int importance, LogType type)
    {
        if (_importance <= importance && _type.HasFlag(type))
        {
            Debug.Log($"�d�v�x :{importance}, ��� :{type}\n" + message);
        }
    }
    /// <summary>
    /// ���������ƂɃt�B���^�����O���ă��O���R���\�[���ɕ\������
    /// </summary>
    /// <param name="message"> �R���\�[���ɕ\��������e </param>
    /// <param name="importance"> �d�v�x </param>
    /// <param name="type"> ��� </param>
    public static void DebugLogWarning(string message, int importance, LogType type)
    {
        if (true/*�����ɏ��������L�q����*/)
        {
            Debug.LogWarning($"�d�v�x :{importance}, ��� :{type}\n" + message);
        }
    }
    /// <summary>
    /// ���������ƂɃt�B���^�����O���ă��O���R���\�[���ɕ\������
    /// </summary>
    /// <param name="message"> �R���\�[���ɕ\��������e </param>
    /// <param name="importance"> �d�v�x </param>
    /// <param name="type"> ��� </param>
    public static void DebugLogError(string message, int importance, LogType type)
    {
        if (true/*�����ɏ��������L�q����*/)
        {
            Debug.LogError($"�d�v�x :{importance}, ��� :{type}\n" + message);
        }
    }
#if UNITY_EDITOR
    // ===================== �ȉ��G�f�B�^�[��ł̂݉ғ�����R�[�h ===================== //
    /// <summary>
    /// �`�F�b�N�{�b�N�X�p�z��
    /// </summary>
    private bool[] _flags = new bool[Enum.GetValues(typeof(LogType)).Length];
    /// <summary>
    /// �Z�b�g�A�b�v���������Ă��邩�ǂ�����\���l
    /// </summary>
    private bool _isSetup = false;
    /// <summary>
    /// "�d�v�x��\���l"��PlayerPrefs�֕ۑ��E�擾�̍ۂɎg�p����L�[
    /// </summary>
    private const string _importancePlayerPrefsKey = "ImportancePPKey";
    /// <summary>
    /// "��ރt���O��\���l"��PlayerPrefs�֕ۑ��E�擾�̍ۂɎg�p����L�[<br/>
    /// �g�p����ۂ͖�����Index�ԍ���t�^����B
    /// </summary>
    private const string _flagsPlayerPrefsKey = "FlagsPPKey";

    //�E�C���h�E���J�����j���[�̃p�X
    [MenuItem("Window/Debug Extension")]
    public static void ShowWindow()
    {
        //�E�C���h�E���쐬���ĕ\��
        EditorWindow.GetWindow(typeof(DebagExtension));
    }

    /// <summary>
    /// �����ɃE�C���h�E�̓��e��`�悷�鏈��������
    /// </summary>
    private void OnGUI()
    {
        if (!_isSetup)
        {
            LoadTheValue();
            _isSetup = true;
        }
        EditorGUILayout.LabelField("�d�v�x�l :0���d�v�x��������, ���l���傫���Ȃ�قǏd�v�x�������Ȃ�B�ő�10�B");
        EditorGUILayout.LabelField("                 ���̒l�ȏ�̒l�̂݃R���\�[���E�B���h�E�ɏo�͂���B");
        // �d�v�x���X���C�_�[����擾���t�B�[���h�ɕۑ�����
        _importance = EditorGUILayout.IntSlider(
            "DrawImportance", _importance, 0, 5);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("�t�B���^�����O�^�C�v :�I������Ă����ނ̂݃R���\�[���E�B���h�E�ɏo�͂���B");
        // ��ނ�\���`�F�b�N�{�b�N�X�Q�̏���
        for (int i = 0; i < _flags.Length; i++)
        {
            RegisterType(i);
        }
    }
    /// <summary>
    /// ���͂�K�p���鏈��
    /// </summary>
    /// <param name="index"> 
    /// �g�O���ɒl���ݒ肳�ꂽ��Ή�����t���O��؂�ւ���
    /// </param>
    private void RegisterType(int index)
    {
        if (_flags[index] != EditorGUILayout.Toggle(IntToType(index).ToString(), _flags[index]))
        {
            if (index == 0)
            {
                // �S�Ă�Flag��Q������
                _type &= IntToType(index);
            }
            else if (index == 1)
            {
                // �S�Ă�Flag�𗧂�����
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
    /// �t���O�z��̒l���X�V����
    /// </summary>
    private void OnUpdateValue()
    {
        for (int i = 0; i < _flags.Length; i++)
        {
            if (i == 0)
            {
                // None�̏���
                _flags[i] = IsNone();
            }
            else if (i == 1)
            {
                // Every�̏��� :
                _flags[i] = _type == LogType.Every;
            }
            else
            {
                _flags[i] = (_type & IntToType(i)) != 0;
            }
        }
    }
    /// <summary>
    /// PlayerPrefs����l���擾���t�B�[���h�ɕۑ�����
    /// </summary>
    private void LoadTheValue()
    {
        // �d�v�x��PlayerPrefs����l���擾���t�B�[���h�ɕۑ�����B
        _importance = PlayerPrefs.GetInt(_importancePlayerPrefsKey, 0);
        // ��ރt���O��PlayerPrefs����l���擾���t�B�[���h�ɕۑ�����B
        for (int i = 0; i < _flags.Length; i++)
        {
            _flags[i] = PlayerPrefs.GetInt(_flagsPlayerPrefsKey + i.ToString(), 0) == 1;
        }
    }
    /// <summary>
    /// PlayerPrefs�ɒl��ۑ�����
    /// </summary>
    private void SaveTheValue()
    {
        // �d�v�x��PlayerPrefs�ɕۑ�����B
        PlayerPrefs.SetInt(_importancePlayerPrefsKey, _importance);
        // ��ރt���O��PlayerPrefs�ɕۑ�����B
        for (int i = 0; i < _flags.Length; i++)
        {
            PlayerPrefs.SetInt(_flagsPlayerPrefsKey + i.ToString(), _flags[i] ? 1 : 0);
        }
    }

    // =================== �ȉ� :�⏕�p�֐� =================== //
    private bool IsNone()
    {
        // �ǂ�Flag�Ƃ����v���Ȃ��Ƃ�, true��Ԃ�
        // None��Every�̔���͏��O�����, 2����J�n����
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
                Debug.LogError("�s���l���n����܂����I");
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