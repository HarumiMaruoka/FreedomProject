using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

[System.Serializable]
public class AsyncAwaitTestClass
{
    /// <summary>
    /// 別スレッドで実行するメソッド。
    /// async 修飾子を使用して、メソッド、ラムダ式、または匿名メソッドが非同期であることを指定します。 
    /// この修飾子が使用されているメソッドまたは式を、"非同期メソッド" と呼びます。 
    /// </summary>
    /// <returns>
    /// 非同期メソッドには、次の戻り値の型があります。
    /// Task:          操作を実行し、値を返さない非同期メソッドの場合。
    /// Task<TResult>: 値を返す非同期メソッドの場合。
    /// </returns>
    public async Task Test()
    {
        // 非同期メソッドは、最初の await 式に到達するまで同期的に実行されますが、
        // この時点で、待機していたタスクが完了するまで中断されます。
        // その間はメソッドの呼び出し元に制御が戻ります。
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
        // n秒待つ（指定時間はﾐﾘsecond）
        Debug.Log("始まり");
        await Task.Run(() => Thread.Sleep(5000));
        Debug.Log("終わり");
        // Genericで指定した型の値を返す。
        return 1;
    }
}