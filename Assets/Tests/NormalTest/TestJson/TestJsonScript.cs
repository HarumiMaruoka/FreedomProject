using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestJsonScript : MonoBehaviour
{
    // JSON形式のデータ読み書きテスト
    // 位置データ
    [System.Serializable]//この一文でインスペクタ上で編集できるようになるだけ
    private struct PositionData
    {
        public Vector3 position;
    }

    const int maxItem = 6;
    private struct ItemData
    {
        //コンストラクタ
        public ItemData(int max)
        {
            _itemData = new int[max];
        }
        //所持数の格納先
        public int[] _itemData;
    }
    //アイテム所持数を格納
    ItemData _item = new ItemData(maxItem);

    /// <summary> 移動速度 </summary>
    [SerializeField] float _moveSpeed;

    /// <summary> ファイルパス </summary>
    [SerializeField] string _filePath;
    [SerializeField] string _itemFilePath;

    [SerializeField] Text _itemText;

    private void Awake()
    {

    }

    private void Start()
    {
        // 座標ファイルのパスを取得し、読み込む。
        _filePath = Path.Combine(Application.persistentDataPath, "TestJson.json");
        OnPositionLoad(_filePath);
        // アイテム所持数ファイルのパスを取得
        _itemFilePath = Path.Combine(Application.persistentDataPath, "TestJson2.json");
        OnItemLoad(_itemFilePath);
    }

    private void Update()
    {
        // 1キー押下で現在位置をセーブする
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Save!");
            OnPositionSave(_filePath);
        }
        // 2キー押下で現在位置をロードする
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("PositionLoad!");
            OnPositionLoad(_filePath);
        }

        // 3キー押下でアイテム所持数をセーブする
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("ItemSave!");
            OnItemSave(_itemFilePath);
        }
        // 4キー押下でアイテム所持数をロードする
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("ItemLoad!");
            OnItemLoad(_itemFilePath);
        }

        float moveX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float moveY = Input.GetAxisRaw("Vertical") * _moveSpeed;

        // 方向キーで移動できるようにしておく
        transform.position = transform.position + new Vector3(moveX, moveY) * Time.deltaTime;
        //アイテム所持数を表示する
        _itemText.text = 
            "item1 : " + _item._itemData[0] + '\n' +
            "item2 : " + _item._itemData[1] + '\n' +
            "item3 : " + _item._itemData[2] + '\n' +
            "item4 : " + _item._itemData[3] + '\n' +
            "item5 : " + _item._itemData[4] + '\n' +
            "item6 : " + _item._itemData[5] + '\n';
    }

    // JSON形式にシリアライズ(変換)してセーブ
    private void OnPositionSave(string filePath)
    {
        // Jsonにシリアライズ(変換)するオブジェクトを作成
        var obj = new PositionData { position = transform.position };

        // JSON形式にシリアライズ(変換)
        var json = JsonUtility.ToJson(obj, false);

        // JSONデータをファイルに保存
        File.WriteAllText(filePath, json);
    }

    // JSON形式をロードしてデシリアライズ
    private void OnPositionLoad(string filePath)
    {
        // 念のためファイルの存在チェック
        if (!File.Exists(filePath)) return;

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(filePath);

        // JSON形式からオブジェクトにデシリアライズ
        var obj = JsonUtility.FromJson<PositionData>(json);

        // Transformにオブジェクトのデータをセット
        transform.position = obj.position;
    }

    // アイテム情報をJSON形式に変換して、セーブ
    private void OnItemSave(string filePath)
    {
        // JSON形式にシリアライズ(変換)
        var json = JsonUtility.ToJson(_item, false);

        // JSONデータをファイルに保存
        File.WriteAllText(filePath, json);
    }

    // JSON形式をロードしてデシリアライズ
    private void OnItemLoad(string filePath)
    {
        // 念のためファイルの存在チェック
        if (!File.Exists(filePath))
        {
            //ファイルが存在しなければ配列を0で初期化
            _item._itemData = Enumerable.Repeat<int>(0, maxItem).ToArray();
            return;
        }

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(filePath);

        // JSONオブジェクトをデシリアライズし、値をセット。
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
