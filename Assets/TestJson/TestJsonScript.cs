using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestJsonScript : MonoBehaviour
{
    // JSON�`���̃f�[�^�ǂݏ����e�X�g
    // �ʒu�f�[�^
    [System.Serializable]//���̈ꕶ�ŃC���X�y�N�^��ŕҏW�ł���悤�ɂȂ邾��
    private struct PositionData
    {
        public Vector3 position;
    }

    const int maxItem = 6;
    private struct ItemData
    {
        //�R���X�g���N�^
        public ItemData(int max)
        {
            _itemData = new int[max];
        }
        //�������̊i�[��
        public int[] _itemData;
    }
    //�A�C�e�����������i�[
    ItemData _item = new ItemData(maxItem);

    /// <summary> �ړ����x </summary>
    [SerializeField] float _moveSpeed;

    /// <summary> �t�@�C���p�X </summary>
    [SerializeField] string _filePath;
    [SerializeField] string _itemFilePath;

    [SerializeField] Text _itemText;

    private void Awake()
    {

    }

    private void Start()
    {
        // ���W�t�@�C���̃p�X���擾���A�ǂݍ��ށB
        _filePath = Path.Combine(Application.persistentDataPath, "TestJson.json");
        OnPositionLoad(_filePath);
        // �A�C�e���������t�@�C���̃p�X���擾
        _itemFilePath = Path.Combine(Application.persistentDataPath, "TestJson2.json");
        OnItemLoad(_itemFilePath);
    }

    private void Update()
    {
        // 1�L�[�����Ō��݈ʒu���Z�[�u����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Save!");
            OnPositionSave(_filePath);
        }
        // 2�L�[�����Ō��݈ʒu�����[�h����
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("PositionLoad!");
            OnPositionLoad(_filePath);
        }

        // 3�L�[�����ŃA�C�e�����������Z�[�u����
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("ItemSave!");
            OnItemSave(_itemFilePath);
        }
        // 4�L�[�����ŃA�C�e�������������[�h����
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("ItemLoad!");
            OnItemLoad(_itemFilePath);
        }

        float moveX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float moveY = Input.GetAxisRaw("Vertical") * _moveSpeed;

        // �����L�[�ňړ��ł���悤�ɂ��Ă���
        transform.position = transform.position + new Vector3(moveX, moveY) * Time.deltaTime;
        //�A�C�e����������\������
        _itemText.text = 
            "item1 : " + _item._itemData[0] + '\n' +
            "item2 : " + _item._itemData[1] + '\n' +
            "item3 : " + _item._itemData[2] + '\n' +
            "item4 : " + _item._itemData[3] + '\n' +
            "item5 : " + _item._itemData[4] + '\n' +
            "item6 : " + _item._itemData[5] + '\n';
    }

    // JSON�`���ɃV���A���C�Y(�ϊ�)���ăZ�[�u
    private void OnPositionSave(string filePath)
    {
        // Json�ɃV���A���C�Y(�ϊ�)����I�u�W�F�N�g���쐬
        var obj = new PositionData { position = transform.position };

        // JSON�`���ɃV���A���C�Y(�ϊ�)
        var json = JsonUtility.ToJson(obj, false);

        // JSON�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(filePath, json);
    }

    // JSON�`�������[�h���ăf�V���A���C�Y
    private void OnPositionLoad(string filePath)
    {
        // �O�̂��߃t�@�C���̑��݃`�F�b�N
        if (!File.Exists(filePath)) return;

        // JSON�f�[�^�Ƃ��ăf�[�^��ǂݍ���
        var json = File.ReadAllText(filePath);

        // JSON�`������I�u�W�F�N�g�Ƀf�V���A���C�Y
        var obj = JsonUtility.FromJson<PositionData>(json);

        // Transform�ɃI�u�W�F�N�g�̃f�[�^���Z�b�g
        transform.position = obj.position;
    }

    // �A�C�e������JSON�`���ɕϊ����āA�Z�[�u
    private void OnItemSave(string filePath)
    {
        // JSON�`���ɃV���A���C�Y(�ϊ�)
        var json = JsonUtility.ToJson(_item, false);

        // JSON�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(filePath, json);
    }

    // JSON�`�������[�h���ăf�V���A���C�Y
    private void OnItemLoad(string filePath)
    {
        // �O�̂��߃t�@�C���̑��݃`�F�b�N
        if (!File.Exists(filePath))
        {
            //�t�@�C�������݂��Ȃ���Δz���0�ŏ�����
            _item._itemData = Enumerable.Repeat<int>(0, maxItem).ToArray();
            return;
        }

        // JSON�f�[�^�Ƃ��ăf�[�^��ǂݍ���
        var json = File.ReadAllText(filePath);

        // JSON�I�u�W�F�N�g���f�V���A���C�Y���A�l���Z�b�g�B
        _item = JsonUtility.FromJson<ItemData>(json);
    }

    public void ItemGet(int index)
    {
        _item._itemData[index]++;
    }

    public void ItemReset()
    {
        _item._itemData = Enumerable.Repeat<int>(0, maxItem).ToArray();
    }
}
