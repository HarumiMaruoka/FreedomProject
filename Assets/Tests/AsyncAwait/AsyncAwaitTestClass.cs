using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

[System.Serializable]
public class AsyncAwaitTestClass
{
    /// <summary>
    /// 別スレッドで実行するメソッド。
    /// </summary>
    /// <returns>
    /// 非同期メソッドには、次の戻り値の型があります。
    /// Task:          操作を実行し、値を返さない非同期メソッドの場合。
    /// Task<TResult>: 値を返す非同期メソッドの場合。
    /// </returns>
    public async Task Test()
    {
        var result = await ExampleAsync();
        Debug.Log(result); // 1
    }

    /// <summary>
    /// awaitで非同期で処理を待ちたいメソッド。
    /// Genericで型を指定する。
    /// </summary>
    /// <returns>
    /// 戻り値の型
    /// </returns>
    public async Task<int> ExampleAsync()
    {
        // 1秒待つ
        Debug.Log("始まり");
        await Task.Run(() => Thread.Sleep(5000));
        // Genericで指定した型の値を返す。
        Debug.Log("終わり");
        return 1;
    }
}