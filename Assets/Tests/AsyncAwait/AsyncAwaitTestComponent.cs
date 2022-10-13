using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// async/await勉強用コンポーネント
/// </summary>
public class AsyncAwaitTestComponent : MonoBehaviour
{
    [SerializeField]
    int _waitTime = 1000;
    [SerializeField]
    KeyCode _runKeyCode;


    AsyncAwaitTestClass _asyncAwaitTestClass = new AsyncAwaitTestClass();
    /// <summary>
    /// Startを別スレッドで実行する。
    /// </summary>
    /// <returns>
    /// 非同期メソッドには、次の戻り値の型があります。
    /// Task:          操作を実行し、値を返さない非同期メソッドの場合。
    /// Task<TResult>: 値を返す非同期メソッドの場合。
    /// </returns>
    private async Task Start()
    {
        var result = await ExampleAsync();
        Debug.Log(result); // 1
    }
    private void Update()
    {
        if (Input.GetKeyDown(_runKeyCode))
        {
            var a = _asyncAwaitTestClass.ExampleAsync();
        }
    }

    /// <summary>
    /// awaitで非同期で処理を待ちたいメソッド。
    /// Genericで型を指定する。
    /// </summary>
    /// <returns>
    /// 戻り値の型
    /// </returns>
    private async Task<int> ExampleAsync()
    {
        // 1秒待つ
        await Task.Run(() => Thread.Sleep(_waitTime));
        // Genericで指定した型の値を返す。
        return 1;
    }
}