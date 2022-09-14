using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// ランキング機能を提供するクラス
/// </summary>
public class RankingSystem
{
    //===== シングルトン関連 =====//
    /// <summary> このクラス唯一のインスタンス </summary>
    static private RankingSystem _instance = new RankingSystem();
    /// <summary> インスタンスのプロパティ </summary>
    static public RankingSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RankingSystem();
            }
            return _instance;
        }
    }
    /// <summary>
    /// プライベートなコンストラクタ<br/>
    /// これで外部から勝手に生成される恐れがなくなる。<br/>
    /// 初期化処理はここに記述すること。<br/>
    /// </summary>
    private RankingSystem()
    {
        // ランキングデータを保存しているjsonファイルへのパスを設定する。
        _recordJsonFilePath = Path.Combine(Application.persistentDataPath, _recordJsonFileName);
        // ランキングデータ用のメモリを確保する。
        _rankingData._data = new IndividualData[_maximumNumberToRecord];
    }

    //===== 定数 =====//
    /// <summary> 記録する最大数 </summary>
    const int _maximumNumberToRecord = 12;
    /// <summary> 記録するjsonファイルの名前 </summary>
    const string _recordJsonFileName = "";

    //===== このクラスで利用する型 =====//
    /// <summary> 個別のスコアや名前などのデータ </summary>
    [System.Serializable]
    public struct IndividualData
    {
        // コンストラクタ
        public IndividualData(int score = -999, string name = "", DateTime dateTimeData = default)
        {
            _score = score;
            _name = name;
            dateTime = dateTimeData;
        }

        /// <summary> スコア </summary>
        public int _score;
        /// <summary> 名前 </summary>
        public string _name;
        /// <summary> 日付のデータ </summary>
        public DateTime dateTime;


        // 他のデータを追加する場合は、ここら辺に追加する事。
        // 追加した場合は、コンストラクタも修正する事。
    }
    /// <summary> 
    /// jsonファイルへ保存する為の型 <br/>
    /// 配列をそのままjson形式にシリアライズ、あるいはデシリアライズできないので<br/>
    /// その問題を回避する為にこの型を定義する。<br/>
    /// </summary>
    [System.Serializable]
    public struct Ranking
    {
        public IndividualData[] _data;
    }

    //===== メンバー変数 =====//
    /// <summary> ランキングのデータ </summary>
    Ranking _rankingData;
    /// <summary> ランキングデータのプロパティ </summary>
    public Ranking RankingData { get => _rankingData; }
    /// <summary>
    /// ランキングデータを保存しているjsonファイルへのパス。<br/>
    /// コンストラクタで値を設定する。<br/>
    /// </summary>
    readonly string _recordJsonFilePath = "";

    //===== メンバー関数 =====//
    // private
    /// <summary> ランキングデータをjsonファイルから取得し、メンバー変数に格納する。 </summary>
    void OnLoad_RankingData()
    {
        Debug.Log("ランキングデータをjsonファイルから取得し、メンバー変数に保存します。");
        // 念のためファイルの存在チェック
        if (!File.Exists(_recordJsonFilePath))
        {
            //ここにファイルが無い場合の処理を書く
            Debug.LogWarning("ランキングデータを保存しているファイルが見つかりません。");

            //処理を抜ける
            return;
        }
        // JSONオブジェクトを、デシリアライズ(C#形式に変換)し、値をセット。
        _rankingData = JsonUtility.FromJson<Ranking>(File.ReadAllText(_recordJsonFilePath));
    }
    /// <summary> メンバー変数に保存されている、ランキングデータをjsonファイルに保存する。 </summary>
    void OnSave_RankingData()
    {
        Debug.Log("ランキングデータをjsonファイルに保存します。");
        // アイテム所持数データを、JSON形式にシリアライズし、ファイルに保存
        File.WriteAllText(_recordJsonFilePath, JsonUtility.ToJson(_rankingData, false));
    }

    // public
    /// <summary>
    /// 引数に渡された値が、記録対象(ランキング県内)に当たるか判定し、<br/>
    /// ランキングにデータを追加する。<br/>
    /// </summary>
    /// <param name="addData"> 追加するデータ </param>
    public void Add_RankingData(IndividualData addData)
    {
        // ランキング県内か判定する。
        // 配列の末尾データより大きいかどうかで判定する。

        // ランキング県内の場合の処理
        if (_rankingData._data[_maximumNumberToRecord - 1]._score < addData._score)
        {
            // 末尾に値を代入する。
            _rankingData._data[_maximumNumberToRecord - 1] = addData;
            // スコアを降順でソートする。
            // スコアが同値であれば、日付が古い順にソートする。
            // (同率であればより早くその値にたどり着いた方が強い理論)
            _rankingData._data.OrderByDescending(d => d._score).ThenBy(d => d.dateTime);
        }

        // ランキング県外の場合なにもしない。
    }
}
